using BLL.Service.Control_Panel.Modules.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Modules;

namespace API.Controllers.Control_Panel.Modules.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateModuleController : ControllerBase
    {
        private readonly UpdateModuleService _updateModuleService;

        public UpdateModuleController(
             UpdateModuleService updateModuleService
            )
        {
            _updateModuleService = updateModuleService;
        }



        // ...........................................................................................
        // .................................................................... Udpate Module
        // .................................................... Starting


        // ........................................ Udpate Single
        [HttpPut("UpdateModule")]
        public async Task<IActionResult> UpdateModule([FromBody] UpdateModuleRequestDto updateModuleDto)
        {
            var response = await _updateModuleService.UpdateModuleAsync(updateModuleDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        // ............................................. Update Range
        [HttpPut]
        [Route("UpdateModules")]
        public async Task<IActionResult> UpdateModules([FromBody] IEnumerable<UpdateModuleRequestDto> updateModuleDtos)
        {
            var response = await _updateModuleService.UpdateModulesAsync(updateModuleDtos);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        // .................................................... End
        // .................................................................... Udpate Module
    }
}
