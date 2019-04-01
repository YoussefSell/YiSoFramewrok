using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using YiSoFramework.Animation;
using YiSoFramework.Animation.Easing;
using YiSoFramework.Animation.Effects;

namespace YiSoFramewrok.UI
{
    public partial class YiSoAnimator : Component
    {
        private YiSoAnimation _animator;
        private Control _targetControl;
        private _Effect _Effect;
        private _Easing _Easing;

        #region Public Props

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
        /// the container control
        /// </summary>
        public ContainerControl ContainerControl { get; private set; }

        /// <summary>
        /// the target control
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
        /// Value To Reach
        /// </summary>
        public int ValueToReach
        {
            get => _animator.ValueToReach;
            set => _animator.ValueToReach = value;
        }

        /// <summary>
        /// Animation Duration
        /// </summary>
        public int Duration
        {
            get => _animator.Duration;
            set => _animator.Duration = value;
        }

        /// <summary>
        /// Reverse the animation
        /// </summary>
        public bool Reverse
        {
            get => _animator.Reverse;
            set => _animator.Reverse = value;
        }

        /// <summary>
        /// animation Loop count
        /// </summary>
        public int Loop
        {
            get => _animator.Loop;
            set => _animator.Loop = value;
        }

        /// <summary>
        /// Easing Function, By Default set to Linear
        /// </summary>
        public _Easing Easing
        {
            get => _Easing;
            set
            {
                _Easing = value;
                _animator.EasingDelegate = EasingFunctions.Get(value);
            }
        }

        /// <summary>
        /// Effect type
        /// </summary>
        public _Effect Effect
        {
            get => _Effect;
            set
            {
                _Effect = value;
                if (!(TargetControl is null))
                    _animator.Effect = GetEffect(_Effect);
            }
        }

        /// <summary>
        /// the Maximum Value that the effect can reach
        /// </summary>
        public int MaximumValue
        {
            get => _animator.MaximumValue;
        }

        /// <summary>
        /// the Minimum Value that the effect can reach
        /// </summary>
        public int MinimumValue
        {
            get => _animator.MinimumValue;
        }

        /// <summary>
        /// the Current Value that the control is in
        /// </summary>
        public int CurrentValue
        {
            get => _animator.CurrentValue;
        }

        /// <summary>
        /// check if the animation is Increasing or decreasing
        /// </summary>
        public bool Increasing
        {
            get => _animator.Increasing;
        }

        /// <summary>
        /// represent the deference between the Original Value and the Value To Reach
        /// </summary>
        public int ChangeAmount
        {
            get => _animator.ChangeAmount;
        }

        /// <summary>
        /// Event raised when the animation is completed
        /// </summary>
        public event EventHandler<AnimationStatus> AnimationStopped;

        /// <summary>
        /// Event raised when the animation is Started
        /// </summary>
        public event EventHandler<AnimationStatus> AnimationStarted;

        /// <summary>
        /// Event raised when the animation is Resumed
        /// </summary>
        public event EventHandler<AnimationStatus> AnimationResumed;

        /// <summary>
        /// Event raised when the animation is Paused
        /// </summary>
        public event EventHandler<AnimationStatus> AnimationPaused;

        /// <summary>
        /// Event raised when the animation Current Value is Changed
        /// </summary>
        public event EventHandler<AnimationStatus> AnimationCurrentValueChanged;

        #endregion

        #region Constructors

        public YiSoAnimator()
        {
            InitializeComponent();
            _animator = new YiSoAnimation();
            _animator.AnimationStopped += _animator_AnimationCompleted;
            _animator.AnimationPaused += _animator_AnimationPaused;
            _animator.AnimationStarted += _animator_AnimationStarted;
            _animator.AnimationResumed += _animator_AnimationResumed;
            _animator.AnimationCurrentValueChanged += _animator_AnimationCurrentValueChanged;
        }

        public YiSoAnimator(IContainer container) : this()
        {
            container.Add(this);
        }

        #endregion

        #region Private Methods

