

using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Identity;
using Shared.DTO.Login.Create;
using Shared.DTO.Logout;
using Shared.DTO.RefreshToken;

namespace BLL.Repository.Access.Interface
{
    public interface IAccessRepo
    {
        Task<Response<LoginDto>> LoginAsync(LoginDto loginDto);
        Task<Response<string>> VerifyTwoFactorTokenAsync(TwoFactorVerificationDto twoFactorVerificationDto);

        Task<Response<bool>> LogoutAsync(LogoutDto logoutDto);

        Task<Response<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    }
}
