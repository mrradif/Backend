using BLL.Repository.Identity.User.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Identity.User;
using Shared.DTO.Control_Panel.Identity.User.Shared.DTO.Control_Panel.Identity.User;

namespace API.Controllers.Control_Panel.Identity.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimsController : ControllerBase
    {
        private readonly IUserRepo _userService;

        public UserClaimsController(IUserRepo userService)
        {
            _userService = userService;
        }



        [HttpPost("AddClaims")]
        public async Task<IActionResult> AddClaims([FromBody] AddClaimDto model)
        {
            var result = await _userService.AddClaimsAsync(model);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }



        [HttpGet("UserClaims")]
        public async Task<IActionResult> GetUserClaims([FromQuery] string userId)
        {
            var response = await _userService.GetUserClaimsAsync(userId);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }


        [HttpGet("UserRolesAndClaims")]
        public async Task<IActionResult> GetUserWithRolesAndClaims([FromQuery] string userId)
        {
            var response = await _userService.GetUserWithRolesAndClaimsAsync(userId);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }



        [HttpGet("UserClaimsAndRolesWithRoleClaims")]
        public async Task<IActionResult> GetUserWithRolesAndRolesClaims([FromQuery] string userId)
        {
            var response = await _userService.GetUserWithRolesAndRolesClaimsAsync(userId);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }




    }
}
