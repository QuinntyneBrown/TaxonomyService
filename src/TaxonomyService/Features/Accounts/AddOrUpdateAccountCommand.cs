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
    public class AddOrUpdateAccountCommand
    {
        public class AddOrUpdateAccountRequest : IRequest<AddOrUpdateAccountResponse>
        {
            public AccountApiModel Account { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateAccountResponse { }

        public class AddOrUpdateAccountHandler : IAsyncRequestHandler<AddOrUpdateAccountRequest, AddOrUpdateAccountResponse>
        {
            public AddOrUpdateAccountHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateAccountResponse> Handle(AddOrUpdateAccountRequest request)
            {
                var entity = await _context.Accounts
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Account.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Accounts.Add(entity = new Account() { TenantId = tenant.Id });
                }

                entity.Firstname = request.Account.Firstname;

                entity.Lastname = request.Account.Lastname;

                entity.Email = request.Account.Email;

                await _context.SaveChangesAsync();

                return new AddOrUpdateAccountResponse();
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
