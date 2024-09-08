

namespace Shared.DTO.Common.Access
{
    public class LoginResponse<TResult>: Response<TResult>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
