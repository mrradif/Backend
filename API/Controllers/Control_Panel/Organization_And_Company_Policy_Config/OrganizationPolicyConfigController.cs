using BLL.Service.Control_Panel.Organization_Policy_Config_Service;
using BLL.Service.Control_Panel.Update.Policy_Config;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Update.Policy_Config;


namespace API.Controllers.Control_Panel.Organization_And_Company_Policy_Config
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationPolicyConfigController : ControllerBase
    {

        private readonly UpdateOrganizationMasterDetailService _updateOrganizationMasterDetailService;
        private readonly DeleteOrganizationConfigWithCompanyConfigService _deleteOrganizationMasterDetailService;


        private readonly GetOrganizationConfigWithCompanyConfigService _getOrganizationConfigWithCompanyConfigService;

           
        private readonly CreateOrganizationConfigWithCompanyConfigService _createOrganizationConfigWithCompanyConfigService;
        private readonly CreateOrganizationConfigIncludeCompanyConfigService _createOrganizationConfigIncludeCompanyConfigService;


        // single
        private readonly CreateOrganizationPolicyConfigService _createOrganizationPolicyConfigService;


        public OrganizationPolicyConfigController(

            UpdateOrganizationMasterDetailService updateOrganizationMasterDetailService, 
            

            GetOrganizationConfigWithCompanyConfigService getOrganizationConfigWithCompanyConfigService,
            CreateOrganizationConfigWithCompanyConfigService createOrganizationConfigWithCompanyConfigService,
            CreateOrganizationConfigIncludeCompanyConfigService createOrganizationConfigIncludeCompanyConfigService,
            DeleteOrganizationConfigWithCompanyConfigService deleteOrganizationMasterDetailService,


            CreateOrganizationPolicyConfigService createOrganizationPolicyConfigService
            )
        {
            _updateOrganizationMasterDetailService = updateOrganizationMasterDetailService;
            


            _getOrganizationConfigWithCompanyConfigService = getOrganizationConfigWithCompanyConfigService;
            _createOrganizationConfigWithCompanyConfigService = createOrganizationConfigWithCompanyConfigService;
            _createOrganizationConfigIncludeCompanyConfigService = createOrganizationConfigIncludeCompanyConfigService;
            _deleteOrganizationMasterDetailService = deleteOrganizationMasterDetailService;

            _createOrganizationPolicyConfigService = createOrganizationPolicyConfigService;
        }




        [HttpPost]
        [Route("CreateOrganizationConfigWithCompanyConfig")]
        public async Task<IActionResult> CreateOrganizationConfigWithCompanyConfig([FromBody] CreateOrganizationConfigWithCompanyConfigRequestDto request)
        {
            var result = await _createOrganizationConfigWithCompanyConfigService.CreateOrganizationConfigWithCompanyConfigAsync(request.OrganizationDto, request.CompanyDtos);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }



        [HttpPost]
        [Route("CreateOrganizationConfigIncludeompanyConfig")]
        public async Task<IActionResult> CreateOrganizationConfigIncludeompanyConfig([FromBody] CreateOrganizationConfigIncludeCompanyConfigRequestDto organizationDto)
        {
            if (organizationDto.OrganizationDto == null || organizationDto.CompanyDtos == null)
            {
                return BadRequest("Invalid data. Organization and at least one company are required.");
            }

            var response = await _createOrganizationConfigIncludeCompanyConfigService.CreateOrganizationConfigIncludeCompanyConfigAsync(
                organizationDto.OrganizationDto, organizationDto.CompanyDtos);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }







        [HttpGet("GetOrganizationConfigWithCompanyConfig")]
        public async Task<IActionResult> GetMasterWithDetails([FromQuery] long masterId)
        {
            var response = await _getOrganizationConfigWithCompanyConfigService.GetOrganizationConfigWithCompanyConfigAsync(masterId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }






        [HttpPut]
        [Route("UpdateOrganizationWithCompanies")]
        public async Task<IActionResult> UpdateOrganizationWithCompanies([FromBody] UpdateOrganizationWithCompaniesDto updateDto)
        {
            if (updateDto == null || updateDto.OrganizationDto == null || updateDto.CompanyDtos == null)
            {
                return BadRequest("Invalid data.");
            }

            var response = await _updateOrganizationMasterDetailService.UpdateOrganizationWithCompaniesAsync(updateDto.OrganizationDto, updateDto.CompanyDtos);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.Message);
            }
        }




        [HttpDelete("DeleteOrganizationConfigWithCompanyConfig")]
        public async Task<IActionResult> DeleteOrganizationWithCompanies([FromBody] long organizationPolicyConfigId)
        {
            if (organizationPolicyConfigId <= 0)
            {
                return BadRequest("Invalid organization policy config ID.");
            }

            var response = await _deleteOrganizationMasterDetailService.DeleteOrganizationWithCompaniesAsync(organizationPolicyConfigId);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }





        [HttpPost("AddOrganizationPolicyConfig")]
        public async Task<IActionResult> AddOrganizationPolicyConfig(CreateOrganizationPolicyConfigRequestDto createDto)
        {
            var response = await _createOrganizationPolicyConfigService.AddOrganizationPolicyConfigAsync(createDto);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }



        [HttpPost("AddOrganizationPolicyConfigs")]
        public async Task<IActionResult> AddOrganizationPolicyConfigs(IEnumerable<CreateOrganizationPolicyConfigRequestDto> createDtos)
        {
            var response = await _createOrganizationPolicyConfigService.CreateOrganizationPolicyConfigsAsync(createDtos);

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
