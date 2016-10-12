using System;
using System.Runtime.Serialization;


namespace Cyrus.Core.DomainServices
{
    public class RecoverableException : ServiceException
    {
        public RecoverableException()
        { }

        public RecoverableException(string message)
            : base(message)
        { }

        public RecoverableException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public RecoverableException(string message, string trackingId)
            : base(message, trackingId)
        {
        }

        public RecoverableException(string message, string trackingId, Exception exception)
            : base(message, trackingId, exception)
        {
        }

        protected RecoverableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
