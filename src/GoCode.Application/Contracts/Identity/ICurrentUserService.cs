using GoCode.Application.Identity.Dto;

namespace GoCode.Application.Contracts.Identity
{
    public interface ICurrentUserService
    {
        UserDto? User { get; }
    }
}
