namespace YiSoFramework
{
    using System;
    using System.Windows.Forms;

    //[DebuggerStepThrough]
    public class YiSoStyler
    {
        private Control _controlToStyle;
        private int _picHeight;
        private int _picWidth;
        private int _zoomedHeight;
        private int _zoomedWidth;

        /// <summary>
        /// check if the control is zoom in or not
        /// </summary>
        public bool Zoomed { get; private set; } = false;

        /// <summary>
        /// the default constructor of the YiSoStyler Class
        /// </summary>
        /// <param name="controlToStyle"></param>
        public YiSoStyler(Control controlToStyle)
        {
            _controlToStyle = controlToStyle;
        }

        /// <summary>
        /// zoom the control by the given value
        /// </summary>
        /// <param name="Zoomby">the zooming value</param>
        public void ZoomIn(int Zoomby)
        {
            if (!Zoomed)
            {
                _picHeight = _controlToStyle.Height;
                _picWidth = _controlToStyle.Width;

                _zoomedHeight = Convert.ToInt32(Math.Round(Zoomby * 0.01 * _picHeight) * 0.5);
                _zoomedWidth = Convert.ToInt32(Math.Round(Zoomby * 0.01 * _picWidth) * 0.5);

                int NewPicHeight = _picHeight + _zoomedHeight * 2;
                int NewPicWidth = _picWidth + _zoomedWidth * 2;

                _controlToStyle.Width = NewPicWidth;
                _controlToStyle.Height = NewPicHeight;
                _controlToStyle.Top = _controlToStyle.Top - _zoomedHeight;
                _controlToStyle.Left = _controlToStyle.Left - _zoomedWidth;

                Zoomed = true;
            }
        }

        /// <summary>
        /// Return the control to it previous state
        /// </summary>
        public void ZoomOut()
        {
            if (Zoomed)
            {
                _controlToStyle.SuspendLayout();
                _controlToStyle.Width = _picWidth;
                _controlToStyle.Left = _controlToStyle.Left + _zoomedWidth;
                _controlToStyle.Height = _picHeight;
                _controlToStyle.Top = _controlToStyle.Top + _zoomedHeight;
                _controlToStyle.ResumeLayout();
                Zoomed = false;
            }
        }
    }
}
