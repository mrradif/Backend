using BLL.Repository.Identity.User.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Control_Panel.Identity.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserConfigsController : ControllerBase
    {
        private readonly IUserConfigsRepo _userConfigsRepo;

        public UserConfigsController(IUserConfigsRepo userConfigsRepo)
        {
            _userConfigsRepo = userConfigsRepo;
        }




        [HttpPost("AddTwoFactorEnableToUser")]
        public async Task<IActionResult> AddTwoFactorEnableToUserAsync([FromQuery] string userId)
        {

            var response = await _userConfigsRepo.AddTwoFactorEnableToUserAsync(userId);
            return Ok(response);

        }

        [HttpPost("AddTwoFactorEnableToUsers")]
        public async Task<IActionResult> AddTwoFactorEnableToUsersAsync(IEnumerable<string> userIds)
        {

            var response = await _userConfigsRepo.AddTwoFactorEnableToUsersAsync(userIds);
            return Ok(response);

        }



        [HttpPost("RemoveTwoFactorEnableToUser")]
        public async Task<IActionResult> RemoveTwoFactorEnableToUserAsync([FromQuery] string userId)
        {

            var response = await _userConfigsRepo.RemoveTwoFactorEnableToUserAsync(userId);
            return Ok(response);

        }

        [HttpPost("RemoveTwoFactorEnableToUsers")]
        public async Task<IActionResult> RemoveTwoFactorEnableToUsersAsync(IEnumerable<string> userIds)
        {

            var response = await _userConfigsRepo.RemoveTwoFactorEnableToUsersAsync(userIds);
            return Ok(response);

        }





        [HttpPost("AddLockoutToUser")]
        public async Task<IActionResult> AddLockoutEnableToUserAsync([FromQuery] string userId)
        {

            var response = await _userConfigsRepo.AddLockoutEnableToUserAsync(userId);
            return Ok(response);

        }

        [HttpPost("AddLockoutToUsers")]
        public async Task<IActionResult> AddLockoutEnableToUsersAsync(IEnumerable<string> userIds)
        {

            var response = await _userConfigsRepo.AddLockoutEnableToUsersAsync(userIds);
            return Ok(response);

        }



        [HttpPost("RemoveLockoutToUser")]
        public async Task<IActionResult> RemoveLockoutEnableToUserAsync([FromQuery] string userId)
        {

            var response = await _userConfigsRepo.RemoveLockoutEnableToUserAsync(userId);
            return Ok(response);

        }

        [HttpPost("RemoveLockoutToUsers")]
        public async Task<IActionResult> RemoveLockoutEnableToUsersAsync(IEnumerable<string> userIds)
        {

            var response = await _userConfigsRepo.RemoveLockoutEnableToUsersAsync(userIds);
            return Ok(response);

        }


    }
}
