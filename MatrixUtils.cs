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
            IMatrixData[][] rgbValues = new IMatrixData[height][];

            for (int i = 0; i < height; i++)
            {
                rgbValues[i] = new IMatrixData[width];
                
                for (int j = 0; j < width; j++)
                {
                    var pixel = bitmapImage.GetPixel(j, i);
                    rgbValues[i][j] = new UnsafeRGBMatrixData(pixel.R, pixel.G, pixel.B);
                }
            }

            var matrix = new RGBMatrix(rgbValues);
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
                    var pixel = (UnsafeRGBMatrixData) matrix.GetValue(j, i);
                    bitmap.SetPixel(j, i, Color.FromArgb(pixel.Red, pixel.Green, pixel.Blue));
                }
            }

            bitmap.Save(outputPath);
            bitmap.Dispose();
        }

        public static RGBMatrix ConvertMatrixToRGBMatrix(IMatrix matrixWithRGBData)
        {
            var rgbData = (IMatrixData[][]) matrixWithRGBData.RawValues;
            return new RGBMatrix(rgbData);
        }
    }
}