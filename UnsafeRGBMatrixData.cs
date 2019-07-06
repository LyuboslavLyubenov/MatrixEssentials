using System;

namespace MatrixEssentials
{
    /// <summary>
    /// Matrix data object representing color. Its unsafe because it doesnt perform any validation on its color values (r, g, b)
    /// </summary>
    public class UnsafeRGBMatrixData : IMatrixData
    {
        /// <summary>
        /// Instantiates black RGBMatrixData
        /// </summary>
        public UnsafeRGBMatrixData() : this(0, 0, 0)
        {
        }

        /// <summary>
        /// Instantiates RGBMatrixData representing pixel color
        /// </summary>
        /// <param name="red">red value (from 0 to 255)</param>
        /// <param name="green">green value (from 0 to 255)</param>
        /// <param name="blue">blue value (from 0 to 255)</param>
        public UnsafeRGBMatrixData(int red, int green, int blue)
        {
            this.Green = green;
            this.Blue = blue;
            this.Red = red;
        }

        public int Blue { get; }
        public int Green { get; }
        public int Red { get; }

        public object RawValue => new[] {this.Red, this.Green, this.Blue};

        public IMatrixData ZeroRepresentation => new UnsafeRGBMatrixData();

        public IMatrixData MultiplyBy(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.MultiplyByFloat(floatNumberData);
            }
            
            if (value is UnsafeRGBMatrixData rgbData)
            {
                return this.MultiplyByRgb(rgbData);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return this.MultiplyByInteger(integerNumberMatrixData);
            }

            throw new NotImplementedException();
        }

        private IMatrixData MultiplyByInteger(IntegerNumberMatrixData integerNumberMatrixData)
        {
            var red = this.Red * integerNumberMatrixData.InternalValue;
            var green = this.Green * integerNumberMatrixData.InternalValue;
            var blue = this.Blue * integerNumberMatrixData.InternalValue;
            return new UnsafeRGBMatrixData(red, green, blue);
        }

        public IMatrixData Add(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.AddFloat(floatNumberData);
            }
            
            if (value is UnsafeRGBMatrixData rgbData)
            {
                return this.AddRgb(rgbData);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return this.AddInteger(integerNumberMatrixData);
            }
            
