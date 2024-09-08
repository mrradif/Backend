using BLL.Service.Control_Panel.User_Creation_Config_Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Administration.Create.User_Creation_Config;

namespace API.Controllers.Control_Panel.Administration.User_Creation_Config
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCreationConfigsController : ControllerBase
    {
        private readonly GetCreateUserCreationConfigService _userCreationService;
        private readonly GetUserCreationConfigService _userCreateConfigSingleService;

        public UserCreationConfigsController(GetCreateUserCreationConfigService userCreationService, GetUserCreationConfigService userCreateConfigSingleService)
        {
            _userCreationService = userCreationService;
            _userCreateConfigSingleService = userCreateConfigSingleService;
        }


        [HttpPost]
        [Route("AddUserCreationConfig")]
        public async Task<IActionResult> AddCompany([FromBody] CreateUserCreationConfigDto createUserCreationConfigDto)
        {
            var response = await _userCreationService.AddUserCreationConfigAsync(createUserCreationConfigDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet]
        [Route("GetUserCreationConfig")]
        public async Task<IActionResult> GetUserCreationConfig()
        {
            var result = await _userCreateConfigSingleService.GetActiveUserConfigAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }



    }
}
