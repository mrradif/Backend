using BLL.Service.Control_Panel.Modules.Delete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Modules;

namespace API.Controllers.Control_Panel.Modules.Delete
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteModuleController : ControllerBase
    {
        private readonly DeleteModuleService _deleteModuleService;
        public DeleteModuleController(
            DeleteModuleService deleteModuleService
            )
        {
            _deleteModuleService = deleteModuleService;
        }



        // .......................................................................................
        // .................................................................... Delete Modules
        // .............................................. Starting


        // ................................... Delete Single
        [HttpDelete("DeleteModule")]
        public async Task<IActionResult> DeleteModule([FromQuery] long id)
        {
            var response = await _deleteModuleService.DeleteModuleAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // .................................... Delete Range
        [HttpDelete("DeleteModules")]
        public async Task<IActionResult> DeleteModules([FromQuery] IEnumerable<long> ids)
        {
            var response = await _deleteModuleService.DeleteModulesAsync(ids);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // .............................. Delete Single
        [HttpDelete]
        [Route("DeleteModuleWithPredicate")]
        public async Task<IActionResult> DeleteModuleWithPredicate([FromQuery] long id)
        {
            var response = await _deleteModuleService.DeleteModuleWithPredicateAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // .............................. Delete Range
        [HttpDelete]
        [Route("DeleteModulesWithPredicate")]
        public async Task<IActionResult> DeleteModulesWithPredicate([FromQuery] IEnumerable<long> ids)
        {
            var response = await _deleteModuleService.DeleteModulesWithPredicateAsync(ids);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // .............................. Delete Range
        [HttpDelete]
        [Route("DeleteRangeModules")]
        public async Task<IActionResult> DeleteRangeModules([FromBody] IEnumerable<DeleteModuleRequestDto> deleteModuleDtos)
        {
            var response = await _deleteModuleService.DeleteRangeModulesAsync(deleteModuleDtos);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // .................................................... End
        // .................................................................... Delete Modules




    }
}
