using System;
using System.Windows.Forms;

namespace YiSoFramework.Animation.Effects
{
    public class FormFadeEffect : IEffect
    {
        private readonly double _originalValue;
        public EffectInteractions Interaction
        {
            get { return EffectInteractions.TRANSPARENCY; }
        }
        public Control TargetControl { get; }

        public FormFadeEffect(Control targetControl)
        {
            if (!(targetControl is Form))
                throw new Exception("Fading effect can be applied only on forms");

            TargetControl = targetControl;
            _originalValue = ((Form)targetControl).Opacity;
        }

        public int GetCurrentValue()
        {
            var form = (Form)TargetControl;
            return (int)(form.Opacity * 100);
        }

        public void SetValue(int originalValue, int valueToReach, int newValue)
        {
            var form = (Form)TargetControl;
            form.Opacity = ((float)newValue) / 100;
        }

        public int GetMinimumValue()
        {
            return 0;
        }

        public int GetMaximumValue()
        {
            return 100;
        }

        public void SetControlToOriginalState()
        {
            ((Form)TargetControl).Opacity = _originalValue;
        }
    }
}
