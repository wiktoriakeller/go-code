using System.Net;

namespace GoCode.Application.Dto.BaseResponse
{
    public interface IResponse<T>
    {
        List<string> Errors { get; }
        HttpStatusCode HttpStatusCode { get; }
        ResponseError ResponseError { get; }
        bool Succeeded { get; }
        T? Data { get; }

        void AddErrorMessage(IEnumerable<string> messages);

        void AddErrorMessage(string message);
    }
}
