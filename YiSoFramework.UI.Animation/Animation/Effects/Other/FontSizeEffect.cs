using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class FontSizeEffect : IEffect
    {
        private readonly System.Drawing.Font _originalValue;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.SIZE; }
        }
        public Control TargetControl { get; }

        public FontSizeEffect(Control targetControl)
        {
            TargetControl = targetControl;
            _originalValue = targetControl.Font;
        }

        public int GetCurrentValue()
        {
            return (int)TargetControl.Font.SizeInPoints;
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            TargetControl.Font = new System.Drawing.Font(TargetControl.Font.FontFamily, newValue);
        }

        public int GetMinimumValue()
        {
            return 0;
        }

        public int GetMaximumValue()
        {
            return int.MaxValue;
        }

        public void SetControlToOriginalState()
        {
            TargetControl.Font = _originalValue;
        }
    }
}
