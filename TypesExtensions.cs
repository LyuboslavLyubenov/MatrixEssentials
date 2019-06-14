using System;

namespace MatrixEssentials
{
    public static class TypesExtensions
    {
        /// <summary>
        /// Gets 0 representation for specific IMatrixData type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IMatrixData GetZeroValueForMatrixType(this Type type)
        {
            if (!type.IsClass || type.IsAbstract || type.IsAssignableFrom(typeof(IMatrixData)))
            {
                throw new ArgumentException();
            }

            var emptyMatrixData = (IMatrixData) Activator.CreateInstance(type);
            return emptyMatrixData.ZeroRepresentation;
        }
    }
}