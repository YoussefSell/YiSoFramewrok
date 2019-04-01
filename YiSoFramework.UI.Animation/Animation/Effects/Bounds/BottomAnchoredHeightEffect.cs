using System.Windows.Forms;
using System.Drawing;

namespace YiSoFramework.Animation.Effects
{
    public class BottomAnchoredHeightEffect : IEffect
    {
        private readonly Rectangle _originalValue;
        public Control TargetControl { get; }
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.BOUNDS; }
        }

        public BottomAnchoredHeightEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalValue = targetControl.Bounds;
        }

        public int GetCurrentValue()
        {
            return TargetControl.Height;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            var size = new Size(TargetControl.Width, newValue);
            var location = new Point(TargetControl.Left, TargetControl.Top +
                (TargetControl.Height - newValue));

            TargetControl.Bounds = new Rectangle(location, size);
        }

        public int GetMinimumValue()
        {
            if (TargetControl.MinimumSize.IsEmpty)
                return int.MinValue;

            return TargetControl.MinimumSize.Height;
        }

        public int GetMaximumValue()
        {
            if (TargetControl.MaximumSize.IsEmpty)
                return int.MaxValue;

            return TargetControl.MaximumSize.Height;
        }

        public void SetControlToOriginalState()
        {
           TargetControl.Bounds = _originalValue;
        }
    }
}
