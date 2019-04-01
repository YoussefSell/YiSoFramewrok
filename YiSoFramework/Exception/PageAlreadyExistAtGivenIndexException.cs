namespace YiSoFramework
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class PageAlreadyExistAtGivenIndexException : Exception
    {
        public PageAlreadyExistAtGivenIndexException()
        {
        }

        public PageAlreadyExistAtGivenIndexException(string message) : base(message)
        {
        }

        public PageAlreadyExistAtGivenIndexException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PageAlreadyExistAtGivenIndexException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}