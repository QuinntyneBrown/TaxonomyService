using MediatR;
using TaxonomyService.Data;
using TaxonomyService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TaxonomyService.Features.Tags
{
    public class GetTagsQuery
    {
        public class GetTagsRequest : IRequest<GetTagsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetTagsResponse
        {
            public ICollection<TagApiModel> Tags { get; set; } = new HashSet<TagApiModel>();
        }

        public class GetTagsHandler : IAsyncRequestHandler<GetTagsRequest, GetTagsResponse>
        {
            public GetTagsHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTagsResponse> Handle(GetTagsRequest request)
            {
                var tags = await _context.Tags
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetTagsResponse()
                {
                    Tags = tags.Select(x => TagApiModel.FromTag(x)).ToList()
                };
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
