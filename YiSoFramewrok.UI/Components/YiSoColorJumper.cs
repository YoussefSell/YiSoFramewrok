using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using YiSoFramework;
using System.Collections.Generic;

namespace YiSoFramework.UI
{
    public partial class YiSoColorJumper : Component
    {
        #region Private Fields

        private Color _targetControlCurrentColor;
        int R_Value = 244; bool MoveRValue = true;
        int G_Value = 65; bool MoveGValue = false;
        int B_Value = 65; bool MoveBValue = false;
        private int _currentColorInList = 0;
        private byte _inListJumpingValue = 0;

        #endregion

        #region Public Props

        /// <summary>
        /// the Control that Contain the object YiSoColorJump
        /// </summary>
        public ContainerControl ContainerControl { get; set; }

        /// <summary>
        /// the target Control that the YiSoColorJump is targeting
        /// </summary>
        public Control TargetControl { get; set; }

        /// <summary>
        /// list of colors
        /// </summary>
        public List<Color> ColorsList { get; set; } = YiSoColors.YiSoColorsList();

        /// <summary>
        /// true to use the list of colors
        /// </summary>
        public bool UseColorList { get; set; } = false;

        /// <summary>
        /// the animation interval
        /// </summary>
        public int Interval { get => timer.Interval; set => timer.Interval = value; }

        /// <summary>
        /// the jumping value to control the transformation between the colors must be between 1 and 255
        /// </summary>
        public byte JumpingValue { get; set; } = 1;

        /// <summary>
        /// the Site reference
        /// </summary>
        public override ISite Site
        {
            get => base.Site;
            set
            {
                base.Site = value;
                if (value is null)
                    return;

                if (value.GetService(typeof(IDesignerHost)) is IDesignerHost service)
                {
                    if (service.RootComponent is ContainerControl rootComponent)
                        ContainerControl = rootComponent;
                }
            }
        }

        #endregion

        #region Constructors

        public YiSoColorJumper()
        {
            InitializeComponent();
        }

        public YiSoColorJumper(IContainer container) : this()
        {
            container.Add(this);
        }

        #endregion

        #region Private Methods

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (UseColorList)
                UseList();
            else
                UseInternalColor();
        }

        private void UseInternalColor()
        {
            if (MoveRValue)
            {
                if (B_Value >= 244)
                {
                    R_Value -= JumpingValue;
                    if (R_Value <= 65)
                    {
                        MoveRValue = false;
                        MoveGValue = true;
                    }
                }
                else if (B_Value <= 65)
                {
                    R_Value += JumpingValue;
                    if (R_Value >= 244)
                    {
                        MoveRValue = false;
                        MoveGValue = true;
                    }
                }
            }
            else if (MoveGValue)
            {
                if (R_Value <= 65)
                {
                    G_Value += JumpingValue;
                    if (G_Value >= 244)
                    {
                        MoveGValue = false;
                        MoveBValue = true;
                    }
                }
                else if (R_Value >= 244)
                {
                    G_Value -= JumpingValue;
                    if (G_Value <= 65)
                    {
                        MoveGValue = false;
                        MoveBValue = true;
                    }
                }
            }
            else if (MoveBValue)
            {
                if (G_Value <= 65)
                {
                    B_Value += JumpingValue;
                    if (B_Value >= 244)
                    {
                        MoveBValue = false;
                        MoveRValue = true;
                    }
                }
                else if (G_Value >= 244)
                {
                    B_Value -= JumpingValue;
                    if (B_Value <= 65)
                    {
                        MoveBValue = false;
                        MoveRValue = true;
                    }
                }
            }

            TargetControl.BackColor = YiSoColorsTool.ValidateColorRGB(R_Value, G_Value, B_Value);
        }

        private void UseList()
        {
            _inListJumpingValue = (byte)(_inListJumpingValue + (JumpingValue == 0 ? 1 : JumpingValue));

            Color curColor = YiSoColorsTool.ColorTransfer(
                   _inListJumpingValue, ColorsList[_currentColorInList], ColorsList[_currentColorInList + 1]);

            TargetControl.BackColor = curColor;

            if (_inListJumpingValue >= 100)
            {
                _inListJumpingValue = 0;
                _currentColorInList++;
            }

            if (_currentColorInList >= ColorsList.Count - 1)
                _currentColorInList = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// start the animation
        /// </summary>
        public void Start()
        {
            if (TargetControl is null)
                TargetControl = ContainerControl;

            _targetControlCurrentColor = TargetControl.BackColor;

            timer.Start();
        }

        /// <summary>
        /// end the animation
        /// </summary>
        /// <param name="SetBackOriginalColor">true to set back the original color of the target control</param>
        public void Stop(bool SetBackOriginalColor = false)
        {
            timer.Stop();

            if (SetBackOriginalColor)
                TargetControl.BackColor = _targetControlCurrentColor;
        }

        #endregion
    }
}
