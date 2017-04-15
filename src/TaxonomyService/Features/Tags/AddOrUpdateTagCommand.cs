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
    public class AddOrUpdateTagCommand
    {
        public class AddOrUpdateTagRequest : IRequest<AddOrUpdateTagResponse>
        {
            public TagApiModel Tag { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateTagResponse { }

        public class AddOrUpdateTagHandler : IAsyncRequestHandler<AddOrUpdateTagRequest, AddOrUpdateTagResponse>
        {
            public AddOrUpdateTagHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateTagResponse> Handle(AddOrUpdateTagRequest request)
            {
                var entity = await _context.Tags
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Tag.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Tags.Add(entity = new Tag() { TenantId = tenant.Id });
                }

                entity.Name = request.Tag.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateTagResponse();
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
