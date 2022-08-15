using System.Net;

namespace GoCode.Application.Dto.BaseResponse
{
    public static class ResponseResult
    {
        public static Response<T> Ok<T>(T value) => new(value, HttpStatusCode.OK);

        public static Response<T> Created<T>(T value) => new(value, HttpStatusCode.Created);

        public static Response<T> Updated<T>(T value) => new(value, HttpStatusCode.OK);

        public static Response<T> Deleted<T>(T value) => new(value, HttpStatusCode.OK);

        public static Response<T> NotFound<T>(IEnumerable<string> errors) => new(errors, ResponseError.NotFound, HttpStatusCode.BadRequest);

        public static Response<T> NotFound<T>(string error) => new(error, ResponseError.NotFound, HttpStatusCode.BadRequest);

        public static Response<T> ValidationError<T>(IEnumerable<string> errors) => new(errors, ResponseError.ValidationError, HttpStatusCode.BadRequest);

        public static Response<T> ValidationError<T>(string error) => new(error, ResponseError.ValidationError, HttpStatusCode.BadRequest);

        public static Response<T> HttpError<T>(IEnumerable<string> errors, HttpStatusCode httpStatusCode) =>
            new(errors, ResponseError.HttpError, httpStatusCode);

        public static Response<T> HttpError<T>(string error, HttpStatusCode httpStatusCode) =>
            new(error, ResponseError.HttpError, httpStatusCode);

        public static Response<T> Fail<T>(string error) => new(error, ResponseError.HttpError, HttpStatusCode.InternalServerError);

        public static Response<T> Unknown<T>(string error) => new(error, ResponseError.Unknown, HttpStatusCode.InternalServerError);
    }
}
