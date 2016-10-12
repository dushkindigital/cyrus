using System;

namespace Cyrus.Services.Bases
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
