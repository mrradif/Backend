using BLL.Service.Control_Panel.Button_Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Administration.Button;

namespace API.Controllers.Control_Panel
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateButtonsController : ControllerBase
    {
        private readonly CreateButtonService _createButtonService;

        public CreateButtonsController(CreateButtonService createButtonService)
        {
            _createButtonService = createButtonService ?? throw new ArgumentNullException(nameof(createButtonService));
        }

        // Add a single button
        [HttpPost("add")]
        public async Task<IActionResult> AddButton([FromBody] CreateButtonRequestDto createButtonDto)
        {
            var response = await _createButtonService.AddButtonAsync(createButtonDto);
            return Ok(response);
        }

        // Add a single button with existence checks
        [HttpPost("add-with-exists")]
        public async Task<IActionResult> AddButtonWithExists([FromBody] CreateButtonRequestDto createButtonDto)
        {
            var response = await _createButtonService.AddButtonWithExistsAsync(createButtonDto);
            return Ok(response);
        }

        // Add multiple buttons
        [HttpPost("add-range")]
        public async Task<IActionResult> AddButtons([FromBody] IEnumerable<CreateButtonRequestDto> createButtonDtos)
        {
            var response = await _createButtonService.AddButtonsAsync(createButtonDtos);
            return Ok(response);
        }
    }
}
