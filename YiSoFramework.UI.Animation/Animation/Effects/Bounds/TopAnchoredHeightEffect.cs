using System;
using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class TopAnchoredHeightEffect : IEffect
    {
        private readonly int _originalValue;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.HEIGHT; }
        }

        public Control TargetControl { get; }

        public TopAnchoredHeightEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalValue = targetControl.Height;
        }

        public int GetCurrentValue()
        {
            return TargetControl.Height;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            TargetControl.Height = newValue;
        }

        public int GetMinimumValue()
        {
            return TargetControl.MinimumSize.IsEmpty ? int.MinValue
                : TargetControl.MinimumSize.Height;
        }

        public int GetMaximumValue()
        {
            return TargetControl.MaximumSize.IsEmpty ? int.MaxValue
                : TargetControl.MaximumSize.Height;
        }

        public void SetControlToOriginalState()
        {
            TargetControl.Height = _originalValue;
        }
    }
}
