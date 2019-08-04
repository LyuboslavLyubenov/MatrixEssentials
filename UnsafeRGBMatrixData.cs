using System;

namespace MatrixEssentials
{
    /// <summary>
    /// Object representing UnsafeRGBMatrixData raw values. Produced from UnsafeRGBMatrixData.RawValue property
    /// </summary>
    public struct UnsafeRGBMatrixDataRawValues
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public UnsafeRGBMatrixDataRawValues(int red, int green, int blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

    }

    /// <summary>
    /// Matrix data object representing color. Its unsafe because it doesnt perform any validation on its color values (r, g, b)
    /// </summary>
    public struct UnsafeRGBMatrixData : IMatrixData
    {
        /// <summary>
        /// Instantiates RGBMatrixData representing pixel color. if lower than zero, will set to zero
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

        public int Blue
        {
            get;
            set;
        }

        public int Green
        {
            get;
            set;
        }

        public int Red
        {
            get;
            set;
        }

        public object RawValue => new UnsafeRGBMatrixDataRawValues(this.Red, this.Green, this.Blue);

        public IMatrixData ZeroRepresentation => new UnsafeRGBMatrixData();

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
            if (obj.GetType() != this.GetType())
            {
                return 0;
            }

            var matrixData = (UnsafeRGBMatrixData)obj;

            if (matrixData.Blue == this.Blue && matrixData.Green == this.Green && matrixData.Red == this.Red)
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