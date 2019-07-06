using System;

namespace MatrixEssentials
{
    public class IntegerNumberMatrixData : IMatrixData
    {
        public object RawValue => this.InternalValue;
        
        public IMatrixData ZeroRepresentation => new IntegerNumberMatrixData();

        public int InternalValue { get; }

        public IntegerNumberMatrixData() : this(0)
        {
        }
        
        public IntegerNumberMatrixData(int value)
        {
            this.InternalValue = value;
        }

        public IMatrixData MultiplyBy(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberMatrixData)
            {
                return new IntegerNumberMatrixData(this.InternalValue * (int)floatNumberMatrixData.InternalValue);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return new IntegerNumberMatrixData(this.InternalValue * integerNumberMatrixData.InternalValue);
            }
            
            throw new NotImplementedException();
        }

        public IMatrixData Add(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberMatrixData)
            {
                return new IntegerNumberMatrixData(this.InternalValue + (int)floatNumberMatrixData.InternalValue);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return new IntegerNumberMatrixData(this.InternalValue + integerNumberMatrixData.InternalValue);
            }
            
            throw new NotImplementedException();
        }

        public IMatrixData Divide(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberMatrixData)
            {
                return new IntegerNumberMatrixData(this.InternalValue / (int)floatNumberMatrixData.InternalValue);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return new IntegerNumberMatrixData(this.InternalValue / integerNumberMatrixData.InternalValue);
            }
            
            if (value is UnsafeRGBMatrixData unsafeRgbMatrixData)
            {
                if (unsafeRgbMatrixData.Blue == unsafeRgbMatrixData.Green &&
                    unsafeRgbMatrixData.Blue == unsafeRgbMatrixData.Red)
                {
                    return new IntegerNumberMatrixData(this.InternalValue / unsafeRgbMatrixData.Blue);
                }
                
                var averageOfRGBValues =
                    (unsafeRgbMatrixData.Blue + unsafeRgbMatrixData.Green + unsafeRgbMatrixData.Red) / 3;
                var result = this.InternalValue / averageOfRGBValues;
                return new IntegerNumberMatrixData(result);
            }
            
            throw new NotImplementedException();
        }
        
        public int CompareTo(object obj)
        {
            var anotherMatrixData = obj as IMatrixData;

            if (anotherMatrixData is FloatNumberMatrixData floatNumberMatrixData)
            {
                return this.InternalValue.CompareTo(floatNumberMatrixData.InternalValue);
            }

            if (anotherMatrixData is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return this.InternalValue.CompareTo(integerNumberMatrixData.InternalValue);
            }
            
            throw new NotImplementedException();
        }
    }
}