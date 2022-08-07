﻿using GoCode.Application.Dtos;
using GoCode.Application.Dtos.Responses;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Contracts.Identity;
using MediatR;

namespace GoCode.Application.Identity.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<CreateUserResponse>>
    {
        private readonly IIdentityService _identitySerivce;

        public CreateUserCommandHandler(IIdentityService identitySerivce)
        {
            _identitySerivce = identitySerivce;
        }

        public async Task<Response<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _identitySerivce.CreateUserAsync(request);
            return response;
        }
    }
}