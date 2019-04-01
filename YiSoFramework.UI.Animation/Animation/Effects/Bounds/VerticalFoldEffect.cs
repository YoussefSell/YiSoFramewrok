using System.Windows.Forms;
using System.Drawing;

namespace YiSoFramework.Animation.Effects
{
    public class VerticalFoldEffect : IEffect
    {
        private readonly Rectangle _originalValue;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.BOUNDS; }
        }
        public Control TargetControl { get; }

        public VerticalFoldEffect(Control targetControl)
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
            var center = new Point((TargetControl.Left + TargetControl.Right) / 2, TargetControl.Top);

            var size = new Size(newValue, TargetControl.Height);
            var location = new Point(center.X - (newValue / 2), TargetControl.Top);

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
