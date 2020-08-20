using System;
using System.Runtime.Serialization;

namespace RSSReader.Exceptions
{
    [Serializable]
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException(string message) : base(message) { }

        protected InvalidUrlException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}