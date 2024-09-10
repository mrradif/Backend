
namespace Shared.DTO.Common
{
    public class Response<TResult>
    {
        public bool Success { get; set; }
        public int? StatusCode { get; set; }
        public string Message { get; set; }
        public TResult Data { get; set; }


        // Constructor for success cases
        public Response(bool success, int? statusCode, TResult data, string message)
        {
            Success = success;
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }

        // Constructor for failure cases
        public Response(bool success, int? statusCode, string message)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = default;
        }

        // Default constructor
        public Response() { }

    }
}
