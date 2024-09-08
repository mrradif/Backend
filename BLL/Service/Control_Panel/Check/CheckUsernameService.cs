using BLL.Repository.Identity.User.Interface;
using DAL.Context.Control_Panel;
using DAL.Service.Logger;


namespace BLL.Service.Control_Panel.Check
{
    public class CheckUsernameService
    {
        private readonly IUserService _userService;

        public CheckUsernameService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            try
            {
                return await _userService.CheckUsernameExistsAsync(username);
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                throw;
            }
        }
    }
}
