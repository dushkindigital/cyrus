using Cyrus.Core.DomainServices.Dto;
using MediatR;

namespace Cyrus.Core.DomainServices.Query
{
    public class AccountUserInfoQuery
        : BaseRequest,
        IRequest<AccountUserInfoDto>
    {
        public int Id { get; set; }
    }

}

