using System;
using System.Collections.Generic;

namespace MatrixEssentials
{
    /// <summary>
    /// Matrix containing RGB (color) values
    /// </summary>
    public class RGBMatrix : Matrix
    {
        public RGBMatrix(IList<IList<IMatrixData>> matrix) : base(matrix)
        {
        }

        public RGBMatrix(int width, int height) : base(width, height, typeof(RGBMatrixData))
        {
        }
    }
}