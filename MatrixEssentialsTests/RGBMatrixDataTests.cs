using System;
using MatrixEssentials;
using NUnit.Framework;

namespace MatrixEssentialsTests
{
    public class RGBMatrixDataTests
    {
        [Test]
        public void BlueGreenRedPropertiesRepresentingCorrectValues()
        {
            var black = new UnsafeRGBMatrixData();
            Assert.AreEqual(0, black.Red);
            Assert.AreEqual(0, black.Green);
            Assert.AreEqual(0, black.Blue);

            var randomColor = Utils.GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);
            
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
            var randomColor = new UnsafeRGBMatrixData(randomRed, randomGreen, randomBlue);

            var rawValues = (int[])randomColor.RawValue;
            
            Assert.AreEqual(randomRed, rawValues[0]);
            Assert.AreEqual(randomGreen, rawValues[1]);
            Assert.AreEqual(randomBlue, rawValues[2]);
        }

        [Test]
        public void ZeroRepresentationIsCorrect()
        {
            var zeroRepresentation = (UnsafeRGBMatrixData)new UnsafeRGBMatrixData().ZeroRepresentation;
            
            Assert.AreEqual(0, zeroRepresentation.Red);
            Assert.AreEqual(0, zeroRepresentation.Green);
            Assert.AreEqual(0, zeroRepresentation.Blue);
        }

        [Test]
        public void MultiplyByAddDivideThrowNotImplementedExceptionIfParameterIsNull()
        {
            var randomColor = Utils.GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);

            Assert.Throws<NotImplementedException>(() => { randomColor.MultiplyBy(null); });
            Assert.Throws<NotImplementedException>(() => { randomColor.Add(null); });
            Assert.Throws<NotImplementedException>(() => { randomColor.Divide(null); });
        }
        
        [Test]
        public void ReturningRGBMatrixDataWithColorChannelsMultipliedByFloatIfParameterIsFloatMatrixDataType()
        {
            var randomColor = Utils.GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);
            var randomFloat = new FloatNumberMatrixData((float)new Random().NextDouble());

            var actual = (UnsafeRGBMatrixData)randomColor.MultiplyBy(randomFloat);
            
            Assert.AreEqual((int)Math.Round(randomRed * randomFloat.InternalValue), actual.Red);
            Assert.AreEqual((int)Math.Round(randomGreen * randomFloat.InternalValue), actual.Green);
            Assert.AreEqual((int)Math.Round(randomBlue * randomFloat.InternalValue), actual.Blue);
        }

        [Test]
        public void ReturningRGBMatrixDataWithColorChannelsMultipliedByOtherColorChannelIfParameterIsRGBMatrixDataType()
        {
            var randomColor = Utils.GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue, 10, 10, 10);
            var randomColor2 = Utils.GetRandomColor(out var randomRed2, out var randomGreen2, out var randomBlue2, 10, 10, 10);

            var actual = (UnsafeRGBMatrixData) randomColor.MultiplyBy(randomColor2);
            
            Assert.AreEqual(randomRed * randomRed2, actual.Red);
            Assert.AreEqual(randomGreen * randomGreen2, actual.Green);
            Assert.AreEqual(randomBlue * randomBlue2, actual.Blue);
        }

        [Test]
        public void ReturningRGBMatrixDataWithColorChannelsDividedByFloatIfParameterIsFloatMatrixDataType()
        {
            var randomColor = Utils.GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);
            var randomFloat = new FloatNumberMatrixData(1f + (float)new Random().NextDouble());

            var actual = (UnsafeRGBMatrixData)randomColor.Divide(randomFloat);
            
            Assert.AreEqual((int)Math.Round(randomRed / randomFloat.InternalValue), actual.Red);
            Assert.AreEqual((int)Math.Round(randomGreen / randomFloat.InternalValue), actual.Green);
            Assert.AreEqual((int)Math.Round(randomBlue / randomFloat.InternalValue), actual.Blue);
        }

        [Test]
        public void ReturningRGBMatrixDataWithColorChannellsDividedByEachOtherIfParameterIsRGBMatrixDataType()
        {
            var randomColor = Utils.GetRandomColor(out var randomRed, out var randomGreen, out var randomBlue);
            var randomColor2 = Utils.GetRandomColor(out var randomRed2, out var randomGreen2, out var randomBlue2);

            var actual = (UnsafeRGBMatrixData) randomColor.Divide(randomColor2);
            
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