using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixEssentials.Arithmetics
{
    /// <summary>
    /// Controlls how struct implementing IMatrixData should do basic arithmetic operations to another structs implementing IMatrixData
    /// </summary>
    public interface IArithmeticsController
    {
        /// <summary>
        /// Defines how it should add another IMatrixData to itself
        /// </summary>
        /// <param name="current">current matrix data</param>
        /// <param name="value"></param>
        /// <returns>result from the addition</returns>
        IMatrixData Add(IMatrixData current, IMatrixData value);

        /// <summary>
        /// Defines how it should multiply another IMatrixData to itself
        /// </summary>
        /// <param name="current">current matrix data</param>
        /// <param name="value"></param>
        /// <returns>result from the multiplication</returns>
        IMatrixData Multiply(IMatrixData current, IMatrixData value);

        /// <summary>
        /// Defines how it should divide another IMatrixData to itself
        /// </summary>
        /// <param name="current">current matrix data</param>
        /// <param name="value"></param>
        /// <returns>result from the division</returns>
        IMatrixData Divide(IMatrixData current, IMatrixData value);
    }
}
