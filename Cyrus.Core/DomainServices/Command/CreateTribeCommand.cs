using Cyrus.Core.DomainModels;
using MediatR;

namespace Cyrus.Core.DomainServices.Command
{
    public class CreateTribeCommand
        : BaseRequest,
        IAsyncRequest<Tribe>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
