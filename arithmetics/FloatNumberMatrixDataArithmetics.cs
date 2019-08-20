using System;

namespace MatrixEssentials.Arithmetics
{
    public class FloatNumberMatrixDataArithmetics : IArithmeticsController
    {
        public IMatrixData Add(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                var result = (float)current.RawValue + floatNumberData.InternalValue;
                return new FloatNumberMatrixData(result);
            }

            throw new System.NotImplementedException();
        }

        public IMatrixData Divide(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                var result = (float)current.RawValue / floatNumberData.InternalValue;
                return new FloatNumberMatrixData(result);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                var result = (float)current.RawValue / (integerNumberMatrixData.InternalValue + 0f);
                return new FloatNumberMatrixData(result);
            }

            if (value is UnsafeRGBMatrixData unsafeRgbMatrixData)
            {
                if (unsafeRgbMatrixData.Blue == unsafeRgbMatrixData.Green &&
                    unsafeRgbMatrixData.Blue == unsafeRgbMatrixData.Red)
                {
                    return new FloatNumberMatrixData((float)current.RawValue / unsafeRgbMatrixData.Blue);
                }

                var averageOfRGBValues =
                    (unsafeRgbMatrixData.Blue + unsafeRgbMatrixData.Green + unsafeRgbMatrixData.Red) / 3;
                var result = (float)current.RawValue / averageOfRGBValues;
                return new FloatNumberMatrixData(result);
            }

            throw new System.NotImplementedException();
        }

        public IMatrixData Multiply(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                var result = (float)current.RawValue * (float)floatNumberData.InternalValue;
                return new FloatNumberMatrixData(result);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                var result = (float)current.RawValue * integerNumberMatrixData.InternalValue;
                return new FloatNumberMatrixData(result);
            }

            throw new NotImplementedException();
        }
    }
}
