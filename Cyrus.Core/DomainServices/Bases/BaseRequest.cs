using System;

namespace Cyrus.Core.DomainServices
{
    public abstract class BaseRequest
    {
        private readonly Guid _trackingId;

        public Guid TrackingId { get { return _trackingId; } }

        public BaseRequest()
        {
            _trackingId = Guid.NewGuid();
        }
    }
}
