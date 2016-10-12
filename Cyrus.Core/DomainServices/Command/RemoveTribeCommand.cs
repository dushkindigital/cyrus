using MediatR;

namespace Cyrus.Core.DomainServices.Command
{
    public class RemoveTribeCommand : BaseRequest, IRequest
    {
        public int TribeId { get; set; }
    }
}
