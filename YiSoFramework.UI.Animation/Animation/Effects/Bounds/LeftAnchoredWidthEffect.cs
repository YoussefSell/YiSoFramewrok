using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class LeftAnchoredWidthEffect : IEffect
    {
        private readonly int _originalValue;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.WIDTH; }
        }
        public Control TargetControl { get; }

        public LeftAnchoredWidthEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalValue = targetControl.Width;
        }

        public int GetCurrentValue()
        {
            return TargetControl.Width;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            TargetControl.Width = newValue;
        }

        public int GetMinimumValue()
        {
            if (TargetControl.MinimumSize.IsEmpty)
                return int.MinValue;

            return TargetControl.MinimumSize.Width;
        }

        public int GetMaximumValue()
        {
            if (TargetControl.MaximumSize.IsEmpty)
                return int.MaxValue;

            return TargetControl.MaximumSize.Width;
        }

        public void SetControlToOriginalState()
        {
            TargetControl.Width = _originalValue;
        }
    }
}
