
namespace Shared.DTO.Common
{
    public class PaginationResponse<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TResult Data { get; set; }
        public int? StatusCode { get; set; }



        public PaginationResponse(TResult data, string message = "Operation Successfull", int? statusCode = null)
        {
            Data = data;
            Success = true;
            Message = message;
            StatusCode = statusCode;
        }

        public PaginationResponse(string message, int? statusCode = null)
        {
            Message = message;
            Success = false;
            StatusCode = statusCode;
        }

        public PaginationResponse()
        {

        }


    }
}
