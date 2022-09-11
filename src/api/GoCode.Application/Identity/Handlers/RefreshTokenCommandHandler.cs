using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;

namespace GoCode.Application.Identity.Handlers
{
    public class RefreshTokenCommandHandler : IHandlerWrapper<RefreshTokenCommand, RefreshTokenResponse>
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
