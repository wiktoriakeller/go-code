using GoCode.Application.Common.BaseResponse;
using MediatR;

namespace GoCode.Application.Common
{
    public interface IHandlerWrapper<TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
        where TRequest : IRequest<Response<TResponse>>
    {
    }
}
