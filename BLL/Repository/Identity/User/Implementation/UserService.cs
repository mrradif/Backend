using BLL.Repository.Identity.User.Interface;
using Microsoft.AspNetCore.Identity;


namespace BLL.Repository.Identity.User.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return (user != null);
        }
    }
}
