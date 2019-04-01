namespace YiSoFramework
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class PageNotExistException : Exception
    {
        public PageNotExistException()
        {
        }

        public PageNotExistException(string message) : base(message)
        {
        }

        public PageNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PageNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}