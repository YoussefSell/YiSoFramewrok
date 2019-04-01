using System.Drawing;
using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class ControlFadeEffect : IEffect
    {
        private readonly Color _originalColor;

        public const int MAX_OPACITY = 255; //original color
        public const int MIN_OPACITY = 0; // transparent
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.TRANSPARENCY; }
        }
        public Control TargetControl { get; }

        public ControlFadeEffect(Control control)
        {
            TargetControl = control;
            _originalColor = TargetControl.BackColor;
        }

        public int GetCurrentValue()
        {
            return TargetControl.BackColor.A;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            newValue = newValue > MAX_OPACITY ? MAX_OPACITY : newValue ;
            newValue = newValue < MIN_OPACITY ? MIN_OPACITY : newValue ;

            TargetControl.BackColor = Color.FromArgb(newValue / MAX_OPACITY,
                _originalColor.R,
                _originalColor.G,
                _originalColor.B);
        }

        public int GetMinimumValue()
        {
            return MIN_OPACITY;
        }

        public int GetMaximumValue()
        {
            return MAX_OPACITY;
        }

        public void SetControlToOriginalState()
        {
            TargetControl.BackColor = _originalColor;
        }
    }
}
