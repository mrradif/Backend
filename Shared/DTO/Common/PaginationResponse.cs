
namespace Shared.DTO.Common
{
    public class PaginationResponse<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TResult Data { get; set; }


        public PaginationResponse(TResult data, string message = "Operation Successfull")
        {
            Data = data;
            Success = true;
            Message = message;
        }

        public PaginationResponse(string message)
        {
            Message = message;
            Success = false;
        }
    }
}
