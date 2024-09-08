using BLL.Repository.Access.Interface;
using BLL.Repository.Identity.User.Interface;
using BLL.Service.Control_Panel.Check;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Identity;
using Shared.DTO.Login.Create;
using Shared.DTO.Login.Search;
using Shared.DTO.Logout;
using Shared.DTO.RefreshToken;

namespace API.Controllers.Control_Panel.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessRepo _accessRepo;
        private readonly IUserRepo _userService;

        private readonly CheckUsernameService _checkUsernameService;

        public AccessController(IAccessRepo accessRepo, IUserRepo userService, CheckUsernameService checkUsernameService)
        {
            _accessRepo = accessRepo;
            _userService = userService;
            _checkUsernameService = checkUsernameService;
        }



        [HttpPost]
        [Route("CheckUser")]
        public async Task<IActionResult> CheckUsername([FromBody] UserNameSearchCriteriaDto searchDto)
        {

            if (string.IsNullOrEmpty(searchDto.Username))
            {
                return BadRequest("Username is required.");
            }

            bool usernameExists = await _checkUsernameService.CheckUsernameExistsAsync(searchDto.Username);

            return Ok(usernameExists);

        }
    



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _accessRepo.LoginAsync(loginDto);

            return Ok(response);

        }





        [HttpPost("verify-two-factor")]
        public async Task<IActionResult> VerifyTwoFactorToken([FromBody] TwoFactorVerificationDto verificationDto)
        {
            var response = await _userService.VerifyTwoFactorTokenAsync(verificationDto);

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }





        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDto logoutDto)
        {
            if (string.IsNullOrEmpty(logoutDto.RefreshToken))
            {
                return BadRequest("Refresh token is required for logout.");
            }

            var response = await _accessRepo.LogoutAsync(logoutDto);
            
            return Ok(response);
        }




        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto == null || string.IsNullOrEmpty(refreshTokenDto.RefreshToken))
            {
                return BadRequest("Invalid refresh token data");
            }

            var response = await _accessRepo.RefreshTokenAsync(refreshTokenDto);

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
    }
}
