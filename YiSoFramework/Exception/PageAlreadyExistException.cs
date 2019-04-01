namespace YiSoFramework
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class PageAlreadyExistException : Exception
    {
        public PageAlreadyExistException()
        {
        }

        public PageAlreadyExistException(string message) : base(message)
        {
        }

        public PageAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PageAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}