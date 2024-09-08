
using BLL.Repository.Identity.User.Interface;
using DAL.Context.Control_Panel;
using DAL.Service.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Identity;
using Shared.DTO.Control_Panel.Identity.User.Update;


namespace API.Controllers.Control_Panel.Identity.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userService;

        public UsersController(IUserRepo userService)
        {
            _userService = userService;
        }


        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<ApplicationUserDto>>> GetAllUsers()
        {
            var usersDto = await _userService.GetAllUsersAsync();
            return Ok(usersDto);
        }




        [HttpPost("GetUsersWithCriteria")]
        public async Task<IActionResult> GetAllUsers([FromBody] UserSearchCriteriaDto searchCriteria)
        {
            var response = await _userService.GetAllUsersAsync(searchCriteria);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] CreateApplicationUserDto userDto)
        {
            try
            {
                var response = await _userService.AddUserAsync(userDto);

                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex); 
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateApplicationUserDto userDto)
        {
            var response = await _userService.UpdateUserAsync(userDto);

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }




        [HttpPost("VerifyTwoFactor")]
        public async Task<IActionResult> VerifyTwoFactorToken([FromBody] TwoFactorVerificationDto verificationDto)
        {
            var response = await _userService.VerifyTwoFactorTokenAsync(verificationDto);

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            if (token == null || email == null)
                return BadRequest("Invalid token or email.");

            var result = await _userService.ConfirmEmailAsync(email, token);

            return Ok(result);
        }



        [HttpGet("ResendEmailConfirmation")]
        public async Task<IActionResult> ResendEmailConfirmation(string email)
        {
            var result = await _userService.ResendEmailConfirmationAsync(email);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }






        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var user = await Task.FromResult(UserHelper.AppUser());

                if (user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
