using BLL.Service.Control_Panel.Applications.Delete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Application;

namespace API.Controllers.Control_Panel.Applications.Delete
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteApplicationController : ControllerBase
    {
        private readonly DeleteApplicationService _deleteApplicationsService;

        public DeleteApplicationController(
            DeleteApplicationService deleteApplicationsService
            )
        {
            _deleteApplicationsService = deleteApplicationsService;
        }



        // ....................................................................................
        // ................................................................ Delete Application
        // .................................................. Staring


        // .............................. Delete Single
        [HttpDelete]
        [Route("DeleteApplication")]
        public async Task<IActionResult> DeleteApplication([FromQuery] long id)
        {
            var response = await _deleteApplicationsService.DeleteApplicationAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // .............................. Delete Range
        [HttpDelete]
        [Route("DeleteApplications")]
        public async Task<IActionResult> DeleteApplications([FromQuery] IEnumerable<long> ids)
        {
            var response = await _deleteApplicationsService.DeleteApplicationsAsync(ids);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // .............................. Delete Single
        [HttpDelete]
        [Route("DeleteApplicationWithPredicate")]
        public async Task<IActionResult> DeleteApplicationWithPredicate([FromQuery] long id)
        {
            var response = await _deleteApplicationsService.DeleteApplicationWithPredicateAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // .............................. Delete Range
        [HttpDelete]
        [Route("DeleteApplicationsWithPredicate")]
        public async Task<IActionResult> DeleteApplicationsWithPredicate([FromQuery] IEnumerable<long> ids)
        {
            var response = await _deleteApplicationsService.DeleteApplicationsWithPredicateAsync(ids);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // .............................. Delete Range
        [HttpDelete]
        [Route("DeleteRangeApplications")]
        public async Task<IActionResult> DeleteRangeApplications([FromBody] IEnumerable<DeleteApplicationRequestDto> deleteApplicationDtos)
        {
            var response = await _deleteApplicationsService.DeleteRangeApplicationsAsync(deleteApplicationDtos);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // .................................................. End
        // ................................................................ Delete Application



    }
}
