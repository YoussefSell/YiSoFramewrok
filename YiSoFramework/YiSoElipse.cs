namespace YiSoFramework
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class YiSoElipse
    {
        [DllImport("Gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern bool DeleteObject(IntPtr obj);
        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public static void ApplyRoundedEdges(Form form, int Elipse)
        {
            if (form == null)
                throw new YiSoTargetControlNotspecifiedException();

            IntPtr NewRegion = CreateRoundRectRgn(0, 0, form.Width, form.Height, Elipse, Elipse);
            if (NewRegion != IntPtr.Zero)
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.Region = Region.FromHrgn(NewRegion);
            }
            DeleteObject(NewRegion);
        }

        public static void ApplyRoundedEdges(Control control, int Elipse)
        {
            if (control == null)
                throw new YiSoTargetControlNotspecifiedException();

            IntPtr NewRegion = CreateRoundRectRgn(0, 0, control.Width, control.Height, Elipse, Elipse);
            if (NewRegion != IntPtr.Zero)
            {
                control.Region = Region.FromHrgn(NewRegion);
            }
            DeleteObject(NewRegion);
        }

        public static void UnApplyRoundedEdges(Control control)
        {
            if (control.Region != null)
            {
                control.Region = null;
            }
        }

        public static void UnApplyRoundedEdges(Form Form)
        {
            if (Form.Region != null)
            {
                Form.Region = null;
            }
        }
    }
}
