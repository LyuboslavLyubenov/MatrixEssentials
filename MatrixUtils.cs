using SkiaSharp;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

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
            var bitmapImage = SKBitmap.Decode(imagePath);
            var width = bitmapImage.Width;
            var height = bitmapImage.Height;
            IMatrixData[][] rgbValues = new IMatrixData[height][];

            for (int i = 0; i < height; i++)
            {
                rgbValues[i] = new IMatrixData[width];
                
                for (int j = 0; j < width; j++)
                {
                    var pixel = bitmapImage.GetPixel(j, i);
                    rgbValues[i][j] = new UnsafeRGBMatrixData(pixel.Red, pixel.Green, pixel.Blue);
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
            var bitmap = new SKBitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var pixel = (UnsafeRGBMatrixData) matrix.GetValue(j, i);
                    var safeRedValue = ConvertColorValueToSafeValue(pixel.Red);
                    var safeGreenValue = ConvertColorValueToSafeValue(pixel.Green);
                    var safeBlueValue = ConvertColorValueToSafeValue(pixel.Blue);
                    var color = new SKColor((byte)safeRedValue, (byte)safeGreenValue, (byte)safeBlueValue);
                    bitmap.SetPixel(j, i, color);
                }
            }

            var bitmapData = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Jpeg, 80);
            using (var stream = File.OpenWrite(outputPath))
            {
                bitmapData.SaveTo(stream);
            }
        }

        public static void CreateImageFromMatrixParalleled(RGBMatrix matrix, string outputPath)
        {
            var width = matrix.Width;
            var height = matrix.Height;
            var bitmap = new SKBitmap(width, height);

            Parallel.For(0, height, (int i) =>
            {
                for (int j = 0; j < width; j++)
                {
                    var pixel = (UnsafeRGBMatrixData) matrix.GetValue(j, i);
                    var safeRedValue = ConvertColorValueToSafeValue(pixel.Red);
                    var safeGreenValue = ConvertColorValueToSafeValue(pixel.Green);
                    var safeBlueValue = ConvertColorValueToSafeValue(pixel.Blue);
                    var color = new SKColor((byte)safeRedValue, (byte)safeGreenValue, (byte)safeBlueValue);
                    bitmap.SetPixel(j, i, color);
                }
            });

            var bitmapData = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Jpeg, 80);
            using (var stream = File.OpenWrite(outputPath))
            {
                bitmapData.SaveTo(stream);
            }
        }
        
        private static int ConvertColorValueToSafeValue(int colorValue)
        {
            if (colorValue > 255)
            {
                return 255;
            }

            if (colorValue < 0)
            {
                return 0;
            }

            return colorValue;
        }

        public static RGBMatrix ConvertMatrixToRGBMatrix(IMatrix matrixWithRGBData)
        {
            var rgbData = (IMatrixData[][]) matrixWithRGBData.RawValues;
            return new RGBMatrix(rgbData);
        }
    }
}