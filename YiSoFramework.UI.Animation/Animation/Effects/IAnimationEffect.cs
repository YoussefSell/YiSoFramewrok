namespace YiSoFramework.Animation.Effects
{
    /// <summary>
    /// By implementing this interface you define what property of your control
    /// is manipulated and the way you manipulate it.
    /// </summary>
    public interface IEffect
    {
        EffectInteractions Interaction { get; }
        System.Windows.Forms.Control TargetControl { get; }

        int GetCurrentValue();
        void SetValue(int originalValue, int valueToReach, int newValue);
        void SetControlToOriginalState();

        int GetMinimumValue();
        int GetMaximumValue();
    }
}
