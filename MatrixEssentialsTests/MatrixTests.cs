using System;
using System.Collections.Generic;
using MatrixEssentials;
using NUnit.Framework;

namespace MatrixEssentialsTests
{
    public class MatrixTests
    {
        [Test]
        public void CantCreateMatrixWhenPassedNullInConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Matrix(null);
            });
        }

        [Test]
        public void CantCreateEmptyMatrix()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Matrix(new IMatrixData[0][]);
            });
        }

        [Test]
        public void CantCreateWithDifferentMatrixData()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var asd = new IMatrixData[1][]
                {
                    new IMatrixData[1]
                    {
                        new AnotherMatrixData()
                    }
                };
            });
        }

        [Test]
        public void SuccessfullyCreatingEmptyMatrix()
        {
            new Matrix(10, 10, typeof(AnotherMatrixData));
            new Matrix(new IMatrixData[1][] { new IMatrixData[1] { new AnotherMatrixData() }});
            Assert.Pass();
        }
        
        private class AnotherMatrixData : IMatrixData
        {
            public int CompareTo(object obj)
            {
                throw new System.NotImplementedException();
            }

            public object RawValue { get; }
            public IMatrixData ZeroRepresentation { get; }
            public IMatrixData MultiplyBy(IMatrixData value)
            {
                throw new System.NotImplementedException();
            }

            public IMatrixData Add(IMatrixData value)
            {
                throw new System.NotImplementedException();
            }

            public IMatrixData Subtract(IMatrixData value)
            {
                throw new NotImplementedException();
            }

            public IMatrixData Divide(IMatrixData value)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}