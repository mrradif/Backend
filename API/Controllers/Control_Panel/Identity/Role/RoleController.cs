using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Identity.Role;
using Shared.DTO.Control_Panel.Identity.Role.Create;

namespace API.Controllers.Control_Panel.Identity.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepo _roleService;

        public RolesController(IRoleRepo roleService)
        {
            _roleService = roleService;
        }



        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequestDto createRoleDto)
        {
            var result = await _roleService.CreateRoleAsync(createRoleDto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRangeRoles([FromBody] IEnumerable<CreateRoleRequestDto> roles)
        {
            var result = await _roleService.AddRangeAsync(roles);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPost("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto updateRoleDto)
        {
            var result = await _roleService.UpdateRoleAsync(updateRoleDto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var result = await _roleService.DeleteRoleAsync(roleName);
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
