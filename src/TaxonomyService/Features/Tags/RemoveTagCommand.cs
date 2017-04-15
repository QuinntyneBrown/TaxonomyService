using MediatR;
using TaxonomyService.Data;
using TaxonomyService.Data.Model;
using TaxonomyService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TaxonomyService.Features.Tags
{
    public class RemoveTagCommand
    {
        public class RemoveTagRequest : IRequest<RemoveTagResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveTagResponse { }

        public class RemoveTagHandler : IAsyncRequestHandler<RemoveTagRequest, RemoveTagResponse>
        {
            public RemoveTagHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveTagResponse> Handle(RemoveTagRequest request)
            {
                var tag = await _context.Tags.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                tag.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveTagResponse();
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
