using System;
using System.Windows.Forms;
using YiSoFramework.Animation.Easing;
using YiSoFramework.Animation.Effects;
using Stopwatch = System.Diagnostics.Stopwatch;
using AnimationTimer = System.Timers.Timer;

namespace YiSoFramework.Animation
{
    public class YiSoAnimation
    {
        #region Private fields

        bool _paused = false;
        bool _reversed = false;
        int _originalValue;
        int _performedLoops = 0;
        IAsyncResult _animationExcutionResualt;
        Control _targetControl;
        AnimationStatus _animationStatus;
        AnimationTimer _animationTimer;
        Stopwatch _stopwatch;

        #endregion

        #region Public Props

        /// <summary>
        /// Target Control
        /// </summary>
        public Control TargetControl
        {
            get => _targetControl;
            set
            {
                if (value is null)
                    throw new ArgumentNullException("the provided value is null");

                _targetControl = value;
                InitializeControl();
            }
        }

        /// <summary>
        /// Value To Reach
        /// </summary>
        public int ValueToReach { get; set; } = 0;

        /// <summary>
        /// Animation Duration
        /// </summary>
        public int Duration { get; set; } = 1000;

        /// <summary>
        /// Reverse the animation
        /// </summary>
        public bool Reverse { get; set; } = false;

        /// <summary>
        /// animation Loop count
        /// </summary>
        public int Loop { get; set; } = 1;

        /// <summary>
        /// Easing Function, By Default set to Linear
        /// </summary>
        public EasingDelegate EasingDelegate { get; set; } = EasingFunctions.Linear;

        /// <summary>
        /// Effect type
        /// </summary>
        public IEffect Effect { get; set; }

        /// <summary>
        /// the Maximum Value that the effect can reach
        /// </summary>
        public int MaximumValue { get => Effect?.GetMaximumValue() ?? 0; }

        /// <summary>
        /// the Minimum Value that the effect can reach
        /// </summary>
        public int MinimumValue { get => Effect?.GetMinimumValue() ?? 0; }

        /// <summary>
        /// the Current Value that the control is in
        /// </summary>
        public int CurrentValue { get => Effect?.GetCurrentValue() ?? 0; }

        /// <summary>
        /// check if the animation is Increasing or decreasing
        /// </summary>
        public bool Increasing { get => _originalValue < ValueToReach; }

        /// <summary>
        /// represent the deference between the Original Value and the Value To Reach
        /// </summary>
        public int ChangeAmount { get => Math.Abs(_originalValue - ValueToReach); }

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

        #region Construction

        public YiSoAnimation()
        {
            _stopwatch = new Stopwatch();
            _animationStatus = new AnimationStatus();
            _animationTimer = new AnimationTimer();
            _animationTimer.Elapsed += _animationTimer_Elapsed;
            UpdateAnimationState();
        }

        public YiSoAnimation(Control targetControl) : this()
        {
            TargetControl = targetControl;
            InitializeControl();
        }

        ~YiSoAnimation()
        {
            _animationTimer?.Dispose();
        }

        private void InitializeControl()
        {
            if (Effect is null)
                throw new ArgumentNullException("Effect is null you have to specify a valid Effect");

            _originalValue = Effect.GetCurrentValue();
            CheckMinMaxValues();

            if (_originalValue == ValueToReach)
            {
                _animationStatus.IsCompleted = true;
                OnAnimationStopped();
                return;
            }

            UpdateInterval();
        }

        #endregion

        #region Private methods

        private void _animationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _animationExcutionResualt = TargetControl.BeginInvoke(new MethodInvoker(() =>
            {
                Effect.SetValue(_originalValue, ValueToReach, GetNewValue());
                UpdateAnimationState();
                OnAnimationCurrentValueChanged();

                if (_stopwatch.ElapsedMilliseconds >= Duration)
                {
                    if (Reverse && (!_reversed || Loop <= 0 || _performedLoops < Loop))
                    {
                        _reversed = !_reversed;
                        if (_reversed)
                            _performedLoops++;

                        FlipValues();
                        _stopwatch.Restart();
                        _animationTimer.Start();
                    }
                    else
                    {
                        _animationStatus.IsCompleted = true;
                        Stop();
                    }
                }
            }));
        }
        
