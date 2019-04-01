using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class XLocationEffect : IEffect
    {
        private readonly int _originalValue;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.X; }
        }
        public Control TargetControl { get; }

        public XLocationEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalValue = targetControl.Left;
        }

        public int GetCurrentValue()
        {
            return TargetControl.Left;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            TargetControl.Left = newValue;
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
            TargetControl.Left = _originalValue;
        }
    }
}
