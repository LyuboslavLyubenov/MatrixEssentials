using System;
using MatrixEssentials;

namespace MatrixEssentialsTests
{
    public class Utils
    {
        Utils()
        {    
        }
        
        public static UnsafeRGBMatrixData GetRandomColor(out int red, out int green, out int blue, int redLimit = 255, int greenLimit = 255, int blueLimit = 255)
        {
            var randomRed = new Random().Next(0, redLimit);
            var randomGreen = new Random().Next(0, greenLimit);
            var randomBlue = new Random().Next(0, blueLimit);
            var randomColor = new UnsafeRGBMatrixData(randomRed, randomGreen, randomBlue);

            red = randomRed;
            green = randomGreen;
            blue = randomBlue;
            
            return randomColor;
        }
    }
}