        private void CheckMinMaxValues()
        {
            if (ValueToReach > MaximumValue)
                ValueToReach = MaximumValue;

            if (ValueToReach < MinimumValue)
                ValueToReach = MinimumValue;
        }

        private void UpdateInterval()
        {
            int actualValueChange = Math.Abs(_originalValue - ValueToReach);

            _animationTimer.Interval = (Duration > actualValueChange) ?
                           (Duration / actualValueChange) : actualValueChange;

            if (Effect.Interaction == EffectInteractions.COLOR)
                _animationTimer.Interval = 10;
        }

        private void UpdateAnimationState()
        {
            _animationStatus.IsCompleted = false;
            _animationStatus.ValueToReach = ValueToReach;
            _animationStatus.Increasing = Increasing;
            _animationStatus.CurrentValue = CurrentValue;
            _animationStatus.ElapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        }

        private int GetNewValue()
        {
            var cur = _paused ? CurrentValue : _originalValue;

            int minValue = Math.Min(cur, ValueToReach);
            int maxValue = Math.Abs(ValueToReach - cur);
            var val = (int)EasingDelegate(_stopwatch.ElapsedMilliseconds, minValue, maxValue, Duration);

            if (!Increasing)
                val = (cur + ValueToReach) - val - 1;

            return val;
        }

        private void FlipValues()
        {
            var temp = _originalValue;
            _originalValue = ValueToReach;
            ValueToReach = temp;
        }

