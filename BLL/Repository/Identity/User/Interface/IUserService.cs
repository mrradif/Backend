

namespace BLL.Repository.Identity.User.Interface
{
    public interface IUserService
    {
        Task<bool> CheckUsernameExistsAsync(string username);
    }
}
