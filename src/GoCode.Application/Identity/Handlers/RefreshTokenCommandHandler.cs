using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dto.BaseResponse;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;
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

        public async Task<Response<RefreshTokenResponse>> Handle(RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _identityService.RefreshTokenAsync(request);
            return response;
        }
    }
}
