using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;
using Cyrus.Core.DomainModels;
using Cyrus.Core.DomainServices.Command;
using Cyrus.Core.DomainServices.Query;
using Cyrus.WebApi.ViewModels;
using MediatR;

namespace Cyrus.WebApi.Controllers
{
    //[Authorize]
    [RoutePrefix("api/v1")]
    public class TribeController : ApiController
    {
        //private readonly IService<Tribe> _tribeService;
        private readonly IMediator _mediator;

        public TribeController(IMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator), "mediator");
            _mediator = mediator;
        }

        // GET api/v1/tribes
        [HttpGet]
        [Route("tribes")]
        public async Task<IEnumerable<Tribe>> Get()
        {
            // Example of calling the paginated result
            // var pagedTribes = _mediator.Send(new PaginateQuery<Tribe>(2,3,orderBy: x=>x.OrderBy(c=>c.Name)));

            // Depending on your design decisions, you might want to limit the amount of results that can be queried.
            // We made our services require a "Take" amount. You may not want to adopt this approach.

            var tribes = await _mediator.SendAsync(new AsyncGenericQuery<Tribe>(20));

            return tribes;

        }

        // GET api/v1/tribes/1
        [HttpGet]
        [Route("tribes/{id}")]
        public async Task<IEnumerable<Tribe>> Get(int id)
        {
            // Example of calling the paginated result
            // var pagedTribes = _mediator.Send(new PaginateQuery<Tribe>(2, orderBy: x=>x.OrderBy(c=>c.Name)));

            // Depending on your design decisions, you might want to limit the amount of results that can be queried.
            // We made our services require a "Take" amount. You may not want to adopt this approach.

            var tribe = await _mediator.SendAsync(new AsyncGenericQuery<Tribe>(
                null, orderBy: x => x.OrderBy(c => c.Name), predicate: u => u.UserId==id));

            return tribe;

        }

        // This method is also an example of a webapi which doesn't want to expose the domain model. So tribes are created with a specific 
        // view model,and so we need to perform fluent validation on that view model. It may seem redundant to validate this model, then our 
        // service validates the command as well (which is the same in this case). But depending on your implementation, your service might 
        // allow more configurations, or it could be an "CreateOrUpdate" service, and so you would want to do some preliminary validation
        // here before calling the service. So really I'm just trying to show all potential ways to use these features. Your project architecture, 
        // complexity and code style/conventions might favor one more than the other, but the building blocks are here.


        // POST api/v1/tribes
        [HttpPost]
        [Route("tribes")]
        public async Task<int> Post([FromBody] CreateOrUpdateTribeViewModel model)
        {
            var command = new CreateTribeCommand
            {
                Name = model.Name,
                Description = model.Description
            };

            var tribe = await _mediator.SendAsync(command);

            return tribe.Id;
        }

        public async Task<Tribe> Put(int id, [FromBody] CreateOrUpdateTribeViewModel model)
        {
            var command = new UpdateTribeCommand
            {
                Id = id,
                Name = model.Name,
                Description = model.Description
            };

            var tribe = await _mediator.SendAsync(command);

            return tribe;
        }

        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // Normally this would signify deleting a student, and we would make a route attribute for DropAllTribes, 
        // but for the sake of this example, we are hijacking this functionality, and making it use the drop all courses

        // PUT api/v1/tribes
        [HttpDelete]
        [Route("tribes")]
        public IHttpActionResult Delete(int id)
        {
            var command = new RemoveTribeCommand
            {
                TribeId = id
            };

            _mediator.Send(command);

            return Ok();
        }

    }

}

