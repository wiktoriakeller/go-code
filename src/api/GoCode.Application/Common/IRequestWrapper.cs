using GoCode.Application.Common.BaseResponse;
using MediatR;

namespace GoCode.Application.Common
{
    public interface IRequestWrapper<T> : IRequest<Response<T>>
    {
    }
}
