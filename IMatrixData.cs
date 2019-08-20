using System;

namespace MatrixEssentials
{
    /// <summary>
    /// Abstraction representing data stored in matrix
    /// </summary>
    public interface IMatrixData : IComparable
    {
        /// <summary>
        /// Raw value of this abstraction. Can be null.
        /// </summary>
        object RawValue { get; }

        /// <summary>
        /// Instance of this type representing 0
        /// </summary>
        IMatrixData ZeroRepresentation { get; }
    }
}