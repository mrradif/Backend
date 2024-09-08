using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Identity.Role;

namespace API.Controllers.Control_Panel.Identity.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleClaimsController : ControllerBase
    {
        private readonly IRoleRepo _roleService;

        public RoleClaimsController(IRoleRepo roleService)
        {
            _roleService = roleService;
        }


        [HttpGet("GetRolesWithClaims")]
        public async Task<IActionResult> GetRolesWithClaims()
        {
            var response = await _roleService.GetRolesWithClaimsAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("AddRoleClaims")]
        // [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AddRoleClaims([FromBody] RoleClaimsDto roleClaimsDto)
        {
            var result = await _roleService.AddRoleClaimsAsync(roleClaimsDto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("UpdateRoleClaims")]
        // [Authorize(Policy = "RequireSystemAdminRole")]
        public async Task<IActionResult> UpdateRoleClaims([FromBody] RoleClaimsDto roleClaimsDto)
        {
            var result = await _roleService.UpdateRoleClaimsAsync(roleClaimsDto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("RemoveRoleClaims")]
        public async Task<IActionResult> RemoveRoleClaims([FromBody] RoleClaimsDto roleClaimsDto)
        {
            var result = await _roleService.RemoveRoleClaimsAsync(roleClaimsDto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
