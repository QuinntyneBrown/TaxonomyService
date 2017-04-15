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
    public class GetCategoriesQuery
    {
        public class GetCategoriesRequest : IRequest<GetCategoriesResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetCategoriesResponse
        {
            public ICollection<CategoryApiModel> Categories { get; set; } = new HashSet<CategoryApiModel>();
        }

        public class GetCategoriesHandler : IAsyncRequestHandler<GetCategoriesRequest, GetCategoriesResponse>
        {
            public GetCategoriesHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCategoriesResponse> Handle(GetCategoriesRequest request)
            {
                var categories = await _context.Categories
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetCategoriesResponse()
                {
                    Categories = categories.Select(x => CategoryApiModel.FromCategory(x)).ToList()
                };
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
