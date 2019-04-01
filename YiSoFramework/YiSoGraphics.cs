namespace YiSoFramework
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;

    [DebuggerStepThrough]
    public class YiSoGraphics
    {
        public static Image OverlayColor(Image _Image, Color Find, Color Replace)
        {
            Bitmap bitmap = new Bitmap(_Image);
            for (int y = 0; y < bitmap.Height; ++y)
            {
                for (int x = 0; x < bitmap.Width; ++x)
                {
                    if (!YiSoGraphics.smethod_0(bitmap.GetPixel(x, y)))
                        bitmap.SetPixel(x, y, Replace);
                }
            }
            return (Image)bitmap;
        }

        public static Image OverlayColor(Image _Image, Color Replace)
        {
            Bitmap bitmap = new Bitmap(_Image);
            for (int y = 0; y < bitmap.Height; ++y)
            {
                for (int x = 0; x < bitmap.Width; ++x)
                {
                    if (!YiSoGraphics.smethod_0(bitmap.GetPixel(x, y)))
                        bitmap.SetPixel(x, y, Replace);
                }
            }
            return (Image)bitmap;
        }

        public static Image Smoothen(Image _img)
        {
            Bitmap bitmap = new Bitmap(_img);
            List<int[]> numArrayList = new List<int[]>();
            for (int y = 0; y < bitmap.Height - 1; ++y)
            {
                for (int x = 0; x < bitmap.Width - 1; ++x)
                {
                    Color[] colorArray = new Color[4];
                    colorArray[0] = bitmap.GetPixel(x, y);
                    colorArray[2] = bitmap.GetPixel(x, y + 1);
                    colorArray[1] = bitmap.GetPixel(x + 1, y);
                    colorArray[3] = bitmap.GetPixel(x + 1, y + 1);
                    if (colorArray[1] == colorArray[2] && !YiSoGraphics.smethod_0(colorArray[1]) && YiSoGraphics.smethod_0(colorArray[0]))
                        numArrayList.Add(new int[2] { x, y });
                    if (colorArray[0] == colorArray[3] && !YiSoGraphics.smethod_0(colorArray[0]) && YiSoGraphics.smethod_0(colorArray[2]))
                        numArrayList.Add(new int[2] { x, y + 1 });
                    if (colorArray[0] == colorArray[3] && !YiSoGraphics.smethod_0(colorArray[0]) && YiSoGraphics.smethod_0(colorArray[1]))
                        numArrayList.Add(new int[2] { x + 1, y });
                    if (colorArray[1] == colorArray[2] && !YiSoGraphics.smethod_0(colorArray[1]) && YiSoGraphics.smethod_0(colorArray[3]))
                        numArrayList.Add(new int[2] { x + 1, y + 1 });
                }
            }
            for (int index = 0; index < numArrayList.Count; ++index)
                bitmap.SetPixel(numArrayList[index][0], numArrayList[index][1], YiSoGraphics.AddColor(Color.Yellow, Color.FromArgb(211, 211, 211)));
            return (Image)bitmap;
        }

        public static Color AddColor(Color col1, Color col2)
        {
            return Color.FromArgb(((int)col1.R + (int)col2.R) / 2, ((int)col1.G + (int)col2.G) / 2, ((int)col1.B + (int)col2.B) / 2);
        }

        private static bool smethod_0(Color color_0)
        {
            if ((int)color_0.R == 0 && (int)color_0.G == 0)
                return (int)color_0.B == 0;
            return false;
        }

        /// <summary>
        /// this method allows you to transfer a color (<paramref name="baseColor"/>) to a new color based on the <paramref name="extenderColor"/> and the <paramref name="jumpingValue"/>
        /// </summary>
        /// <param name="jumpingValue">the value jumping between the <paramref name="baseColor"/> and <paramref name="extenderColor"/> </param>
        /// <param name="baseColor">the base color which the method will start with</param>
        /// <param name="extenderColor">the color which <paramref name="baseColor"/> will jump into it</param>
        /// <returns>the new color which is Transfer from the <paramref name="baseColor"/> </returns>
        //public static Color ColorTransfer(int jumpingValue, Color baseColor, Color extenderColor)
        //{
        //    return YiSoColors.ColorTransfer(jumpingValue, baseColor, extenderColor);
        //}
    }
}
