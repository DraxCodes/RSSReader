using System;
using System.Runtime.Serialization;

namespace RSSReader.Exceptions
{
    [Serializable]
    public class InvalidRssFeedException : Exception
    {
        public InvalidRssFeedException()
        {
        }

        public InvalidRssFeedException(string message) : base(message)
        {
        }

        public InvalidRssFeedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRssFeedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}