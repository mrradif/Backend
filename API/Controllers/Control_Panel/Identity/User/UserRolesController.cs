using BLL.Repository.Identity.User.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Identity.User;

namespace API.Controllers.Control_Panel.Identity.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRepo _userService;

        public UserRolesController(IUserRepo userService)
        {
            _userService = userService;
        }





        [HttpPost("AssignRoles")]
        public async Task<IActionResult> AssignRoles([FromBody] UserRoleAssignDto model)
        {
            var result = await _userService.UserRoleAssignAsync(model);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }


        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles([FromQuery] string userId)
        {
            var result = await _userService.GetUserRolesAsync(userId);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(500, result.Message);
            }
        }


    }
}
