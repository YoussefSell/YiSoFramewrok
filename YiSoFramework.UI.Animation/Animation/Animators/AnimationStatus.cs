namespace YiSoFramework.Animation
{
    public class AnimationStatus : System.EventArgs
    {
        public bool IsCompleted { get; internal set; }
        public long ElapsedMilliseconds { get; internal set; }
        public int ValueToReach { get; internal set; }
        public int CurrentValue { get; internal set; }
        public bool Increasing { get; internal set; }

        public AnimationStatus() {}
    }
}
