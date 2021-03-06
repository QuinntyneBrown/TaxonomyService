using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaxonomyService.Features.Core;
using static TaxonomyService.Features.Accounts.AddOrUpdateProfileCommand;
using static TaxonomyService.Features.Accounts.GetProfilesQuery;
using static TaxonomyService.Features.Accounts.GetProfileByIdQuery;
using static TaxonomyService.Features.Accounts.RemoveProfileCommand;

namespace TaxonomyService.Features.Accounts
{
    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateProfileResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateProfileRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateProfileResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateProfileRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetProfilesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetProfilesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetProfileByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetProfileByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveProfileResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveProfileRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
