using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixEssentials
{
    /// <summary>
    /// Matrix object
    /// </summary>
    public class Matrix : IMatrix
    {
        private readonly Type matrixDataType;

        
        #if DEBUG
        private readonly bool isInDebugMode = true; 
        #else
        private readonly bool isInDebugMode = false;
#endif


        /// <summary>
        /// Zero representation of IMatrixData type used in this matrix. Used for reducing Activator.CreateInstance calls
        /// </summary>
        private IMatrixData defaultMatrixData;
        
        /// <summary>
        /// Use when you need ZeroRepresentation
        /// </summary>
        protected IMatrixData ZeroRepresentationOfMatrixData => this.defaultMatrixData.ZeroRepresentation;

        /// <summary>
        /// Internal matrix structure
        /// </summary>
        private readonly IMatrixData[][] matrix;

        /// <summary>
        /// Creates matrix object with data from nested lists
        /// </summary>
        /// <param name="matrix"></param>
        /// <exception cref="ArgumentException">when matrix list is empty</exception>
        /// <exception cref="ArgumentNullException">when matrix is null</exception>
        /// <exception cref="ArgumentException">when matrix's is containing data of different types</exception>
        public Matrix(IMatrixData[][] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }
            
            if (matrix.GetLength(0) == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(matrix));
            }
            
            this.matrixDataType = matrix[0][0].GetType();
            this.defaultMatrixData = (IMatrixData) Activator.CreateInstance(matrixDataType);

            if (matrix.SelectMany(matrixData => matrixData)
                .Any(matrixDataType => matrixDataType.GetType() != this.matrixDataType))
            {
                throw new ArgumentException("Inconsistent MatrixData type. all IMatrixData must be of same type");
            }

            this.Width = matrix[0].GetLength(0);
            this.Height = matrix.GetLength(0);
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

            this.matrixDataType = matrixDataType;
            this.defaultMatrixData = (IMatrixData) Activator.CreateInstance(matrixDataType);

            this.Width = width;
            this.Height = height;

            var matrixEmptyObject = ZeroRepresentationOfMatrixData;
            this.matrix = new IMatrixData[Height][];

            for (int i = 0; i < Height; i++)
            {
                this.matrix[i] = new IMatrixData[Width];

                for (int j = 0; j < Width; j++)
                {
                    this.matrix[i][j] = matrixEmptyObject;
                }
            }
        }

        public object RawValues => this.matrix;

        public int Width { get; }

        public int Height { get; }
        
        public IMatrixData Sum => this.matrix.SelectMany(number => number).ToList().Sum();

        public IMatrix Normalized
        {
            get
            {
                var highestValue = this.HighestValue;
                var result = new Matrix(this.Width, this.Height, this.matrixDataType);
                
                for (int i = 0; i < this.Height; i++)
                {
                    for (int j = 0; j < this.Width; j++)
                    {
                        var normalizedValue = this.matrix[i][j].Divide(highestValue);
                        result.SetValue(j, i, normalizedValue);
                    }
                }

                return result;
            }
        }
        
        public IMatrixData HighestValue
        {
            get
            {
                IMatrixData result = null;
                
                for (int i = 0; i < this.Height; i++)
                {
                    for (int j = 0; j < this.Width; j++)
                    {
                        if (result == null)
                        {
                            result = this.matrix[i][j];
                            continue;
                        }

                        var comparisionResult = result.CompareTo(this.matrix[i][j]);
                        if (comparisionResult < 0)
                        {
                            result = this.matrix[i][j];
                        }
                    }
                }

                return result;
            }
        }
        
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

        public IMatrix Add(IMatrix matrix)
        {
            if (this.Width != matrix.Width || this.Height != matrix.Height)
            {
                throw new ArgumentException("Matrices must have same width and height");
            }

            var matrixInternalStructure = new IMatrixData[Height][];
            
            for (int i = 0; i < matrix.Height; i++)
            {
                matrixInternalStructure[i] = new IMatrixData[Width];
                
                for (int j = 0; j < matrix.Width; j++)
                {
                    var matrixData = matrix.GetValue(j, i);
                    var currentMatrixData = this.GetValue(j, i);
                    var matrixDataAdditionResult = matrixData.Add(currentMatrixData);
                    matrixInternalStructure[i][j] = matrixDataAdditionResult;
                }
            }

            return new Matrix(matrixInternalStructure);
        }

        public IMatrix Convolute(IMatrix kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            var kernelSum = (float)Convert.ToDouble(kernel.Sum.RawValue);
            var resultMatrixSize = this.CalculateConvolutedImageDimensions(kernel);
            var resultMatrix = new Matrix(resultMatrixSize[0], resultMatrixSize[1], typeof(UnsafeRGBMatrixData));

            for (var rowIndex = 0; rowIndex < resultMatrix.Height; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < resultMatrix.Width; columnIndex++)
                {
                    var newValue = CalculateValueForPosition(rowIndex, columnIndex, this, kernel, kernelSum);
                    resultMatrix.SetValue(columnIndex, rowIndex, newValue);
                }
            }

            return resultMatrix;
        }

        public IMatrix ConvoluteParalleled(IMatrix kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }

            var kernelSum = (float)Convert.ToDouble(kernel.Sum.RawValue);
            var resultMatrixSize = this.CalculateConvolutedImageDimensions(kernel);
            var resultMatrix = new Matrix(resultMatrixSize[0], resultMatrixSize[1], typeof(UnsafeRGBMatrixData));

            Parallel.For(0, resultMatrix.Height, (rowIndex) =>
            {
                for (var columnIndex = 0; columnIndex < resultMatrix.Width; columnIndex++)
                {
                    var newValue = CalculateValueForPosition(rowIndex, columnIndex, this, kernel, kernelSum);
                    resultMatrix.SetValue(columnIndex, rowIndex, newValue);
                }
            });

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
            IMatrixData endValue = ZeroRepresentationOfMatrixData;

            for (var i = 0; i < kernel.Height; i++)
            {
                IMatrixData innerCycleCalculationResult = ZeroRepresentationOfMatrixData;

                for (var j = 0; j < kernel.Width; j++)
                {
                    var imageColumn = column + j - 1;
                    var imageRow = row + i - 1;

                    //allows bluring edges of the picture (a.k.a prevents from throwing exceptions when kernel is on the edge of the picture)
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
            
            if (Math.Abs(kernelSum - (-1)) > 0.001f)
            {
                if (Math.Abs(kernelSum) < 0.001f)
                {
                    return endValue.CompareTo(endValue.ZeroRepresentation) < 0 ? endValue.ZeroRepresentation : endValue;
                }
                
                return endValue.Divide(new FloatNumberMatrixData(kernelSum));
            }

            var sum = kernel.Sum;
            var sumValue = Convert.ToDouble(sum.RawValue);
            
            if (Math.Abs(sumValue) > 0.001f)
            {
                return endValue.Divide(new FloatNumberMatrixData((float)sumValue));
            }

            return endValue.CompareTo(endValue.ZeroRepresentation) < 0 ? endValue.ZeroRepresentation : endValue;
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
                    this.Width,
                    this.Height
                };
        }

        public override string ToString()
        {
            if (isInDebugMode)
            {
                //causes exceptions on large matrixes
                return "";
            }
            
            var maxWhiteSpace = new string(' ', 10);
            var output = new StringBuilder();

            for (var i = 0; i < this.matrix.GetLength(0); i++)
            {
                var matrixRow = this.matrix[i];

                for (int j = 0; j < matrixRow.GetLength(0); j++)
                {
                    var matrixElement = matrixRow[j];
                    var matrixElementToString = matrixElement.ToString();

                    if (matrixElementToString.Length > maxWhiteSpace.Length - 1)
                    {
                        matrixElementToString = matrixElementToString.Substring(0, maxWhiteSpace.Length - 1);
                    }

                    string whitespace = null;

                    if (j == matrixRow.GetLength(0) - 1)
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