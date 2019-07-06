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

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                var result = this.InternalValue * integerNumberMatrixData.InternalValue;
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
                var result = this.InternalValue / (float) floatNumberData.InternalValue;
                return new FloatNumberMatrixData(result);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                var result = this.InternalValue / (integerNumberMatrixData.InternalValue + 0f);
                return new FloatNumberMatrixData(result);
            }
            
            if (value is UnsafeRGBMatrixData unsafeRgbMatrixData)
            {
                if (unsafeRgbMatrixData.Blue == unsafeRgbMatrixData.Green &&
                    unsafeRgbMatrixData.Blue == unsafeRgbMatrixData.Red)
                {
                    return new FloatNumberMatrixData(this.InternalValue / unsafeRgbMatrixData.Blue);
                }
                
                var averageOfRGBValues =
                    (unsafeRgbMatrixData.Blue + unsafeRgbMatrixData.Green + unsafeRgbMatrixData.Red) / 3;
                var result = this.InternalValue / averageOfRGBValues;
                return new FloatNumberMatrixData(result);
            }
            
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return this.InternalValue.ToString();
        }

        public int CompareTo(object obj)
        {
            var matrixData = obj as FloatNumberMatrixData;

            if (matrixData == null)
            {
                return 0;
            }

            return this.InternalValue.CompareTo(matrixData.InternalValue);
        }
    }
}