        private void Rest()
        {
            if (_animationStatus.IsCompleted)
            {
                if (_reversed)
                {
                    _reversed = !_reversed;
                    _performedLoops = 0;
                }

                FlipValues();
            }
            _paused = false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// start the animation
        /// </summary>
        public void Start()
        {
            _animationStatus.IsCompleted = false;
            _animationTimer.Start();

            if (_paused)
            {
                OnAnimationResumed();
                _stopwatch.Start();
                _paused = false;
            }
            else
            {
                OnAnimationStarted();
                _stopwatch.Restart();
            }
        }

        /// <summary>
        /// pause the animation
        /// </summary>
        public void Pause()
        {
            if (_animationStatus.IsCompleted)
                return;

            _targetControl.EndInvoke(_animationExcutionResualt);
            _animationTimer.Stop();
            _stopwatch.Stop();
            _paused = true;
            OnAnimationPaused();
        }

        /// <summary>
        /// stop the animation
        /// </summary>
        public void Stop()
        {
            _targetControl.EndInvoke(_animationExcutionResualt);
            _animationTimer.Stop();
            _stopwatch.Reset();
            OnAnimationStopped();
            Rest();
        }

        /// <summary>
        /// Rest the Animation
        /// </summary>
        public void Reset()
        {
            if (!_animationStatus.IsCompleted)
                return;

            Effect.SetControlToOriginalState();
        }

        /// <summary>
        /// Raise the Animation Completed Event
        /// </summary>
        public void OnAnimationStopped()
        {
            AnimationStopped?.Invoke(_targetControl, _animationStatus);
        }

        /// <summary>
        /// Raise the Animation Started Event
        /// </summary>
        public void OnAnimationStarted()
        {
            AnimationStarted?.Invoke(_targetControl, _animationStatus);
        }

        /// <summary>
        /// Raise the Animation Resumed Event
        /// </summary>
        public void OnAnimationResumed()
        {
            AnimationResumed?.Invoke(_targetControl, _animationStatus);
        }

        /// <summary>
        /// Raise the Animation Paused Event
        /// </summary>
        public void OnAnimationPaused()
        {
            AnimationPaused?.Invoke(_targetControl, _animationStatus);
        }

        /// <summary>
        /// Raise the Animation Paused Event
        /// </summary>
        public void OnAnimationCurrentValueChanged()
        {
            AnimationCurrentValueChanged?.Invoke(_targetControl, _animationStatus);
        }

        /// <summary>
        /// get the string representation of the object
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return $@"Effect Type : {Effect.GetType()}, Easing Type : {EasingDelegate.GetType()}
Value To Reach : {ValueToReach}
Duration : {Duration}";
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// animation completed event for the static Animate method
        /// </summary>
        public static event EventHandler<AnimationStatus> Animated;

        /// <summary>
        /// Animate a control property from its present value to a target one
        /// </summary>
        /// <param name="control">Target control</param>
        /// <param name="iEffect">Effect to apply</param>
        /// <param name="easing">Easing function to apply</param>
        /// <param name="valueToReach">Target value reached when animation completes</param>
        /// <param name="duration">Amount of time taken to reach the target value</param>
        /// <param name="delay">Amount of delay to apply before animation starts</param>
        /// <param name="reverse">If set to true, animation reaches target value and animates back to initial value. It takes 2*<paramref name="duration"/></param>
        /// <param name="loops">If reverse is set to true, indicates how many loops to perform. Negatives or zero mean infinite loop</param>
        /// <returns></returns>
        public static AnimationStatus Animate(Control control, IEffect iEffect,
            EasingDelegate easing, int valueToReach, int duration, int delay,
            bool reverse = false, int loops = 1)
        {
            //used to calculate animation frame based on how much time has effectively passed
            var stopwatch = new Stopwatch();

            //used to access animation progress
            var animationStatus = new AnimationStatus();

            //This timer allows delayed start. Control's state checks and evaluations are delayed too.
            new System.Threading.Timer((state) =>
            {
                //is there anything to do here?
                int originalValue = iEffect.GetCurrentValue();
                if (originalValue == valueToReach)
                {
                    animationStatus.IsCompleted = true;
                    animationStatus.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    return;
                }

                //upper bound check
                int maxVal = iEffect.GetMaximumValue();
                if (valueToReach > maxVal)
                {
                    throw new ArgumentException($"Value must be lesser than the maximum allowed. " +
                        $"Max: {maxVal}, provided value: {valueToReach}", "valueToReach");
                }

                //lower bound check
                int minVal = iEffect.GetMinimumValue();
                if (valueToReach < minVal)
                {
                    throw new ArgumentException("Value must be greater than the minimum allowed. " +
                        $"Min: {minVal}, provided value: {valueToReach}", "valueToReach");
                }

                bool reversed = false;
                int performedLoops = 0;

                int actualValueChange = Math.Abs(originalValue - valueToReach);

                AnimationTimer animationTimer = new AnimationTimer
                {
                    //adjust interval (naive, edge cases can mess up)
                    Interval = (duration > actualValueChange) ?
                   (duration / actualValueChange) : actualValueChange
                };

                //because of naive interval calculation this is required
                if (iEffect.Interaction == EffectInteractions.COLOR)
                    animationTimer.Interval = 10;

                //main animation timer tick
                animationTimer.Elapsed += (o, e2) =>
                {
                    //main logic
                    bool increasing = originalValue < valueToReach;

                    int minValue = Math.Min(originalValue, valueToReach);
                    int maxValue = Math.Abs(valueToReach - originalValue);
                    int newValue = (int)easing(stopwatch.ElapsedMilliseconds, minValue, maxValue, duration);

                    if (!increasing)
                        newValue = (originalValue + valueToReach) - newValue - 1;

                    control.BeginInvoke(new MethodInvoker(() =>
                    {
                        iEffect.SetValue(originalValue, valueToReach, newValue);

                        bool timeout = stopwatch.ElapsedMilliseconds >= duration;
                        if (timeout)
                        {
                            if (reverse && (!reversed || loops <= 0 || performedLoops < loops))
                            {
                                reversed = !reversed;
                                if (reversed)
                                    performedLoops++;

                                int initialValue = originalValue;
                                int finalValue = valueToReach;

                                valueToReach = valueToReach == finalValue ? initialValue : finalValue;
                                originalValue = valueToReach == finalValue ? initialValue : finalValue;

                                stopwatch.Restart();
                                animationTimer.Start();
                            }
                            else
                            {
                                animationStatus.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                                animationStatus.IsCompleted = true;
                                animationTimer.Stop();
                                stopwatch.Stop();
                                Animated?.Invoke(control, animationStatus);
                            }
                        }
                    }));
                };

                //start
                stopwatch.Start();
                animationTimer.Start();

            }, null, delay, System.Threading.Timeout.Infinite);

            return animationStatus;
        }

        #endregion
    }
}
