using GoCode.Application.Dtos;
using GoCode.Application.Dtos.Responses;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Contracts.Identity;
using MediatR;

namespace GoCode.Application.Identity.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<RefreshTokenResponse>>
    {
        private readonly IIdentityService _identityService;

        public RefreshTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public Task<Response<RefreshTokenResponse>> Handle(RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}