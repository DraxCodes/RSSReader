using System;
using System.Runtime.Serialization;

namespace RSSReader.Exceptions
{
    [Serializable]
    public class InvalidRssFeedException : Exception
    {

        public InvalidRssFeedException(string message) : base(message) { }

        protected InvalidRssFeedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}