            throw new NotImplementedException();
        }

        private IMatrixData AddInteger(IntegerNumberMatrixData integerNumberMatrixData)
        {
            var integerValue = integerNumberMatrixData.InternalValue;
            var red = (int) Math.Round(this.Red + (double)integerValue);
            var green = (int) Math.Round(this.Green + (double)integerValue);
            var blue = (int) Math.Round(this.Blue + (double)integerValue);

            red = Math.Min(red, 255);
            green = Math.Min(green, 255);
            blue = Math.Min(blue, 255);

            return new UnsafeRGBMatrixData(red, green, blue);
        }
        
        /// <summary>
        /// Add float to this instance
        /// </summary>
        /// <param name="floatData"></param>
        /// <returns>Multiplication result</returns>
        private IMatrixData AddFloat(FloatNumberMatrixData floatData)
        {
            var floatDataRaw = (float) floatData.RawValue;
            var red = (int) Math.Round(this.Red + floatDataRaw);
            var green = (int) Math.Round(this.Green + floatDataRaw);
            var blue = (int) Math.Round(this.Blue + floatDataRaw);

            red = Math.Min(red, 255);
            green = Math.Min(green, 255);
            blue = Math.Min(blue, 255);

            return new UnsafeRGBMatrixData(red, green, blue);
        }

        /// <summary>
        /// Add RGBMatrixData to this instance
        /// </summary>
        /// <param name="unsafeRgbData"></param>
        /// <returns>Addition result</returns>
        private IMatrixData AddRgb(UnsafeRGBMatrixData unsafeRgbData)
        {
            var rgbRawData = (int[]) unsafeRgbData.RawValue;
            return new UnsafeRGBMatrixData(this.Red + rgbRawData[0], this.Green + rgbRawData[1], this.Blue + rgbRawData[2]);
        }

        public IMatrixData Divide(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.DivideFloat(floatNumberData);
            }
            
            if (value is UnsafeRGBMatrixData rgbData)
            {
                return this.DivideRgb(rgbData);
            }

            if (value is IntegerNumberMatrixData integerNumberMatrixData)
            {
                return this.DivideInteger(integerNumberMatrixData);
            }
            
            throw new NotImplementedException();
        }

        private IMatrixData DivideInteger(IntegerNumberMatrixData integerNumberMatrixData)
        {
            var floatDataRaw = integerNumberMatrixData.InternalValue;
            var red = this.Red / floatDataRaw;
            var green = this.Green / floatDataRaw;
            var blue = this.Blue / floatDataRaw;
            return new UnsafeRGBMatrixData(red, green, blue);
        }

        private void ValidateColorRange(int colorValue)
        {
            if (colorValue < 0 || colorValue > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(colorValue));
            }
        }

        /// <summary>
        /// Multiplies this instance by float
        /// </summary>
        /// <param name="floatData"></param>
        /// <returns>Multiplication result</returns>
        private IMatrixData MultiplyByFloat(FloatNumberMatrixData floatData)
        {
            var floatDataRaw = (float) floatData.RawValue;
            var red = (int) Math.Round(this.Red * floatDataRaw);
            var green = (int) Math.Round(this.Green * floatDataRaw);
            var blue = (int) Math.Round(this.Blue * floatDataRaw);
            return new UnsafeRGBMatrixData(red, green, blue);
        }

        /// <summary>
        /// Multiplies this instance by RGBMatrixData
        /// </summary>
        /// <param name="unsafeRgbData"></param>
        /// <returns>Multiplication result</returns>
        private IMatrixData MultiplyByRgb(UnsafeRGBMatrixData unsafeRgbData)
        {
            var rgbRawData = (int[]) unsafeRgbData.RawValue;
            return new UnsafeRGBMatrixData(this.Red * rgbRawData[0], this.Green * rgbRawData[1], this.Blue * rgbRawData[2]);
        }

        /// <summary>
        /// Divides by float
        /// </summary>
        /// <param name="floatData"></param>
        /// <returns>Division result</returns>
        private IMatrixData DivideFloat(FloatNumberMatrixData floatData)
        {
            var floatDataRaw = (float) floatData.RawValue;
            var red = (int) Math.Round(this.Red / floatDataRaw);
            var green = (int) Math.Round(this.Green / floatDataRaw);
            var blue = (int) Math.Round(this.Blue / floatDataRaw);
            return new UnsafeRGBMatrixData(red, green, blue);
        }

        /// <summary>
        /// Divides by RGBMatrixData
        /// </summary>
        /// <param name="unsafeRgbData"></param>
        /// <returns>Division result</returns>
        private IMatrixData DivideRgb(UnsafeRGBMatrixData unsafeRgbData)
        {
            var rgbRawData = (int[]) unsafeRgbData.RawValue;
            return new UnsafeRGBMatrixData(this.Red / rgbRawData[0], this.Green / rgbRawData[1], this.Blue / rgbRawData[2]);
        }

        public override string ToString()
        {
            return $"r:{this.Red} g:{this.Green} b:{this.Blue}";
        }

        /// <summary>
        /// Compares this instance to another object. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// It have strange behaviour.
        /// It will return 0 when not comparing with RGBMatrixData. Nothing very strange so far.
        /// Weird things happen when is trying to compare with another RGBMatrixData.
        /// It will return -1 if any of this instance's color values are less than comparable's color values.
        ///
        /// For example:
        /// (1, 100, 1) is less than (2, 0, 0)
        /// (2, 100, 1) is less than (0, 0, 2)
        ///
        /// It will return 0 if its color values are the same.
        ///
        /// And it will return 1 if all its values are greater than comparable's
        /// For example:
        /// (1, 100, 1) is greater than (0, 99, 0)
        /// But not greater than (2, 0, 0)
        /// </returns>
        public int CompareTo(object obj)
        {
            var matrixData = obj as UnsafeRGBMatrixData;

            if (matrixData == null || (matrixData.Blue == this.Blue && matrixData.Green == this.Green && matrixData.Red == this.Red))
            {
                return 0;
            }

            if (this.Red < matrixData.Red || this.Green < matrixData.Green || this.Blue < matrixData.Blue)
            {
                return -1;
            }

            return 1;
        }
    }
}