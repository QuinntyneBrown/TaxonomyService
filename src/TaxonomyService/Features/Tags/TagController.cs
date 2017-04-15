using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaxonomyService.Features.Core;
using static TaxonomyService.Features.Tags.AddOrUpdateTagCommand;
using static TaxonomyService.Features.Tags.GetTagsQuery;
using static TaxonomyService.Features.Tags.GetTagByIdQuery;
using static TaxonomyService.Features.Tags.RemoveTagCommand;

namespace TaxonomyService.Features.Tags
{
    [Authorize]
    [RoutePrefix("api/tag")]
    public class TagController : ApiController
    {
        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateTagResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateTagRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateTagResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateTagRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetTagsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetTagsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetTagByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetTagByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveTagResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveTagRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
