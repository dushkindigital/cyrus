using System;
using System.Runtime.Serialization;


namespace Cyrus.Services
{
    public class UnrecoverableException : ServiceException
    {
        public UnrecoverableException()
        { }

        public UnrecoverableException(string message)
            : base(message)
        { }

        public UnrecoverableException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public UnrecoverableException(string message, string trackingId)
            : base(message, trackingId)
        {
        }

        public UnrecoverableException(string message, string trackingId, Exception exception)
            : base(message, trackingId, exception)
        {
        }

        protected UnrecoverableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
