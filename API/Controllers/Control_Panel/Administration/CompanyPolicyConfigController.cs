using BLL.Service.Control_Panel.Create;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Create;

namespace API.Controllers.Control_Panel.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyPolicyConfigController : ControllerBase
    {
        private readonly CreateCompanyPolicyConfigService _service;

        public CompanyPolicyConfigController(CreateCompanyPolicyConfigService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCompanyPolicyConfig(CreateCompanyPolicyConfigRequestDto createDto)
        {
            var response = await _service.AddCompanyPolicyConfigAsync(createDto);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("addRange")]
        public async Task<IActionResult> CreateCompanyPolicyConfigs(IEnumerable<CreateCompanyPolicyConfigRequestDto> createDtos)
        {
            var response = await _service.CreateCompanyPolicyConfigsAsync(createDtos);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
