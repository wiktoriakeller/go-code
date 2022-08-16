using GoCode.Application.Identity.Dto;

namespace GoCode.Application.Common.Contracts.Identity
{
    public interface ICurrentUserService
    {
        UserDto? User { get; }
    }
}
