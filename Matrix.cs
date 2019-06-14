using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixEssentials
{
    /// <summary>
    /// Matrix object
    /// </summary>
    public class Matrix : IMatrix
    {
        private readonly Type _matrixDataType;

        /// <summary>
        /// Internal matrix structure
        /// </summary>
        private readonly IList<IList<IMatrixData>> matrix;

        /// <summary>
        /// Creates matrix object with data from nested lists
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentException">when matrix list is empty</exception>
        /// <exception cref="ArgumentNullException">when matrix is null</exception>
        public Matrix(IList<IList<IMatrixData>> matrix)
        {
            if (matrix.Count == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(matrix));
            }


            this.Width = matrix[0].Count;
            this.Height = matrix.Count;
            this.matrix = matrix;
        }

        /// <summary>
        /// Creates empty matrix with given size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <exception cref="ArgumentOutOfRangeException">When width or height is not positive number</exception>
        /// <exception cref="ArgumentException">When matrixDataType is type not inheriting IMatrixData</exception>
        public Matrix(int width, int height, Type matrixDataType)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            if (!matrixDataType.GetInterfaces().Contains(typeof(IMatrixData)))
            {
                throw new ArgumentException(nameof(matrixDataType));
            }

            this._matrixDataType = matrixDataType;

            this.Width = width;
            this.Height = height;

            var matrixEmptyObject = (IMatrixData) Activator.CreateInstance(matrixDataType);
            this.matrix = new List<IList<IMatrixData>>();

            for (int i = 0; i < Height; i++)
            {
                this.matrix.Add(new List<IMatrixData>());

                for (int j = 0; j < Width; j++)
                {
                    this.matrix[i].Add(matrixEmptyObject);
                }
            }
        }

        public object RawValues => this.matrix;

        public int Width { get; }

        public int Height { get; }


        public IMatrixData Sum => this.matrix.SelectMany(number => number).ToList().Sum();

        public IMatrixData GetValue(int column, int row)
        {
            if (column >= Width || row >= Height || column < 0 || row < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.matrix[row][column];
        }

        public void SetValue(int column, int row, IMatrixData value)
        {
            if (column >= Width || row >= Height || column < 0 || row < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.matrix[row][column] = value;
        }

        public IMatrix Convolute(IMatrix kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            var resultMatrixSize = this.CalculateConvolutedImageDimensions(kernel);
            var resultMatrix = new Matrix(resultMatrixSize[0], resultMatrixSize[1], typeof(RGBMatrixData));

            for (var rowIndex = 0; rowIndex < resultMatrix.Height; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < resultMatrix.Width; columnIndex++)
                {
                    var newValue = CalculateValueForPosition(rowIndex, columnIndex, this, kernel);
                    resultMatrix.SetValue(columnIndex, rowIndex, newValue);
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Calculate value for position in result matrix (made from multiplication of image and kernel)
        /// </summary>
        /// <param name="image">image</param>
        /// <param name="row">current result matrix row</param>
        /// <param name="column">current result matrix column</param>
        /// <param name="kernel">kernel</param>
        /// <param name="kernelSum">Optional. Sum of kernel matrix. If not specified every time it will calculate (bad for performance i guess)</param>
        /// <returns>calculated value for specified position</returns>
        private IMatrixData CalculateValueForPosition(int row, int column, IMatrix image, IMatrix kernel,
            float kernelSum = -1)
        {
            IMatrixData endValue = (IMatrixData) Activator.CreateInstance(this._matrixDataType);

            for (var i = 0; i < kernel.Height; i++)
            {
                IMatrixData innerCycleCalculationResult = (IMatrixData) Activator.CreateInstance(this._matrixDataType);

                for (var j = 0; j < kernel.Width; j++)
                {
                    var imageColumn = column + j - 1;
                    var imageRow = row + i - 1;

                    //allows bluring edges of the picture
                    if (imageColumn < 0 || imageRow < 0 || imageColumn >= image.Width || imageRow >= image.Height)
                    {
                        continue;
                    }

                    innerCycleCalculationResult =
                        innerCycleCalculationResult.Add(image.GetValue(imageColumn, imageRow)
                            .MultiplyBy(kernel.GetValue(j, i)));
                }

                endValue = endValue.Add(innerCycleCalculationResult);
            }

            if (Math.Abs(kernelSum - (-1)) > 0.01)
            {
                return endValue.Divide(new FloatNumberMatrixData(kernelSum));
            }
            else
            {
                return endValue.Divide(kernel.Sum);
            }
        }

        /// <summary>
        /// Calculates convoluted image size
        /// </summary>
        /// <param name="kernel">kernel matrix</param>
        /// <returns>array of integers. 0 position is convoluted image width, 1 is convoluted image height</returns>
        private int[] CalculateConvolutedImageDimensions(IMatrix kernel)
        {
            return
                new[]
                {
                    this.Width - kernel.Width - 1,
                    this.Height - kernel.Height + 1
                };
        }

        public override string ToString()
        {
            var maxWhiteSpace = new string(' ', 10);
            var output = new StringBuilder();

            for (var i = 0; i < this.matrix.Count; i++)
            {
                var matrixRow = this.matrix[i];

                for (int j = 0; j < matrixRow.Count; j++)
                {
                    var matrixElement = matrixRow[j];
                    var matrixElementToString = matrixElement.ToString();

                    if (matrixElementToString.Length > maxWhiteSpace.Length - 1)
                    {
                        matrixElementToString = matrixElementToString.Substring(0, maxWhiteSpace.Length - 1);
                    }

                    string whitespace = null;

                    if (j == matrixRow.Count - 1)
                    {
                        output.AppendLine(matrixElementToString);
                        continue;
                    }

                    whitespace = maxWhiteSpace.Substring(0, maxWhiteSpace.Length - (matrixElementToString.Length - 1));
                    output.Append(matrixElementToString);
                    output.Append(whitespace);
                }
            }

            return output.ToString();
        }
    }
}