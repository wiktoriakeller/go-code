using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dtos;
using GoCode.Application.Dtos.Responses;
using GoCode.Application.Identity.Commands;
using MediatR;

namespace GoCode.Application.Identity.Handlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Response<AuthenticateUserResponse>>
    {
        private readonly IIdentityService _identityService;

        public AuthenticateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public Task<Response<AuthenticateUserResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var response = _identityService.AuthenticateUserAync(request);
            return response;
        }
    }
}