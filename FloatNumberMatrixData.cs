using System;

namespace MatrixEssentials
{
    /// <summary>
    /// MatrixData representing float number
    /// </summary>
    public class FloatNumberMatrixData : IMatrixData
    {
        public FloatNumberMatrixData()
        {
        }

        public FloatNumberMatrixData(float internalValue)
        {
            this.InternalValue = internalValue;
        }

        public float InternalValue { get; }

        public object RawValue => this.InternalValue;

        public IMatrixData ZeroRepresentation => new FloatNumberMatrixData(0);

        public IMatrixData MultiplyBy(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                var result = this.InternalValue * (float) floatNumberData.InternalValue;
                return new FloatNumberMatrixData(result);
            }

            throw new NotImplementedException();
        }

        public IMatrixData Add(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                var result = this.InternalValue + (float) floatNumberData.InternalValue;
                return new FloatNumberMatrixData(result);
            }

            throw new System.NotImplementedException();
        }

        public IMatrixData Divide(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                var result = this.InternalValue * (float) floatNumberData.InternalValue;
                return new FloatNumberMatrixData(result);
            }

            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return this.InternalValue.ToString();
        }
    }
}