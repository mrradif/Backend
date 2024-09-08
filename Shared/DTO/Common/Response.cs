
namespace Shared.DTO.Common
{
    public class Response<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TResult Data { get; set; }


    }
}
