using System;

namespace MatrixEssentials
{
    /// <summary>
    /// Matrix data object representing color
    /// </summary>
    public class RGBMatrixData : IMatrixData
    {
        /// <summary>
        /// Instantiates black RGBMatrixData
        /// </summary>
        public RGBMatrixData() : this(0, 0, 0)
        {
        }

        /// <summary>
        /// Instantiates RGBMatrixData representing pixel color
        /// </summary>
        /// <param name="red">red value (from 0 to 255)</param>
        /// <param name="green">green value (from 0 to 255)</param>
        /// <param name="blue">blue value (from 0 to 255)</param>
        public RGBMatrixData(int red, int green, int blue)
        {
            ValidateColorRange(green);
            ValidateColorRange(blue);
            ValidateColorRange(red);

            this.Green = green;
            this.Blue = blue;
            this.Red = red;
        }

        public int Blue { get; }
        public int Green { get; }
        public int Red { get; }

        public object RawValue => new[] {this.Red, this.Green, this.Blue};

        public IMatrixData ZeroRepresentation => new RGBMatrixData();

        public IMatrixData MultiplyBy(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.MultiplyByFloat(floatNumberData);
            }
            else if (value is RGBMatrixData rgbData)
            {
                return this.MultiplyByRgb(rgbData);
            }

            throw new NotImplementedException();
        }

        public IMatrixData Add(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.AddFloat(floatNumberData);
            }
            else if (value is RGBMatrixData rgbData)
            {
                return this.AddRgb(rgbData);
            }

            throw new NotImplementedException();
        }

        public IMatrixData Divide(IMatrixData value)
        {
            if (value is FloatNumberMatrixData floatNumberData)
            {
                return this.DivideFloat(floatNumberData);
            }
            else if (value is RGBMatrixData rgbData)
            {
                return this.DivideRgb(rgbData);
            }

            throw new NotImplementedException();
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
            return new RGBMatrixData(red, green, blue);
        }

        /// <summary>
        /// Multiplies this instance by RGBMatrixData
        /// </summary>
        /// <param name="rgbData"></param>
        /// <returns>Multiplication result</returns>
        private IMatrixData MultiplyByRgb(RGBMatrixData rgbData)
        {
            var rgbRawData = (int[]) rgbData.RawValue;
            return new RGBMatrixData(this.Red * rgbRawData[0], this.Green * rgbRawData[1], this.Blue * rgbRawData[2]);
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

            return new RGBMatrixData(red, green, blue);
        }

        /// <summary>
        /// Add RGBMatrixData to this instance
        /// </summary>
        /// <param name="rgbData"></param>
        /// <returns>Addition result</returns>
        private IMatrixData AddRgb(RGBMatrixData rgbData)
        {
            var rgbRawData = (int[]) rgbData.RawValue;
            return new RGBMatrixData(this.Red + rgbRawData[0], this.Green + rgbRawData[1], this.Blue + rgbRawData[2]);
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
            return new RGBMatrixData(red, green, blue);
        }

        /// <summary>
        /// Divides by RGBMatrixData
        /// </summary>
        /// <param name="rgbData"></param>
        /// <returns>Division result</returns>
        private IMatrixData DivideRgb(RGBMatrixData rgbData)
        {
            var rgbRawData = (int[]) rgbData.RawValue;
            return new RGBMatrixData(this.Red / rgbRawData[0], this.Green / rgbRawData[1], this.Blue / rgbRawData[2]);
        }

        public override string ToString()
        {
            return $"r:{this.Red} g:{this.Green} b:{this.Blue}";
        }
    }
}