using System.Drawing;
using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class ColorChannelShiftEffect : IEffect
    {
        private readonly Color _originalColor;
        public ColorChannels ColorChannel { get; set; } 
            = ColorChannels.A;

        public EffectInteractions Interaction
        {
            get { return EffectInteractions.COLOR; }
        }
        public Control TargetControl { get; }

        public ColorChannelShiftEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalColor = targetControl.BackColor;
        }

        public int GetCurrentValue()
        {
            if (ColorChannel == ColorChannels.A)
                return TargetControl.BackColor.A;

            if (ColorChannel == ColorChannels.R)
                return TargetControl.BackColor.R;

            if (ColorChannel == ColorChannels.G)
                return TargetControl.BackColor.G;

            return TargetControl.BackColor.B;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            if (newValue >= 0 && newValue <= 255)
            {
                int a = TargetControl.BackColor.A;
                int r = TargetControl.BackColor.R;
                int g = TargetControl.BackColor.G;
                int b = TargetControl.BackColor.B;

                switch (ColorChannel)
                {
                    case ColorChannels.A: a = newValue; break;
                    case ColorChannels.R: r = newValue; break;
                    case ColorChannels.G: g = newValue; break;
                    case ColorChannels.B: b = newValue; break;
                }

                TargetControl.BackColor = Color.FromArgb(a, r, g, b);
            }
        }

        public int GetMinimumValue()
        {
            return 0;
        }

        public int GetMaximumValue()
        {
            return 255;
        }

        public void SetControlToOriginalState()
        {
            TargetControl.BackColor = _originalColor;
        }
    }
}
