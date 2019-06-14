using System.Collections.Generic;
using System.Drawing;

namespace MatrixEssentials
{
    public class MatrixUtils
    {
        MatrixUtils()
        {
        }

        /// <summary>
        /// Creates RGBMatrix matrix from image
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns>Matrix containing image data</returns>
        public static IMatrix CreateMatrixFromImage(string imagePath)
        {
            var bitmapImage = new Bitmap(imagePath);
            var width = bitmapImage.Width;
            var height = bitmapImage.Height;
            var matrix = new RGBMatrix(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var pixel = bitmapImage.GetPixel(j, i);
                    matrix.SetValue(j, i, new RGBMatrixData(pixel.R, pixel.G, pixel.B));
                }
            }

            return matrix;
        }

        /// <summary>
        /// Create image from RGBMatrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="outputPath">destination image path</param>
        public static void CreateImageFromMatrix(RGBMatrix matrix, string outputPath)
        {
            var width = matrix.Width;
            var height = matrix.Height;
            var bitmap = new Bitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var pixel = (RGBMatrixData) matrix.GetValue(j, i);
                    bitmap.SetPixel(j, i, Color.FromArgb(pixel.Red, pixel.Green, pixel.Blue));
                }
            }

            bitmap.Save(outputPath);
            bitmap.Dispose();
        }

        public static RGBMatrix ConvertMatrixToRGBMatrix(IMatrix matrixWithRGBData)
        {
            var rgbData = (IList<IList<IMatrixData>>) matrixWithRGBData.RawValues;
            return new RGBMatrix(rgbData);
        }
    }
}