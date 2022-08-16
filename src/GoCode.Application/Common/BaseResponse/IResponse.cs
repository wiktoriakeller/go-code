using System.Net;

namespace GoCode.Application.Common.BaseResponse
{
    public interface IResponse
    {
        List<string> Errors { get; }
        HttpStatusCode HttpStatusCode { get; }
        ResponseError ResponseError { get; }
        bool Succeeded { get; }

        void AddErrorMessage(IEnumerable<string> messages);

        void AddErrorMessage(string message);
    }
}
