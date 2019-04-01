using System;
using System.Windows.Forms;
using System.Drawing;

namespace YiSoFramework.Animation.Effects
{
    public class RightAnchoredWidthEffect : IEffect
    {
        private readonly Rectangle _originalValue;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.BOUNDS; }
        }
        public Control TargetControl { get; }

        public RightAnchoredWidthEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalValue = targetControl.Bounds;
        }

        public int GetCurrentValue()
        {
            return TargetControl.Width;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            var size = new Size(newValue, TargetControl.Height);
            var location = new Point(TargetControl.Left +
                (TargetControl.Width - newValue), TargetControl.Top);

            TargetControl.Bounds = new Rectangle(location, size);
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
            TargetControl.Bounds = _originalValue;
        }
    }
}
