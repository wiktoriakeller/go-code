using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Identity.Commands;
using GoCode.Application.Identity.Responses;
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
