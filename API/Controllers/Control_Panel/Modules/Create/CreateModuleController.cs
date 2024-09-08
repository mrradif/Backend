using BLL.Service.Control_Panel.Modules.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Modules;

namespace API.Controllers.Control_Panel.Modules.Create
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateModuleController : ControllerBase
    {
        private readonly CreateModuleService _createModuleService;

        public CreateModuleController(
            CreateModuleService createModuleService
            )
        {
            _createModuleService = createModuleService;
        }


        // ..........................................................................................
        // ............................................................................. Add Modules
        // ..................................................... Starting


        // .............................................. Add Single 
        [HttpPost("AddModule")]
        public async Task<IActionResult> AddModule([FromBody] CreateModuleRequestDto createModuleDto)
        {
            var response = await _createModuleService.AddModuleAsync(createModuleDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // .............................................. Add Single With Exists Check
        [HttpPost]
        [Route("AddModuleWithExists")]
        public async Task<IActionResult> AddModuleWithExists([FromBody] CreateModuleRequestDto createModuleDto)
        {
            var response = await _createModuleService.AddModuleWithExistsAsync(createModuleDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // ............................................. Add Range
        [HttpPost]
        [Route("AddModules")]
        public async Task<IActionResult> AddModules([FromBody] IEnumerable<CreateModuleRequestDto> createModuleDtos)
        {
            var response = await _createModuleService.AddModulesAsync(createModuleDtos);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // ..................................................... End
        // ............................................................................. Add Modules


    }
}
