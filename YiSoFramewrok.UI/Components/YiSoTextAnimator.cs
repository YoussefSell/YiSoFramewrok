using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace YiSoFramework.UI
{
    public enum TextAnimationType
    {
        TextTyping,
        TextBlinking,
        TextGrow
    }

    public partial class YiSoTextAnimator : Component, IYiSoComponet
    {
        #region private properties

        private bool _growed = false;
        private Font _originalFont;
        private Color _originalColor;
        private Control _targetControl;
        private string _textToAnimate;
        private char[] _chars;
        private int _curChar = 0;

        #endregion

        #region Public Props

        /// <summary>
        /// represent an instant of the form that holds the YiSoDragControl instant
        /// </summary>
        public ContainerControl ContainerControl { get; private set; }

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

        /// <summary>
        /// specify the target control for the Ellipser
        /// </summary>
        public Control TargetControl
        {
            get => _targetControl;
            set
            {
                _targetControl = value;
                InitializeControl();
            }
        }

        /// <summary>
        /// specify the animation type
        /// </summary>
        public TextAnimationType TextAnimationType { get; set; } = TextAnimationType.TextTyping;

        /// <summary>
        /// speed of animation
        /// </summary>
        public int AnimationSpeed
        {
            get => TextAnimator.Interval;
            set => TextAnimator.Interval = value;
        }

        /// <summary>
        /// keep the first character when the animation of the text is Finished
        /// </summary>
        public bool KeepFirstChar { get; set; } = false;

        /// <summary>
        /// the first color to blink the text to
        /// </summary>
        public Color TextBlinkingColor { get; set; } = YiSoColors.GRAY;

        /// <summary>
        /// the Reverse color to blink the text to
        /// </summary>
        public Color TextBlinkingColorReverse { get; set; } = YiSoColors.LIME;

        /// <summary>
        /// the font that the text should grow to
        /// </summary>
        public Font FontSizeToGrowTo { get; set; }

        /// <summary>
        /// the font that the text should grow from
        /// </summary>
        public Font FontSizeToGrowFrom { get; set; }

        #endregion

        #region Constructors

        public YiSoTextAnimator()
        {
            InitializeComponent();
        }

        public YiSoTextAnimator(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Methods

        public void InitializeControl()
        {
            if (!(_targetControl is null))
            {
                _textToAnimate = _targetControl.Text;
                _originalColor = _targetControl.ForeColor;
                _originalFont = _targetControl.Font;
                _chars = _textToAnimate.ToCharArray();
            }
        }

        public void RemoveControl()
        {
            if (!(_targetControl is null))
            {
                _textToAnimate = "";
                _chars = new char['\0'];
            }
        }

        public void Start()
        {
            if (TargetControl is null)
                throw new YiSoTargetControlNotspecifiedException();

            TextAnimator.Start();

            switch (TextAnimationType)
            {
                case TextAnimationType.TextTyping:
                    TargetControl.Text = "";
                    break;
                case TextAnimationType.TextBlinking:
                    break;
                case TextAnimationType.TextGrow:
                    if (FontSizeToGrowFrom is null)
                        throw new ArgumentNullException("you have to specify a Font to Grow from");
                    if (FontSizeToGrowTo is null)
                        throw new ArgumentNullException("you have to specify a Font to Grow to");
                    _growed = false;
                    break;
                default:
                    break;
            }
        }

        public void Stop()
        {
            TextAnimator.Stop();
            TargetControl.Text = _textToAnimate;
            TargetControl.ForeColor = _originalColor;
            _targetControl.Font = _originalFont;
        }

        #endregion

        private void TextAnimator_Tick(object sender, EventArgs e)
        {
            switch (TextAnimationType)
            {
                case TextAnimationType.TextTyping:
                    AnimateText();
                    break;
                case TextAnimationType.TextBlinking:
                    AnimateTextBlinking();
                    break;
                case TextAnimationType.TextGrow:
                    AnimateGrowTo();
                    break;
                default:
                    AnimateText();
                    break;
            }
        }

        private void AnimateTextBlinking()
        {
            var curColor = TargetControl.ForeColor.IsColor(TextBlinkingColor) ?
                TextBlinkingColorReverse : TextBlinkingColor;

            TargetControl.ForeColor = curColor;
        }

        private void AnimateText()
        {
            if (_curChar >= _chars.Length)
            {
                TargetControl.Text = "";
                _curChar = 0;

                if (!KeepFirstChar)
                {
                    return;
                }
            }

            TargetControl.Text += _chars[_curChar++];
        }

        private void AnimateGrowTo()
        {
            if (_growed)
            {
                TargetControl.Font = FontSizeToGrowFrom;
                _growed = false;
            }
            else
            {
                TargetControl.Font = FontSizeToGrowTo;
                _growed = true;
            }
        }
    }
}
