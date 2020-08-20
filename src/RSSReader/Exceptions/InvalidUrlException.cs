﻿using System;
using System.Runtime.Serialization;

namespace RSSReader.Exceptions
{
    [Serializable]
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException()
        {
        }

        public InvalidUrlException(string message) : base(message)
        {
        }

        public InvalidUrlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}