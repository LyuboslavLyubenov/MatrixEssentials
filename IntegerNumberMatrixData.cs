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