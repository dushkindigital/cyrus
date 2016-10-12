using System.Threading.Tasks;
using Cyrus.Core.DomainModels;
using Cyrus.Core.DomainServices;
using Cyrus.Core.DomainServices.Command;
using Cyrus.Data;
using Cyrus.Data.Extensions;
using Mehdime.Entity;
using MediatR;

namespace Cyrus.Services.Command
{
    public class CreateTribeCommandHandler : IDatabaseService, IAsyncRequestHandler<CreateTribeCommand, Tribe>
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        
        public CreateTribeCommandHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public async Task<Tribe> Handle(CreateTribeCommand command)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<ICyrusDbContext>();

                // Map our command to a new tribe entity. We purposely don't use automapping for this. We want to control our mapping in a 1 to 1 manner
                var domainModel = new Tribe
                {
                    Name = command.Name,
                    Description = command.Description
                };

                dbCtx.Tribes.Add(domainModel);

                await dbContextScope.SaveChangesAsync();

                // This tribe will have the Id field populated.
                return domainModel;
            }
        }

    }

}