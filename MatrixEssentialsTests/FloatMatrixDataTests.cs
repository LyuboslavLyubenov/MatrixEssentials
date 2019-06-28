using System;
using MatrixEssentials;
using NUnit.Framework;

namespace MatrixEssentialsTests
{
    public class FloatMatrixDataTests
    {
        private void AssertFloatsAreEqual(float first, float second)
        {
            Assert.True(Math.Abs(first - second) < 0.0001f);
        }
        
        [Test]
        public void InternalValueRepresentingCorrectNumber()
        {
            var zeroFloatingNumber = new FloatNumberMatrixData();
            AssertFloatsAreEqual(0f, zeroFloatingNumber.InternalValue);
            
            var randomFloatingNumber = (float)new Random().NextDouble();
            var randomNumberFloatingNumber = new FloatNumberMatrixData(randomFloatingNumber);
            AssertFloatsAreEqual(randomFloatingNumber, randomNumberFloatingNumber.InternalValue);
        }

        [Test]
        public void RawValueIsEqualToInternalValue()
        {
            var zeroFloatingNumber = new FloatNumberMatrixData();
            AssertFloatsAreEqual(zeroFloatingNumber.InternalValue, (float)zeroFloatingNumber.RawValue);

            var randomFloatingNumber = (float)new Random().NextDouble();
            var randomNumberFloatingNumber = new FloatNumberMatrixData(randomFloatingNumber);
            AssertFloatsAreEqual(randomFloatingNumber, (float)randomNumberFloatingNumber.RawValue);
        }

        [Test]
        public void ZeroRepresentationIsCorrect()
        {
            AssertFloatsAreEqual(0f, (float)new FloatNumberMatrixData().ZeroRepresentation.RawValue);
        }

        [Test]
        public void MultiplyByAddDivideThrowNullReferenceExceptionIfParameterIsNull()
        {
            var randomFloat = new FloatNumberMatrixData((float)new Random().NextDouble());

            Assert.Throws<NullReferenceException>(() => { randomFloat.MultiplyBy(null); });
            Assert.Throws<NullReferenceException>(() => { randomFloat.Add(null); });
            Assert.Throws<NullReferenceException>(() => { randomFloat.Divide(null); });
        }
        
        [Test]
        public void ReturningCorrectResultWhenMultiplyByWithFloatNumberMatrixData()
        {
            var random = new Random();
            var randomNumber = (float)random.NextDouble();
            var secondRandomNumber = (float)random.NextDouble();
            
            var first = new FloatNumberMatrixData(randomNumber);
            var second = new FloatNumberMatrixData(secondRandomNumber);

            var multiplicationResult = first.MultiplyBy(second);
            
            AssertFloatsAreEqual(randomNumber * secondRandomNumber, (float)multiplicationResult.RawValue);
        }
        
        [Test]
        public void ThrowingNotImplementedExceptionWhenMultiplyingByUnsupportedDataType()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                new FloatNumberMatrixData().MultiplyBy(new UnknownMatrixDataType()); 
            });
        }
        
        [Test]
        public void ReturningCorrectResultWhenAddWithFloatNumberMatrixData()
        {
            var random = new Random();
            var randomNumber = (float)random.NextDouble();
            var secondRandomNumber = (float)random.NextDouble();
            
            var first = new FloatNumberMatrixData(randomNumber);
            var second = new FloatNumberMatrixData(secondRandomNumber);

            var additionResult = first.Add(second);
            
            AssertFloatsAreEqual(randomNumber + secondRandomNumber, (float)additionResult.RawValue);
        }
        
        [Test]
        public void ThrowingNotImplementedExceptionAddUnsupportedDataType()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                new FloatNumberMatrixData().MultiplyBy(new UnknownMatrixDataType()); 
            });
        }
        
        [Test]
        public void ReturningCorrectResultWhenDivideWithFloatNumberMatrixData()
        {
            var random = new Random();
            var randomNumber = (float)random.NextDouble() + 0.1f;
            var secondRandomNumber = (float)random.NextDouble() + 0.1f;
            
            var first = new FloatNumberMatrixData(randomNumber);
            var second = new FloatNumberMatrixData(secondRandomNumber);

            var divisionResult = first.Divide(second);
            
            AssertFloatsAreEqual(randomNumber / secondRandomNumber, (float)divisionResult.RawValue);
        }

        [Test]
        public void ToStringReturnsFloatingNumberAsString()
        {
            var value = (float) new Random().NextDouble();
            var actual = new FloatNumberMatrixData(value);
            AssertFloatsAreEqual(value, actual.InternalValue);
        }
    }

    class UnknownMatrixDataType : IMatrixData
    {
        public object RawValue { get; }
        public IMatrixData ZeroRepresentation { get; }
        public IMatrixData MultiplyBy(IMatrixData value)
        {
            throw new NotImplementedException();
        }

        public IMatrixData Add(IMatrixData value)
        {
            throw new NotImplementedException();
        }

        public IMatrixData Divide(IMatrixData value)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}