namespace MatrixEssentials
{
    /// <summary>
    /// Abstraction representing data stored in matrix
    /// </summary>
    public interface IMatrixData
    {
        /// <summary>
        /// Raw value of this abstraction. Can be null.
        /// </summary>
        object RawValue { get; }

        /// <summary>
        /// Instance of this type representing 0
        /// </summary>
        IMatrixData ZeroRepresentation { get; }

        /// <summary>
        /// Multiplies by another IMatrixData
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Multiplication result</returns>
        IMatrixData MultiplyBy(IMatrixData value);

        /// <summary>
        /// Adds another IMatrixData to this IMatrixData
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Addition result</returns>
        IMatrixData Add(IMatrixData value);

        /// <summary>
        /// Divides IMatrixData with specified matrix data
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Division result</returns>
        IMatrixData Divide(IMatrixData value);
    }
}