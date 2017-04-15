using MediatR;
using TaxonomyService.Data;
using TaxonomyService.Data.Model;
using TaxonomyService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TaxonomyService.Features.Accounts
{
    public class RemoveProfileCommand
    {
        public class RemoveProfileRequest : IRequest<RemoveProfileResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveProfileResponse { }

        public class RemoveProfileHandler : IAsyncRequestHandler<RemoveProfileRequest, RemoveProfileResponse>
        {
            public RemoveProfileHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveProfileResponse> Handle(RemoveProfileRequest request)
            {
                var profile = await _context.Profiles.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                profile.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveProfileResponse();
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
