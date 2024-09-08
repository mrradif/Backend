using BLL.Service.Control_Panel.Applications.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Application;

namespace API.Controllers.Control_Panel.Applications.Update
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateApplicationController : ControllerBase
    {
        private readonly UpdateApplicationService _updateApplicationService;

        public UpdateApplicationController(
            UpdateApplicationService updateApplicationService
            )
        {
            _updateApplicationService = updateApplicationService;
        }



        // ..................................................................................
        // ................................................................ Udpate Applications
        // ............................................ Starting


        // ............................................. Update Single
        [HttpPut]
        [Route("UpdateApplication")]
        public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationRequestDto updateApplicationDto)
        {
            var response = await _updateApplicationService.UpdateApplicationAsync(updateApplicationDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        // ............................................. Update Range
        [HttpPut]
        [Route("UpdateApplications")]
        public async Task<IActionResult> UpdateApplications([FromBody] IEnumerable<UpdateApplicationRequestDto> updateApplicationDtos)
        {
            var response = await _updateApplicationService.UpdateApplicationsAsync(updateApplicationDtos);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // ............................................ End
        // ................................................................ Update Applications


    }
}
