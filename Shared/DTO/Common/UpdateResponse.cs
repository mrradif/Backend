namespace Shared.DTO.Common
{
    public class UpdateResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
