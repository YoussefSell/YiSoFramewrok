namespace YiSoFramework
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;

    public static class Helper
    {
        /// <summary>
        /// convert a string to a float
        /// </summary>
        /// <param name="val">string to convert</param>
        /// <returns>if parse is failed -1 will be returned</returns>
        public static float ToFloat(this string val)
        {
            var res = -1f;
            float.TryParse(val, out res);
            return res;
        }

        /// <summary>
        /// convert a string to a int
        /// </summary>
        /// <param name="val">string to convert</param>
        /// <returns>if parse is failed -1 will be returned</returns>
        public static int ToInt(this string val)
        {
            var res = -1;
            int.TryParse(val, out res);
            return res;
        }

        /// <summary>
        /// convert a YiSoFontSize enum to a float
        /// </summary>
        /// <param name="val">enum value to convert</param>
        /// <returns>the corresponded floating point</returns>
        public static float ToFloat(this YiSoFontSize val)
        {
            float result = 12;

            switch (val)
            {
                case YiSoFontSize.Size12: result = 12; break;
                case YiSoFontSize.Size18: result = 18; break;
                case YiSoFontSize.Size24: result = 24; break;
                case YiSoFontSize.Size36: result = 36; break;
                case YiSoFontSize.Size48: result = 48; break;
                case YiSoFontSize.Size60: result = 60; break;
                case YiSoFontSize.Size72: result = 72; break;
            }

            return result;
        }

        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source) where T : class
        {
            if (!typeof(T).IsSerializable)
            {
                throw new System.ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (source is null)
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// check if the color is a black color
        /// </summary>
        /// <param name="color">the color to check</param>
        /// <returns>true if black</returns>
        public static bool IsBlack(this Color color)
        {
            if (Color.Black.ToArgb().Equals(color.ToArgb()))
                return true;

            return false;
        }

        /// <summary>
        /// check if the color is equal to specified color
        /// </summary>
        /// <param name="color">the color to check</param>
        /// <param name="toCompare">the color to compare to</param>
        /// <returns>true if black</returns>
        public static bool IsColor(this Color color, Color toCompare)
        {
            if (toCompare.ToArgb().Equals(color.ToArgb()))
                return true;

            return false;
        }

        /// <summary>
        /// check if the color is a White color
        /// </summary>
        /// <param name="color">the color to check</param>
        /// <returns>true if White</returns>
        public static bool IsWhite(this Color color)
        {
            if (Color.White.ToArgb().Equals(color.ToArgb()))
                return true;

            return false;
        }

        /// <summary>
        /// check if the color is a White or black color
        /// </summary>
        /// <param name="color">the color to check</param>
        /// <returns>true if the color is black or White</returns>
        public static bool IsBlackOrWhite(this Color color)
        {
            if (color.IsBlack() || color.IsWhite())
                return true;

            return false;
        }

        /// <summary>
        /// get the count of the Words inside the string
        /// </summary>
        /// <param name="value">the string to count the Words for</param>
        /// <returns>number of Words</returns>
        public static int WordsCount(this string value)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' , '\t' };
            return value.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// get the count of the chars inside the string
        /// </summary>
        /// <param name="value">the string to count the chars for</param>
        /// <returns>number of chars</returns>
        public static int CharsCount(this string value)
        {
            return value.ToCharArray().Length;
        }
    }

    public static class ControlsHelpers
    {
        public static void MoveRight(this Control targetControl, int moveTo)
        {
            targetControl.Location = new Point(targetControl.Location.X + moveTo, targetControl.Location.Y);
        }

        public static void MoveLeft(this Control targetControl, int moveTo)
        {
            targetControl.Location = new Point(targetControl.Location.X - moveTo, targetControl.Location.Y);
        }

        public static void MoveDown(this Control targetControl, int moveTo)
        {
            targetControl.Location = new Point(targetControl.Location.X, targetControl.Location.Y + moveTo);
        }

        public static void MoveUp(this Control targetControl, int moveTo)
        {
            targetControl.Location = new Point(targetControl.Location.X, targetControl.Location.Y - moveTo);
        }
    }
}
