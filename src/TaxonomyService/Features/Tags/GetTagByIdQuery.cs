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
    public class GetTagByIdQuery
    {
        public class GetTagByIdRequest : IRequest<GetTagByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetTagByIdResponse
        {
            public TagApiModel Tag { get; set; } 
        }

        public class GetTagByIdHandler : IAsyncRequestHandler<GetTagByIdRequest, GetTagByIdResponse>
        {
            public GetTagByIdHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTagByIdResponse> Handle(GetTagByIdRequest request)
            {                
                return new GetTagByIdResponse()
                {
                    Tag = TagApiModel.FromTag(await _context.Tags
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
