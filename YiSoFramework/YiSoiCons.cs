namespace YiSoFramework
{
    using System.Drawing;

    public static class YiSoiCons
    {
        public static Bitmap GetIcon(YiSoiConsList icon)
        {
            switch (icon)
            {
                case YiSoiConsList.CheckIcon:
                    return Properties.Resources.check;
                case YiSoiConsList.ErrorIcon:
                    return Properties.Resources.cancel;
                case YiSoiConsList.warning:
                    return Properties.Resources.warning;
                case YiSoiConsList.none:
                default: return null;
            }
        }
    }
}
