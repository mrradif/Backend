using BLL.Service.Control_Panel.Applications.Create;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Application;

namespace API.Controllers.Control_Panel.Applications.Create
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateApplicationController : ControllerBase
    {
        private readonly CreateApplicationService _createApplicationService;

        public CreateApplicationController(
            CreateApplicationService createApplicationService
            )
        {
            _createApplicationService = createApplicationService;
        }



        // ..................................................................................
        // ................................................................ Add Applications
        // ............................................ Starting


        // .............................................. Add Single
        [HttpPost]
        [Route("AddApplication")]
        public async Task<IActionResult> AddApplication([FromBody] CreateApplicationRequestDto createApplicationDto)
        {
            var response = await _createApplicationService.AddApplicationAsync(createApplicationDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        // .............................................. Add Single With Exists Check
        [HttpPost]
        [Route("AddApplicationWithExists")]
        public async Task<IActionResult> AddApplicationWithExists([FromBody] CreateApplicationRequestDto createApplicationDto)
        {
            var response = await _createApplicationService.AddApplicationWithExistsAsync(createApplicationDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        // ............................................. Add Range
        [HttpPost]
        [Route("AddApplications")]
        public async Task<IActionResult> AddApplications([FromBody] IEnumerable<CreateApplicationRequestDto> createApplicationDtos)
        {
            var response = await _createApplicationService.AddApplicationsAsync(createApplicationDtos);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        // ............................................ End
        // ................................................................ Add Applications


    }
}
