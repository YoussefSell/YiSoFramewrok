using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YiSoFramework;

namespace YiSoFramework.UI
{
    [DefaultEvent("Click")]
    public partial class YiSoButton : UserControl, IButtonControl
    {
        #region private props

        private DialogResult _dialogResult = DialogResult.None;
        private int _borderRadius = 5;
        private Color _buttonColor = YiSoColors.DEEPPURPLE;
        private Color _buttonCurrentColor;
        private Color _buttonclickColor;
        private Color _hoverdColor = YiSoColors.BLUE;
        private Color _pressedColor = YiSoColors.BLUE;
        private Color _textColor = YiSoColors.WHITE;

        #endregion

        #region Public props

        public override Font Font {
            get => TextLable.Font;
            set
            {
                TextLable.Font = value;
                Refresh();
            }
        }

        [Category("YiSoConfigurations")]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = value;
                RoundedEdges();
            }
        }

        [Category("YiSoConfigurations")]
        public Color ButtonColor
        {
            get => _buttonColor;
            set
            {
                _buttonColor = _buttonCurrentColor = value;
                _buttonclickColor = YiSoColorsTool.ColorShadow(BackColor, 50);
            }
        }

        [Category("YiSoConfigurations")]
        public Color HoverdColor
        {
            get => _hoverdColor;
            set
            {
                _hoverdColor = value;
            }
        }

        [Category("YiSoConfigurations")]
        public Color PressedColor
        {
            get => _pressedColor;
            set
            {
                _pressedColor = value;
            }
        }

        [Category("YiSoConfigurations")]
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                TextLable.ForeColor = _textColor;
            }
        }

        [Category("YiSoConfigurations")]
        public string ButtonText { get => TextLable.Text; set { TextLable.Text = value; Refresh(); } }

        public DialogResult DialogResult { get => _dialogResult; set => _dialogResult = value; }

        #endregion

        public YiSoButton()
        {
            InitializeComponent();
            Initializer();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        #region Private Methods

        public override void Refresh()
        {
            base.Refresh();
            AdjustSizing();
            RoundedEdges();
        }

        private void Initializer()
        {
            YiSoElipse.ApplyRoundedEdges(this, _borderRadius);
            BorderStyle = BorderStyle.None;
            BackColor = _buttonColor;

            //label styling
            //TextLable.Font = YiSoFonts.GetRobotoFont(FontWeight, FontSize);
            TextLable.ForeColor = _textColor;
        }

        private void AdjustSizing()
        {
            Size = new Size(TextLable.Size.Width + 32, 36);
            TextLable.Location = GetLableLocation();
        }

        private Point GetLableLocation()
        {
            return new Point((Size.Width - TextLable.Size.Width) / 2, (Size.Height - TextLable.Size.Height) / 2);
        }

        private void RoundedEdges()
        {
            YiSoElipse.ApplyRoundedEdges(this, _borderRadius);
        }
        #endregion

        private void TextLable_SizeChanged(object sender, EventArgs e)
        {
            AdjustSizing();
        }

        private void YiSoDefaultButton_SizeChanged(object sender, EventArgs e)
        {
            AdjustSizing();
        }

        private void YiSoDefaultButton_FontChanged(object sender, EventArgs e)
        {
            AdjustSizing();
        }

        private void TextLable_FontChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void YiSoButton_Load(object sender, EventArgs e)
        {

        }

        public void NotifyDefault(bool value)
        {

        }

        #region Click Handling

        public void PerformClick()
        {
            OnClick(new EventArgs());
        }

        private void TextLable_Click(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }

        private void YiSoButton_MouseDown(object sender, MouseEventArgs e)
        {
            _buttonColor = Color.Black;
        }

        private void YiSoButton_MouseUp(object sender, MouseEventArgs e)
        {
            //_buttonColor = _buttonCurrentColor;
        }

        private void YiSoButton_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
