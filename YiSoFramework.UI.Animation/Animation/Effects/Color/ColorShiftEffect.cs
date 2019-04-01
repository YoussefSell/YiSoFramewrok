using System;
using System.Drawing;
using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class ColorShiftEffect : IEffect
    {
        private readonly Color _originalColor;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.COLOR; }
        }
        public Control TargetControl { get; }

        public ColorShiftEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalColor = TargetControl.BackColor;
        }

        public int GetCurrentValue()
        {
            return TargetControl.BackColor.ToArgb();
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            int actualValueChange = Math.Abs(originalValue - valueToReach);
            int currentValue = GetCurrentValue();

            double absoluteChangePerc = ((double)((originalValue - newValue) * 100)) / actualValueChange;
            absoluteChangePerc = Math.Abs(absoluteChangePerc);

            if (absoluteChangePerc > 100.0f)
                return;

            Color originalColor = Color.FromArgb(originalValue);
            Color newColor = Color.FromArgb(valueToReach);

            int newA = Interpolate(originalColor.A, newColor.A, absoluteChangePerc);
            int newR = Interpolate(originalColor.R, newColor.R, absoluteChangePerc);
            int newG = Interpolate(originalColor.G, newColor.G, absoluteChangePerc);
            int newB = Interpolate(originalColor.B, newColor.B, absoluteChangePerc);

            TargetControl.BackColor = Color.FromArgb(newA, newR, newG, newB);
        }

        public int GetMinimumValue()
        {
            return Color.Black.ToArgb();
        }

        public int GetMaximumValue()
        {
            return Color.White.ToArgb();
        }

        private int Interpolate(int val1, int val2, double changePerc)
        {
            int difference = val2 - val1;
            int distance = (int)(difference * (changePerc / 100));
            return (val1 + distance);
        }

        public void SetControlToOriginalState()
        {
            TargetControl.BackColor = _originalColor;
        }
    }
}
