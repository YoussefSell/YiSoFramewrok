namespace YiSoFramework
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class YiSoTargetControlNotspecifiedException : Exception
    {
        public YiSoTargetControlNotspecifiedException()
        {
        }

        public YiSoTargetControlNotspecifiedException(string message) : base(message)
        {
        }

        public YiSoTargetControlNotspecifiedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected YiSoTargetControlNotspecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}