﻿using GoCode.Application.Common;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;

namespace GoCode.Application.Identity.Handlers
{
    public class AuthenticateUserCommandHandler : IHandlerWrapper<AuthenticateUserCommand, AuthenticateUserResponse>
    {
        private readonly IIdentityService _identityService;

        public AuthenticateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Response<AuthenticateUserResponse>> Handle(AuthenticateUserCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _identityService.AuthenticateUserAync(request);
            return response;
        }
    }
}
