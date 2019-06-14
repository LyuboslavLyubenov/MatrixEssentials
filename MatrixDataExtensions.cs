using System.Collections.Generic;
using System.Reflection;

namespace MatrixEssentials
{
    /// <summary>
    /// Class containing extensions for IMatrixData interface
    /// </summary>
    public static class MatrixDataExtensions
    {
        /// <summary>
        /// Sums all matrix data objects in collection
        /// </summary>
        /// <param name="collection">collection containing matrixdata</param>
        /// <returns>IMatrixData object containing sum of all elements in collection</returns>
        public static IMatrixData Sum(this IList<IMatrixData> collection)
        {
            if (collection.Count == 0)
            {
                var matrixDataType = collection.GetType().GetTypeInfo().GetGenericArguments()[0];
                return matrixDataType.GetZeroValueForMatrixType();
            }

            var endResult = collection[0];

            for (var i = 1; i < collection.Count; i++)
            {
                var matrixData = collection[i];
                endResult = endResult.Add(matrixData);
            }

            return endResult;
        }
    }
}