        private void _animator_AnimationCurrentValueChanged(object sender, AnimationStatus e)
        {
            AnimationCurrentValueChanged?.Invoke(sender, e);
        }

        private void _animator_AnimationResumed(object sender, AnimationStatus e)
        {
            AnimationResumed?.Invoke(sender, e);
        }

        private void _animator_AnimationPaused(object sender, AnimationStatus e)
        {
            AnimationPaused?.Invoke(sender, e);
        }

        private void _animator_AnimationStarted(object sender, AnimationStatus e)
        {
            AnimationStarted?.Invoke(sender, e);
        }

        private void _animator_AnimationCompleted(object sender, AnimationStatus e)
        {
            AnimationStopped?.Invoke(sender, e);
        }

        private void InitializeControl()
        {
            if (!(TargetControl is null))
            {
                _animator.Effect = GetEffect(_Effect);
                _animator.TargetControl = TargetControl;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// start the animation
        /// </summary>
        public void Start()
        {
            _animator.Start();
        }

        /// <summary>
        /// stop the animation
        /// </summary>
        public void Stop()
        {
            _animator.Stop();
        }

        /// <summary>
        /// Pause animation
        /// </summary>
        public void Pause()
        {
            _animator.Pause();
        }

        /// <summary>
        /// Rest the Animation
        /// </summary>
        public void Reset()
        {
            _animator.Reset();
        }

        /// <summary>
        /// Raise the Animation Completed Event
        /// </summary>
        public void OnAnimationCompleted(AnimationStatus e)
        {
            _animator.OnAnimationStopped();
        }

        /// <summary>
        /// Raise the Animation Started Event
        /// </summary>
        public void OnAnimationStarted()
        {
            _animator.OnAnimationStarted();
        }

        /// <summary>
        /// Raise the Animation Resumed Event
        /// </summary>
        public void OnAnimationResumed()
        {
            _animator.OnAnimationResumed();
        }

        /// <summary>
        /// Raise the Animation Paused Event
        /// </summary>
        public void OnAnimationPaused()
        {
            _animator.OnAnimationPaused();
        }

        /// <summary>
        /// Raise the Animation Paused Event
        /// </summary>
        public void OnAnimationCurrentValueChanged()
        {
            _animator.OnAnimationCurrentValueChanged();
        }

        /// <summary>
        /// get the effect instant
        /// </summary>
        /// <param name="effect">effect</param>
        /// <returns>effect instant</returns>
        public IEffect GetEffect(_Effect effect)
        {
            switch (effect)
            {
                case _Effect.BottomAnchoredHeightEffect:
                    return new BottomAnchoredHeightEffect(TargetControl);
                case _Effect.HorizontalFoldEffect:
                    return new HorizontalFoldEffect(TargetControl);
                case _Effect.LeftAnchoredWidthEffect:
                    return new LeftAnchoredWidthEffect(TargetControl);
                case _Effect.RightAnchoredWidthEffect:
                    return new RightAnchoredWidthEffect(TargetControl);
                case _Effect.TopAnchoredHeightEffect:
                    return new TopAnchoredHeightEffect(TargetControl);
                case _Effect.VerticalFoldEffect:
                    return new VerticalFoldEffect(TargetControl);
                case _Effect.XLocationEffect:
                    return new XLocationEffect(TargetControl);
                case _Effect.YLocationEffect:
                    return new YLocationEffect(TargetControl);
                case _Effect.ColorChannelShiftEffect:
                    return new ColorChannelShiftEffect(TargetControl);
                case _Effect.ColorShiftEffect:
                    return new ColorShiftEffect(TargetControl);
                case _Effect.ControlFadeEffect:
                    return new ControlFadeEffect(TargetControl);
                case _Effect.FormFadeEffect:
                    return new FormFadeEffect(TargetControl);
                case _Effect.FontSizeEffect:
                    return new FontSizeEffect(TargetControl);
                default:
                    return new BottomAnchoredHeightEffect(TargetControl);
            }
        }

        #endregion
    }
}
