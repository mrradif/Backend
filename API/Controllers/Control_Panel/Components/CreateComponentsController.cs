using BLL.Service.Control_Panel.Component_Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Administration.Component;

namespace API.Controllers.Control_Panel.Components
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateComponentsController : ControllerBase
    {
        private readonly CreateComponentService _createComponentService;

        public CreateComponentsController(CreateComponentService createComponentService)
        {
            _createComponentService = createComponentService ?? throw new ArgumentNullException(nameof(createComponentService));
        }

        // Add a single component
        [HttpPost("add")]
        public async Task<IActionResult> AddComponent([FromBody] CreateComponentRequestDto createComponentDto)
        {
            var response = await _createComponentService.AddComponentAsync(createComponentDto);
            return Ok(response);
        }

        // Add a single component with existence checks
        [HttpPost("add-with-exists")]
        public async Task<IActionResult> AddComponentWithExists([FromBody] CreateComponentRequestDto createComponentDto)
        {
            var response = await _createComponentService.AddComponentWithExistsAsync(createComponentDto);
            return Ok(response);
        }

        // Add multiple components
        [HttpPost("add-range")]
        public async Task<IActionResult> AddComponents([FromBody] IEnumerable<CreateComponentRequestDto> createComponentDtos)
        {
            var response = await _createComponentService.AddComponentsAsync(createComponentDtos);

            return Ok(response);
        }
    }
}
