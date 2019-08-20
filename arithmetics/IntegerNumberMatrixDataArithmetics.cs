using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixEssentials.Arithmetics
{
    public class IntegerNumberMatrixDataArithmetics : IArithmeticsController
    {
        public IMatrixData Add(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberMatrixData)
            {
                return new IntegerNumberMatrixData((int)current.RawValue + (int)floatNumberMatrixData.InternalValue);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return new IntegerNumberMatrixData((int)current.RawValue + integerNumberMatrixData.InternalValue);
            }

            throw new NotImplementedException();
        }

        public IMatrixData Divide(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberMatrixData)
            {
                return new IntegerNumberMatrixData((int)current.RawValue / (int)floatNumberMatrixData.InternalValue);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return new IntegerNumberMatrixData((int)current.RawValue / integerNumberMatrixData.InternalValue);
            }

            if (value is UnsafeRGBMatrixData unsafeRgbMatrixData)
            {
                if (unsafeRgbMatrixData.Blue == unsafeRgbMatrixData.Green &&
                    unsafeRgbMatrixData.Blue == unsafeRgbMatrixData.Red)
                {
                    return new IntegerNumberMatrixData((int)current.RawValue / unsafeRgbMatrixData.Blue);
                }

                var averageOfRGBValues =
                    (unsafeRgbMatrixData.Blue + unsafeRgbMatrixData.Green + unsafeRgbMatrixData.Red) / 3;
                var result = (int)current.RawValue / averageOfRGBValues;
                return new IntegerNumberMatrixData(result);
            }

            throw new NotImplementedException();
        }

        public IMatrixData Multiply(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberMatrixData)
            {
                return new IntegerNumberMatrixData((int)current.RawValue * (int)floatNumberMatrixData.InternalValue);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return new IntegerNumberMatrixData((int)current.RawValue * integerNumberMatrixData.InternalValue);
            }

            throw new NotImplementedException();
        }
    }
}
