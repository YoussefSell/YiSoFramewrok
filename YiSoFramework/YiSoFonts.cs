namespace YiSoFramework
{
    using System;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Runtime.InteropServices;

    public class YiSoFonts
    {
        public static Font GetRobotoFont(YiSoFontWeight style, YiSoFontSize fontSize, bool useInMemory = true)
        {
            if (useInMemory)
                return new Font(GetRobotoFontFamily(style), fontSize.ToFloat());

            return new Font(GetFontFromFile(style), fontSize.ToFloat());
        }

        private static FontFamily GetRobotoFontFamily(YiSoFontWeight style)
        {
            var fontList = new PrivateFontCollection();

            switch (style)
            {
                case YiSoFontWeight.Black:
                    GetFontFromMemory(fontList, Properties.Resources.Roboto_Black);
                    break;
                case YiSoFontWeight.Bold:
                    GetFontFromMemory(fontList, Properties.Resources.Roboto_Bold);
                    break;
                case YiSoFontWeight.Light:
                    GetFontFromMemory(fontList, Properties.Resources.Roboto_Light);
                    break;
                case YiSoFontWeight.Medium:
                    GetFontFromMemory(fontList, Properties.Resources.Roboto_Medium);
                    break;
                case YiSoFontWeight.Regular:
                    GetFontFromMemory(fontList, Properties.Resources.Roboto_Regular);
                    break;
                case YiSoFontWeight.Italic:
                    GetFontFromMemory(fontList, Properties.Resources.Roboto_Italic);
                    break;
                case YiSoFontWeight.Thin:
                default:
                    GetFontFromMemory(fontList, Properties.Resources.Roboto_Thin);
                    break;
            }

            return fontList.Families[0];
        }

        private static void GetFontFromMemory(PrivateFontCollection fontList, byte[] file)
        {
            IntPtr data = Marshal.AllocCoTaskMem(file.Length);
            Marshal.Copy(file, 0, data, file.Length);
            fontList.AddMemoryFont(data, file.Length);
            Marshal.FreeCoTaskMem(data);
        }

        private static FontFamily GetFontFromFile(YiSoFontWeight style)
        {
            var fontList = new PrivateFontCollection();

            //fontList.AddFontFile(@"Resources\Roboto-BlackItalic.ttf");
            //fontList.AddFontFile(@"Resources\Roboto-BoldItalic.ttf");
            //fontList.AddFontFile(@"Resources\Roboto-LightItalic.ttf");
            //fontList.AddFontFile(@"Resources\Roboto-MediumItalic.ttf");
            //fontList.AddFontFile(@"Resources\Roboto-ThinItalic.ttf");

            switch (style)
            {
                case YiSoFontWeight.Black:
                    fontList.AddFontFile(@"Resources\Roboto-Black.ttf");
                    return fontList.Families[0];
                case YiSoFontWeight.Bold:
                    fontList.AddFontFile(@"Resources\Roboto-Bold.ttf");
                    return fontList.Families[0];
                case YiSoFontWeight.Light:
                    fontList.AddFontFile(@"Resources\Roboto-Light.ttf");
                    return fontList.Families[0];
                case YiSoFontWeight.Medium:
                    fontList.AddFontFile(@"Resources\Roboto-Medium.ttf");
                    return fontList.Families[0];
                case YiSoFontWeight.Regular:
                    fontList.AddFontFile(@"Resources\Roboto-Regular.ttf");
                    return fontList.Families[0];
                case YiSoFontWeight.Italic:
                    fontList.AddFontFile(@"Resources\Roboto-Italic.ttf");
                    return fontList.Families[0];
                case YiSoFontWeight.Thin:
                default:
                    fontList.AddFontFile(@"Resources\Roboto-Thin.ttf");
                    return fontList.Families[0];
            }
        }
    }
}
