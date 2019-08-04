using System;

namespace MatrixEssentials
{
    /// <summary>
    /// MatrixData representing float number
    /// </summary>
    public struct FloatNumberMatrixData : IMatrixData
    {
        public FloatNumberMatrixData(float internalValue)
        {
            this.InternalValue = internalValue;
        }

        public float InternalValue { get; }

        public object RawValue => this.InternalValue;

        public IMatrixData ZeroRepresentation => new FloatNumberMatrixData(0);

        public override string ToString()
        {
            return this.InternalValue.ToString();
        }

        public int CompareTo(object obj)
        {
            var matrixDataType = obj.GetType();

            if (matrixDataType != typeof(FloatNumberMatrixData))
            {
                return 0;
            }

            var matrixData = (FloatNumberMatrixData)obj;
            return this.InternalValue.CompareTo(matrixData.InternalValue);
        }
    }
}