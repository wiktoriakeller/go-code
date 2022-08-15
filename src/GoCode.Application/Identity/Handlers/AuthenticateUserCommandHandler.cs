﻿using GoCode.Application.Contracts.Identity;
using GoCode.Application.Dto.BaseResponse;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;
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

        public async Task<Response<AuthenticateUserResponse>> Handle(AuthenticateUserCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _identityService.AuthenticateUserAync(request);
            return response;
        }
    }
}
