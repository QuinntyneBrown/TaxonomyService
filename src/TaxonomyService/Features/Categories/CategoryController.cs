using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaxonomyService.Features.Core;
using static TaxonomyService.Features.Categories.AddOrUpdateCategoryCommand;
using static TaxonomyService.Features.Categories.GetCategoriesQuery;
using static TaxonomyService.Features.Categories.GetCategoryByIdQuery;
using static TaxonomyService.Features.Categories.RemoveCategoryCommand;

namespace TaxonomyService.Features.Categories
{
    [Authorize]
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateCategoryResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateCategoryRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateCategoryResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateCategoryRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetCategoriesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetCategoriesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetCategoryByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetCategoryByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveCategoryResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveCategoryRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
