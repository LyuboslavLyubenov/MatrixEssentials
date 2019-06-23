using System;
using MatrixEssentials;
using NUnit.Framework;

namespace MatrixEssentialsTests
{
    public class RGBMatrixDataTests
    {
        private RGBMatrixData GetRandomColor(out int red, out int green, out int blue, int redLimit = 255, int greenLimit = 255, int blueLimit = 255)
        {
            var randomRed = new Random().Next(0, redLimit);
            var randomGreen = new Random().Next(0, greenLimit);
            var randomBlue = new Random().Next(0, blueLimit);
            var randomColor = new RGBMatrixData(randomRed, randomGreen, randomBlue);

            red = randomRed;
            green = randomGreen;
            blue = randomBlue;
            
            return randomColor;
        }
        
        [Test]
        public void BlueGreenRedPropertiesRepresentingCorrectValues()
        {
            var black = new RGBMatrixData();
            Assert.AreEqual(0, black.Red);
            Assert.AreEqual(0, black.Green);
            Assert.AreEqual(0, black.Blue);

            var randomColor = GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);
            
            Assert.AreEqual(randomRed, randomColor.Red);
            Assert.AreEqual(randomGreen, randomColor.Green);
            Assert.AreEqual(randomBlue, randomColor.Blue);
        }

        [Test]
        public void RawValuesIsValidIntegerArray()
        {
            var randomRed = new Random().Next(0, 255);
            var randomGreen = new Random().Next(0, 255);
            var randomBlue = new Random().Next(0, 255);
            var randomColor = new RGBMatrixData(randomRed, randomGreen, randomBlue);

            var rawValues = (int[])randomColor.RawValue;
            
            Assert.AreEqual(randomRed, rawValues[0]);
            Assert.AreEqual(randomGreen, rawValues[1]);
            Assert.AreEqual(randomBlue, rawValues[2]);
        }

        [Test]
        public void ZeroRepresentationIsCorrect()
        {
            var zeroRepresentation = (RGBMatrixData)new RGBMatrixData().ZeroRepresentation;
            
            Assert.AreEqual(0, zeroRepresentation.Red);
            Assert.AreEqual(0, zeroRepresentation.Green);
            Assert.AreEqual(0, zeroRepresentation.Blue);
        }

        [Test]
        public void MultiplyByAddDivideThrowNullReferenceExceptionIfParameterIsNull()
        {
            var randomColor = GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);

            Assert.Throws<NullReferenceException>(() => { randomColor.MultiplyBy(null); });
            Assert.Throws<NullReferenceException>(() => { randomColor.Add(null); });
            Assert.Throws<NullReferenceException>(() => { randomColor.Divide(null); });
        }
        
        [Test]
        public void ReturningRGBMatrixDataWithColorChannelsMultipliedByFloatIfParameterIsFloatMatrixDataType()
        {
            var randomColor = GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);
            var randomFloat = new FloatNumberMatrixData((float)new Random().NextDouble());

            var actual = (RGBMatrixData)randomColor.MultiplyBy(randomFloat);
            
            Assert.AreEqual((int)Math.Round(randomRed * randomFloat.InternalValue), actual.Red);
            Assert.AreEqual((int)Math.Round(randomGreen * randomFloat.InternalValue), actual.Green);
            Assert.AreEqual((int)Math.Round(randomBlue * randomFloat.InternalValue), actual.Blue);
        }

        [Test]
        public void ReturningRGBMatrixDataWithColorChannelsMultipliedByOtherColorChannelIfParameterIsRGBMatrixDataType()
        {
            var randomColor = GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue, 10, 10, 10);
            var randomColor2 = GetRandomColor(out var randomRed2, out var randomGreen2, out var randomBlue2, 10, 10, 10);

            var actual = (RGBMatrixData) randomColor.MultiplyBy(randomColor2);
            
            Assert.AreEqual(randomRed * randomRed2, actual.Red);
            Assert.AreEqual(randomGreen * randomGreen2, actual.Green);
            Assert.AreEqual(randomBlue * randomBlue2, actual.Blue);
        }

        [Test]
        public void ReturningRGBMatrixDataWithColorChannelsDividedByFloatIfParameterIsFloatMatrixDataType()
        {
            var randomColor = GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);
            var randomFloat = new FloatNumberMatrixData(1f + (float)new Random().NextDouble());

            var actual = (RGBMatrixData)randomColor.Divide(randomFloat);
            
            Assert.AreEqual((int)Math.Round(randomRed / randomFloat.InternalValue), actual.Red);
            Assert.AreEqual((int)Math.Round(randomGreen / randomFloat.InternalValue), actual.Green);
            Assert.AreEqual((int)Math.Round(randomBlue / randomFloat.InternalValue), actual.Blue);
        }

        [Test]
        public void ReturningRGBMatrixDataWithColorChannellsDividedByEachOtherIfParameterIsRGBMatrixDataType()
        {
            var randomColor = GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);
            var randomColor2 = GetRandomColor(out var randomRed2, out var randomGreen2, out var randomBlue2);

            var actual = (RGBMatrixData) randomColor.Divide(randomColor2);
            
            Assert.AreEqual(randomRed / randomRed2, actual.Red);
            Assert.AreEqual(randomGreen / randomGreen2, actual.Green);
            Assert.AreEqual(randomBlue / randomBlue2, actual.Blue);
        }
        
        
        [Test]
        public void ThrowingNotImplementedExceptionAddUnsupportedDataType()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                new FloatNumberMatrixData().MultiplyBy(new UnknownMatrixDataType()); 
            });
        }
    }
}