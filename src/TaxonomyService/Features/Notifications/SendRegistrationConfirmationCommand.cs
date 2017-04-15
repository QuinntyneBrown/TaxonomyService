using TaxonomyService.Data;
using TaxonomyService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;

namespace TaxonomyService.Features.Notifications
{
    public class SendRegistrationConfirmationCommand
    {
        public class SendRegistrationConfirmationRequest : IRequest<SendRegistrationConfirmationResponse>
        {
            public Guid TenantUniqueId { get; set; }
        }

        public class SendRegistrationConfirmationResponse { }

        public class SendRegistrationConfirmationHandler : IAsyncRequestHandler<SendRegistrationConfirmationRequest, SendRegistrationConfirmationResponse>
        {
            public SendRegistrationConfirmationHandler(TaxonomyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<SendRegistrationConfirmationResponse> Handle(SendRegistrationConfirmationRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly TaxonomyServiceContext _context;
            private readonly ICache _cache;
        }
    }
}