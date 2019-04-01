using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class YLocationEffect : IEffect
    {
        private readonly int _originalValue;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.Y; }
        }
        public Control TargetControl { get; }

        public YLocationEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalValue = targetControl.Top;
        }

        public int GetCurrentValue()
        {
            return TargetControl.Top;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            TargetControl.Top = newValue;
        }

        public int GetMinimumValue()
        {
            return int.MinValue;
        }

        public int GetMaximumValue()
        {
            return int.MaxValue;
        }

        public void SetControlToOriginalState()
        {
            TargetControl.Top = _originalValue;
        }
    }
}
