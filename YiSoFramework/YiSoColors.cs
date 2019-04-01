namespace YiSoFramework
{
    using System.Collections.Generic;
    using System.Drawing;

    [System.Diagnostics.DebuggerStepThrough]
    public class YiSoColors
    {
        public static Color NAVY { get; }       = YiSoColorsTool.GetColor("#001f3f");
        public static Color BLUE { get; }       = YiSoColorsTool.GetColor("#0074D9");
        public static Color AQUA { get; }       = YiSoColorsTool.GetColor("#7FDBFF");
        public static Color TEAL { get; }       = YiSoColorsTool.GetColor("#39CCCC");
        public static Color OLIVE { get; }      = YiSoColorsTool.GetColor("#3D9970");
        public static Color GREEN { get; }      = YiSoColorsTool.GetColor("#2ECC40");
        public static Color LIME { get; }       = YiSoColorsTool.GetColor("#01FF70");
        public static Color YELLOW { get; }     = YiSoColorsTool.GetColor("#FFDC00");
        public static Color ORANGE { get; }     = YiSoColorsTool.GetColor("#FF851B");
        public static Color RED { get; }        = YiSoColorsTool.GetColor("#FF4136");
        public static Color MAROON { get; }     = YiSoColorsTool.GetColor("#85144b");
        public static Color FUCHSIA { get; }    = YiSoColorsTool.GetColor("#F012BE");
        public static Color PURPLE { get; }     = YiSoColorsTool.GetColor("#B10DC9");
        public static Color SILVER { get; }     = YiSoColorsTool.GetColor("#DDDDDD");
        public static Color BLACK { get; }      = YiSoColorsTool.GetColor("#FF000000");
        public static Color WHITE { get; }      = YiSoColorsTool.GetColor("#FFFFFFFF");
        public static Color DEEPPURPLE { get; } = YiSoColorsTool.GetColor("#6200EE");
        public static Color GRAY { get; }       = YiSoColorsTool.GetColor("#bdbdbd");

        /// <summary>
        /// this method return a list that contain all available colors in YiSoFramework
        /// </summary>
        /// <returns>list of colors</returns>
        public static List<Color> YiSoColorsList()
        {
            return new List<Color>
            {
                BLACK,
                WHITE,
                NAVY,
                BLUE,
                AQUA,
                TEAL,
                OLIVE,
                GREEN,
                LIME,
                YELLOW,
                ORANGE,
                MAROON,
                FUCHSIA,
                PURPLE,
                GRAY,
                SILVER
            };
        }
    }
}
