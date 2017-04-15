using MediatR;
using TaxonomyService.Data;
using TaxonomyService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TaxonomyService.Features.Categories
{
    public class GetCategoryByIdQuery
    {
        public class GetCategoryByIdRequest : IRequest<GetCategoryByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetCategoryByIdResponse
        {
            public CategoryApiModel Category { get; set; } 
        }

        public class GetCategoryByIdHandler : IAsyncRequestHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
        {
            public GetCategoryByIdHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request)
            {                
                return new GetCategoryByIdResponse()
                {
                    Category = CategoryApiModel.FromCategory(await _context.Categories
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
