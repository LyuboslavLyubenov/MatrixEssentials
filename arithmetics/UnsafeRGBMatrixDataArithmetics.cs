using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixEssentials.Arithmetics
{
    public class UnsafeRGBMatrixDataArithmetics : IArithmeticsController
    {
        public IMatrixData Add(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.AddFloat((UnsafeRGBMatrixData)current, floatNumberData);
            }

            if (value is UnsafeRGBMatrixData rgbData)
            {
                return this.AddRgb((UnsafeRGBMatrixData)current, rgbData);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return this.AddInteger((UnsafeRGBMatrixData)current, integerNumberMatrixData);
            }

            throw new NotImplementedException();
        }


        /// <summary>
        /// Add float to current UnsafeRGBMatrixData instance
        /// </summary>
        /// <param name="floatData"></param>
        /// <returns>Addition result</returns>
        private UnsafeRGBMatrixData AddFloat(UnsafeRGBMatrixData current, FloatNumberMatrixData floatData)
        {
            var floatDataRaw = floatData.InternalValue;
            var red = current.Red + floatDataRaw;
            var green = current.Green + floatDataRaw;
            var blue = current.Blue + floatDataRaw;
            return new UnsafeRGBMatrixData((int)red, (int)green, (int)blue);
        }

        /// <summary>
        /// Add integer to current UnsafeRGBMatrixData instance
        /// </summary>
        /// <param name="current"></param>
        /// <param name="integerNumberMatrixData"></param>
        /// <returns>addition result</returns>
        private UnsafeRGBMatrixData AddInteger(UnsafeRGBMatrixData current, IntegerNumberMatrixData integerNumberMatrixData)
        {
            var integerValue = integerNumberMatrixData.InternalValue;
            var red = current.Red + integerValue;
            var green = current.Green + integerValue;
            var blue = current.Blue + integerValue;
            return new UnsafeRGBMatrixData(red, green, blue);
        }


        /// <summary>
        /// Add UnsafeRGBMatrixData to current UnsafeRGBMatrixData instance
        /// </summary>
        /// <param name="unsafeRgbData"></param>
        /// <returns>Addition result</returns>
        private UnsafeRGBMatrixData AddRgb(UnsafeRGBMatrixData current, UnsafeRGBMatrixData unsafeRgbData)
        {
            var red = current.Red + unsafeRgbData.Red;
            var green = current.Green + unsafeRgbData.Green;
            var blue = current.Blue + unsafeRgbData.Blue;
            return new UnsafeRGBMatrixData(red, green, blue);
        }


        public IMatrixData Divide(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.DivideFloat((UnsafeRGBMatrixData)current, floatNumberData);
            }

            if (value is UnsafeRGBMatrixData rgbData)
            {
                return this.DivideRgb((UnsafeRGBMatrixData)current, rgbData);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return this.DivideInteger((UnsafeRGBMatrixData)current, integerNumberMatrixData);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Divides current UnsafeRGBMatrixData instance by integer
        /// </summary>
        /// <param name="current"></param>
        /// <param name="integerNumberMatrixData"></param>
        /// <returns></returns>
        private UnsafeRGBMatrixData DivideInteger(UnsafeRGBMatrixData current, IntegerNumberMatrixData integerNumberMatrixData)
        {
            var floatDataRaw = integerNumberMatrixData.InternalValue;
            var red = current.Red / floatDataRaw;
            var green = current.Green / floatDataRaw;
            var blue = current.Blue / floatDataRaw;
            return new UnsafeRGBMatrixData(red, green, blue);
        }


        /// <summary>
        /// Divides current UnsafeRGBMatrixData instance by float
        /// </summary>
        /// <param name="floatData"></param>
        /// <returns>Division result</returns>
        private UnsafeRGBMatrixData DivideFloat(UnsafeRGBMatrixData current, FloatNumberMatrixData floatData)
        {
            var floatDataRaw = floatData.InternalValue;
            var red = current.Red / floatDataRaw;
            var green = current.Green / floatDataRaw;
            var blue = current.Blue / floatDataRaw;
            return new UnsafeRGBMatrixData((int)red, (int)green, (int)blue);
        }

        /// <summary>
        /// Divides current UnsafeRGBMatrixData instance by UnsafeRGBMatrixData
        /// </summary>
        /// <param name="unsafeRgbData"></param>
        /// <returns>Division result</returns>
        private UnsafeRGBMatrixData DivideRgb(UnsafeRGBMatrixData current, UnsafeRGBMatrixData unsafeRgbData)
        {
            var red = current.Red / unsafeRgbData.Red;
            var green = current.Green / unsafeRgbData.Green;
            var blue = current.Blue / unsafeRgbData.Blue;
            return new UnsafeRGBMatrixData(red, green, blue);
        }


        public IMatrixData Multiply(IMatrixData current, IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.MultiplyByFloat((UnsafeRGBMatrixData)current, floatNumberData);
            }

            if (value is UnsafeRGBMatrixData rgbData)
            {
                return this.MultiplyByRgb((UnsafeRGBMatrixData)current, rgbData);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return this.MultiplyByInteger((UnsafeRGBMatrixData)current, integerNumberMatrixData);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Multiplies current UnsafeRGBMatrixData instance by float
        /// </summary>
        /// <param name="floatData"></param>
        /// <returns>Multiplication result</returns>
        private UnsafeRGBMatrixData MultiplyByFloat(UnsafeRGBMatrixData current, FloatNumberMatrixData floatData)
        {
            var floatDataRaw = floatData.InternalValue;
            var red = current.Red * floatDataRaw;
            var green = current.Green * floatDataRaw;
            var blue = current.Blue * floatDataRaw;
            return new UnsafeRGBMatrixData((int)red, (int)green, (int)blue);
        }

        /// <summary>
        /// Multiplies current UnsafeRGBMatrixData instance by UnsafeRGBMatrixData
        /// </summary>
        /// <param name="unsafeRgbData"></param>
        /// <returns>Multiplication result</returns>
        private UnsafeRGBMatrixData MultiplyByRgb(UnsafeRGBMatrixData current, UnsafeRGBMatrixData unsafeRgbData)
        {
            var red = current.Red * unsafeRgbData.Red;
            var green = current.Green * unsafeRgbData.Green;
            var blue = current.Blue * unsafeRgbData.Blue;
            return new UnsafeRGBMatrixData(red, green, blue);
        }

        /// <summary>
        /// Multiplies current UnsafeRGBMatrixData instance by integer
        /// </summary>
        /// <param name="current"></param>
        /// <param name="integerNumberMatrixData"></param>
        /// <returns></returns>
        private UnsafeRGBMatrixData MultiplyByInteger(UnsafeRGBMatrixData current, IntegerNumberMatrixData integerNumberMatrixData)
        {
            var red = current.Red * integerNumberMatrixData.InternalValue;
            var green = current.Green * integerNumberMatrixData.InternalValue;
            var blue = current.Blue * integerNumberMatrixData.InternalValue;
            return new UnsafeRGBMatrixData(red, green, blue);
        }
    }
}
