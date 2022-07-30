namespace GoCode.Application.Dtos
{
    public class Response<T>
    {
        public T? Result { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }

        public Response(bool succeeded, IEnumerable<string> errors, T? result = default)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Result = result;
        }

        public Response(bool succeeded, T? result)
        {
            Succeeded = succeeded;
            Errors = Array.Empty<string>();
            Result = result;
        }
    }
}