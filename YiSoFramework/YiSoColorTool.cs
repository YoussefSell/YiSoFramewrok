namespace YiSoFramework
{
    using System;
    using System.Drawing;

    public class YiSoColorsTool
    {
        /// <summary>
        /// get the color object of the specified Hex Value
        /// </summary>
        /// <param name="HexValue">the HexValue of the color</param>
        /// <returns>Color obj</returns>
        public static Color GetColor(string HexValue)
        {
            return (Color)new ColorConverter().ConvertFromString(HexValue);
        }

        /// <summary>
        /// adds two colors
        /// </summary>
        /// <param name="color1">the first color</param>
        /// <param name="color2">the second color</param>
        /// <returns>new color base on adding the color1 to color2</returns>
        public static Color AddColors(Color color1, Color color2)
        {
            return Color.FromArgb(
                (color1.R + color2.R) / 2,
                (color1.G + color2.G) / 2,
                (color1.B + color2.B) / 2);
        }

        /// <summary>
        /// this method will make a shadow based on the color passed in the parameter
        /// </summary>
        /// <param name="color">the color that you will make the shadow from it</param>
        /// <param name="shadowDepth">the value of the shadow depth</param>
        /// <returns>a shadow version of the passed color</returns>
        public static Color ColorShadow(Color color, int shadowDepth)
        {
            return Color.FromArgb(
                color.R - shadowDepth < 0 ? 0 : color.R - shadowDepth,
                color.G - shadowDepth < 0 ? 0 : color.G - shadowDepth,
                color.B - shadowDepth < 0 ? 0 : color.B - shadowDepth);
        }

        /// <summary>
        /// this method allows you to transfer a color (<paramref name="baseColor"/>) to a new color 
        /// based on the <paramref name="extenderColor"/> and the <paramref name="jumpingValue"/>
        /// </summary>
        /// <param name="jumpingValue">the value jumping between the <paramref name="baseColor"/> and <paramref name="extenderColor"/> </param>
        /// <param name="baseColor">the base color which the method will start with</param>
        /// <param name="extenderColor">the color which <paramref name="baseColor"/> will jump into it</param>
        /// <returns>the new color which is Transfer from the <paramref name="baseColor"/> </returns>
        public static Color ColorTransfer(int jumpingValue, Color baseColor, Color extenderColor)
        {
            int r = (int)(Math.Round(baseColor.R + (extenderColor.R - (double)baseColor.R) * jumpingValue * 0.009, 0));
            int g = (int)(Math.Round(baseColor.G + (extenderColor.G - (double)baseColor.G) * jumpingValue * 0.009, 0));
            int b = (int)(Math.Round(baseColor.B + (extenderColor.B - (double)baseColor.B) * jumpingValue * 0.009, 0));
            return ValidateColorRGB(r, g, b);
        }

        /// <summary>
        /// validate the RGB value of the color
        /// </summary>
        /// <param name="r">red value</param>
        /// <param name="g">green value</param>
        /// <param name="b">blue value</param>
        /// <returns>color from the RGB value</returns>
        public static Color ValidateColorRGB(int r, int g, int b)
        {
            if (r > 255 || r < 0)
            {
                r = r > 255 ? 255 : 0;
            }
            if (g > 255 || g < 0)
            {
                g = g > 255 ? 255 : 0;
            }
            if (b > 255 || b < 0)
            {
                b = b > 255 ? 255 : 0;
            }

            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// get the Distance between two colors
        /// </summary>
        /// <param name="color1">color 1</param>
        /// <param name="color2">color 2</param>
        /// <returns>the Distance between two colors</returns>
        public static double ColorDistance(Color color1, Color color2)
        {
            long rmean = (color1.R + color2.R) / 2;
            long r = color1.R - color2.R;
            long g = color1.G - color2.G;
            long b = color1.B - color2.B;
            return Math.Sqrt((((512 + rmean) * r * r) >> 8) + 4 * g * g + (((767 - rmean) * b * b) >> 8));
        }
    }
}
