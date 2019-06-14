namespace MatrixEssentials
{
    public interface IMatrix
    {
        object RawValues { get; }

        /// <summary>
        /// Width of the matrix
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Height of the matrix
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Calculates sum of the matrix
        /// </summary>
        IMatrixData Sum { get; }

        /// <summary>
        /// Gets value from certain position
        /// </summary>
        /// <param name="column">column position</param>
        /// <param name="row">row position</param>
        /// <returns>value c</returns>
        /// <exception cref="ArgumentOutOfRangeException">when x or y are pointing to elements outside of the matrix</exception>
        IMatrixData GetValue(int column, int row);

        /// <summary>
        /// Sets pixel value
        /// </summary>
        /// <param name="column">column position</param>
        /// <param name="row">row position</param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException">when x or y are pointing to elements outside of the matrix</exception>
        void SetValue(int column, int row, IMatrixData value);

        /// <summary>
        /// Convolutes by kernel
        /// </summary>
        /// <param name="kernel"></param>
        /// <returns>result from multiplication</returns>
        IMatrix Convolute(IMatrix kernel);
    }
}