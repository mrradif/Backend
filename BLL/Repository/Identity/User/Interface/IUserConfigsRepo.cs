

using Shared.DTO.Common;

namespace BLL.Repository.Identity.User.Interface
{
    public interface IUserConfigsRepo
    {
        Task<Response<bool>> AddTwoFactorEnableToUserAsync(string userId);
        Task<Response<bool>> AddTwoFactorEnableToUsersAsync(IEnumerable<string> userIds);

        Task<Response<bool>> RemoveTwoFactorEnableToUserAsync(string userId);
        Task<Response<bool>> RemoveTwoFactorEnableToUsersAsync(IEnumerable<string> userIds);


        Task<Response<bool>> AddLockoutEnableToUserAsync(string userId);
        Task<Response<bool>> AddLockoutEnableToUsersAsync(IEnumerable<string> userIds);

        Task<Response<bool>> RemoveLockoutEnableToUserAsync(string userId);
        Task<Response<bool>> RemoveLockoutEnableToUsersAsync(IEnumerable<string> userIds);
    }
}
