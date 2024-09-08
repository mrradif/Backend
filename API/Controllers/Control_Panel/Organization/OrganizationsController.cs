using BLL.Service.Control_Panel.Organization_Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Delete;
using Shared.DTO.Control_Panel.Administration.Organization;
using Shared.DTO.Control_Panel.Administration.Organization.OrganizationWithCompany;
using Shared.DTO.Control_Panel.Administration.Search;
using Shared.DTO.Control_Panel.Administration.Update;

namespace API.Controllers.Control_Panel.Organization
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly GetOrganizationService _getOrganizationService;
        private readonly GetOrganizationIncludesCompanyService _getOrganizationIncludeCompanyService;
        private readonly GetOrganzationWithCompanyService _getOrganzationWithCompanyService;

        private readonly CreateOrganizationWithFileService _createOrganizationWithFileService;
        private readonly CreateOrganizationService _createOrganizationService;

        private readonly UpdateOrganizationService _updateOrganizationService;
        private readonly DeleteOrganizationsService _deleteOrganizationsService;
        private readonly RemoveOrganizationService _removeOrganizationService;
        private readonly DeleteOrganizationWithCompanyService _deleteOrganizationWithCompanyService;

        private readonly CreateOrganizationWithIncludeCompanyService _createOrganizationWithIncludeCompanyService;

        public OrganizationsController(
            GetOrganizationService getOrganizationService,
            GetOrganizationIncludesCompanyService getOrganizationIncludeCompanyService,
            GetOrganzationWithCompanyService getOrganzationWithCompanyService,

            CreateOrganizationWithFileService createOrganizationWithFileService,
            CreateOrganizationService createOrganizationService,

            UpdateOrganizationService updateOrganizationService,
            RemoveOrganizationService removeOrganizationService,
            DeleteOrganizationsService deleteOrganizationsService,
            DeleteOrganizationWithCompanyService deleteOrganizationWithCompanyService,
            CreateOrganizationWithIncludeCompanyService createOrganizationWithIncludeCompanyService
            )
        {
            _getOrganizationService = getOrganizationService;
            _getOrganizationIncludeCompanyService = getOrganizationIncludeCompanyService;
            _getOrganzationWithCompanyService = getOrganzationWithCompanyService;

            _createOrganizationWithFileService = createOrganizationWithFileService;
            _createOrganizationService = createOrganizationService;

            _updateOrganizationService = updateOrganizationService;
            _removeOrganizationService = removeOrganizationService;
            _deleteOrganizationsService = deleteOrganizationsService;
            _deleteOrganizationWithCompanyService = deleteOrganizationWithCompanyService;
            _createOrganizationWithIncludeCompanyService = createOrganizationWithIncludeCompanyService;

        }



        [HttpGet("GetAllOrganizations")]
        public async Task<IActionResult> GetAllOrganizations()
        {
            var response = await _getOrganizationService.GetAllOrganizationsAsync();

           return Ok(response);
        }


        [HttpGet("GetOrganizations")]
        public async Task<IActionResult> GetOrganizations()
        {
            var response = await _getOrganizationService.GetOrganizationsAsync();

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }




        [HttpGet("GetALLOrganizationsWithCompanies")]
        public async Task<IActionResult> GetOrganizationsWithCompanies()
        {
            var response = await _getOrganizationIncludeCompanyService.GetAllOrganizationsWithCompaniesAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        [HttpGet("GetAllOrganizationsWithCriteria")]
        public async Task<IActionResult> GetOrganizationsByCriteria([FromQuery] OrganizationSearchCriteria criteria)
        {
            var response = await _getOrganizationService.GetOrganizationsByCriteriaAsync(criteria);

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }




        [HttpGet("GetAllOrganizationsWithOrderByCriteria")]
        public async Task<IActionResult> GetAllOrganizationsWithOrderByCriteria([FromQuery] OrganizationSearchCriteria criteria)
        {
            var response = await _getOrganizationService.GetOrganizationsWithOrderingAsync(criteria);

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }




        [HttpGet("GetAllOrganizationsWithCompaniesAndPredicate")]
        public async Task<IActionResult> GetAllOrganizationsWithCompaniesAndPredicate([FromQuery] OrganizationSearchCriteria criteria)
        {
            var response = await _getOrganizationIncludeCompanyService.GetAllOrganizationsWithCompaniesAndPredicateAsync(criteria);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }




        [HttpGet("GetAllOrganizationsWithCompaniesAndPredicateAndOrderBy")]
        public async Task<IActionResult> GetAllOrganizationsWithCompaniesAndPredicateAndOrderBy([FromQuery] OrganizationSearchCriteria criteria)
        {
            var response = await _getOrganizationIncludeCompanyService.GetAllOrganizationsWithCompaniesAndPredicateAndOrderByAsync(criteria);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }





        [HttpPost("AddOrganizationWithFile")]
        public async Task<IActionResult> CreateOrganization([FromForm] CreateOrganizationRequestWithFileDto formDto)
        {
            var result = await _createOrganizationWithFileService.AddOrganizationAsync(formDto);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



        [HttpPost]
        [Route("AddOrganization")]
        public async Task<IActionResult> AddCompany([FromBody] CreateOrganizationRequestDto createOrganizationDto)
        {
            var response = await _createOrganizationService.AddOrganizationAsync(createOrganizationDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpPost]
        [Route("AddOrganizationWithExists")]
        public async Task<IActionResult> AddCompanyWithExists([FromBody] CreateOrganizationRequestDto createOrganizationDto)
        {
            var response = await _createOrganizationService.AddOrganizationWithExistsAsync(createOrganizationDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        [HttpPost]
        [Route("AddOrganizations")]
        public async Task<IActionResult> AddRange([FromBody] IEnumerable<CreateOrganizationRequestDto> createOrganizationDto)
        {
            if (createOrganizationDto == null || !createOrganizationDto.Any())
            {
                return BadRequest("No company data provided.");
            }

            var response = await _createOrganizationService.AddOrganizatiosAsync(createOrganizationDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }




        [HttpPut]
        [Route("UpdateOrganization")]
        public async Task<ActionResult<Response<CreateOrganizationResultDto>>> UpdateOrganization([FromBody] UpdateOrganizationDto updateOrganizationDto)
        {
            var response = await _updateOrganizationService.UpdateOrganizationAsync(updateOrganizationDto);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("UpdateOrganizations")]
        public async Task<ActionResult<Response<UpdateRangeResult<CreateOrganizationResultDto>>>> UpdateOrganizationsBatch(IEnumerable<UpdateOrganizationDto> updateOrganizationDtos)
        {
            var response = await _updateOrganizationService.UpdateOrganizationsAsync(updateOrganizationDtos);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }








        // Get Single


        [HttpGet]
        [Route("GetOrganizationById")]
        public async Task<IActionResult> GetOrganizationById([FromQuery] long organizationId)
        {
            var result = await _getOrganizationService.GetSingleOrganizationById(organizationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }



        [HttpGet("GetSingleOrganizationsWithDtoCriteria")]
        public async Task<IActionResult> GetSingleOrganizationsWithCriteria([FromQuery] OrganizationSearchCriteria criteria)
        {
            var response = await _getOrganizationService.GetSingleOrganizationsByCriteriaAsync(criteria);

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet("GetSingleOrganizationsWithDtoCriteriaInController")]
        public async Task<IActionResult> GetSingleOrganizationsWithDtoCriteriainCotroller([FromQuery] OrganizationSearchCriteria criteria)
        {
            var response = await _getOrganizationService.GetSingleOrganizationsByCriteriaAsync(e => (e.OrganizationName.Contains(criteria.OrganizationName) || (e.IsActive == criteria.IsActive)));

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }



        [HttpGet]
        [Route("GetSingleOrganizationsWithCriteria")]
        public async Task<IActionResult> GetUserCreationConfig([FromQuery] bool? isActive, long? organizationId)
        {
            var result = await _getOrganizationService.GetSingleOrganizationsByCriteriaAsync(e => e.IsActive == isActive || e.OrganizationId == organizationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }





        // Get Single With Company

        [HttpGet("GetSingleOrganizationsWithCompaniesAndPredicate")]
        public async Task<IActionResult> GetSingleOrganizationsWithCompaniesAndPredicate([FromQuery] OrganizationSearchCriteria criteria)
        {
            var response = await _getOrganizationIncludeCompanyService.GetSingleOrganizationsWithCompaniesAndPredicateAsync(criteria);
            return Ok(response);
        }








        // ................................ Create Master Detail
        // .................... Start


        [HttpPost]
        [Route("CreateOrganizationIncludeCompanies")]
        public async Task<IActionResult> CreateOrganizationIncludeCompanies([FromBody] CreateOrganizationIncludesCompanyRequestDto masterDetailDto)
        {
            if (masterDetailDto.Organization == null || masterDetailDto.Companies == null || masterDetailDto.Companies.Count == 0)
            {
                return BadRequest("Invalid data. Organization and at least one company are required.");
            }

            var response = await _createOrganizationWithIncludeCompanyService.CreateOrganizationIncludeCompaniesAsync(
                masterDetailDto.Organization, masterDetailDto.Companies);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }





        [HttpPost]
        [Route("CreateOrganizationWithCompanies")]
        public async Task<IActionResult> CreateOrganizationWithCompanies([FromBody] CreateOrganizationWithCompanyRequestDto masterDetailDto)
        {
            if (masterDetailDto.Organization == null || masterDetailDto.Companies == null)
            {
                return BadRequest("Invalid data. Organization and at least one company are required.");
            }

            var response = await _createOrganizationWithIncludeCompanyService.CreateOrganizationWithCompaniesAsync(
                masterDetailDto.Organization, masterDetailDto.Companies);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }



        // .................... End
        // ................................ Create Master Detail



        // .....................................................................
        // ................................ Get Master Details Without Includes
        // ...................... Start 


        [HttpGet("GeSingleOrganizationWithCompanies")]
        public async Task<IActionResult> GetMasterWithDetails([FromQuery] long masterId)
        {
            var response = await _getOrganzationWithCompanyService.GetOrganizationWithCompaniesAsync(masterId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // ...................... End 
        // ................................ Get Master Details Without Includes
        // .....................................................................







        // .....................................................................
        // ................................ Delete Single Organization
        // ...................... Start 




        [HttpDelete("RemoveOrganization")]

        public async Task<IActionResult> RemoveOrganization([FromQuery] long organizationId)
        {
            try
            {
                var response = await _removeOrganizationService.DeleteOrganizationAsync(organizationId);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("RemoveOrganizationWithPredicate")]

        public async Task<IActionResult> RemoveOrganizationWithPredicate([FromQuery] long organizationId)
        {
            try
            {
                var response = await _removeOrganizationService.DeleteOrganizationWithPredicateAsync(organizationId);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpDelete("DeleteOrganizationWithPredicateInController")]

        public async Task<IActionResult> DeleteOrganizationWithPredicateInController([FromQuery] string organizationName)
        {
            try
            {
                var response = await _removeOrganizationService.DeleteOrganizationWithPredicateAsync(e=> e.OrganizationName == organizationName);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete]
        [Route("RemoveOrganizations")]
        public async Task<IActionResult> RemoveOrganizations([FromQuery] IEnumerable<long> ids)
        {
            try
            {
                var response = await _removeOrganizationService.DeleteOrganizationsAsync(ids);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpDelete]
        [Route("RemoveRangeOrganizationsByDto")]
        public async Task<IActionResult> RemoveRangeOrganizationsByDto([FromBody] IEnumerable<DeleteOrganizationDto> dtos)
        {
            try
            {
                var response = await _removeOrganizationService.DeleteRangeOrganizationsByDtoAsync(dtos);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // ...................... End 
        // ................................ Delete Single Organization
        // .....................................................................









        // ........................................................................
        // ................................ Delete Master Details Without Includes
        // ...................... Start 


        [HttpDelete("DeleteOrganizationWithCompanies")]
        public async Task<IActionResult> DeleteOrganizationWithCompanies([FromQuery] long organizationId)
        {
            if (organizationId <= 0)
            {
                return BadRequest("Invalid organization Id.");
            }

            var response = await _deleteOrganizationWithCompanyService.DeleteOrganizationWithCompaniesAsync(organizationId);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


        // ...................... End 
        // ................................ Delete Master Details Without Includes
        // ........................................................................








        [HttpDelete("DeleteOrganization")]

        public async Task<IActionResult> DeleteOrganization([FromQuery] long organizationId)
        {
            var response = await _deleteOrganizationsService.DeleteOrganizationAsync(organizationId);
            return Ok(response);
        }



        [HttpDelete]
        [Route("DeleteOrganizations")]
        public async Task<IActionResult> DeleteOrganizations([FromBody] IEnumerable<long> ids)
        {
            var response = await _deleteOrganizationsService.DeleteOrganizationsAsync(ids);
            return Ok(response);
        }




        [HttpDelete("DeleteOrganizationWithPredicate")]
        public async Task<IActionResult> DeleteOrganizationWithPredicate([FromQuery] bool isActive)
        {
            var response = await _deleteOrganizationsService.DeleteOrganizationWithPredicateAsync(e=> e.IsActive == isActive);
            return Ok(response);
        }





        // Endpoint to delete organizations using DTOs to create predicates
        [HttpDelete]
        [Route("DeleteRangeOrganizationsWithPredicate")]
        public async Task<IActionResult> DeleteRangeOrganizationsWithPredicate([FromBody] IEnumerable<DeleteOrganizationDto> dtos)
        {
            var response = await _deleteOrganizationsService.DeleteRangeOrganizationsAsync(dtos);
            return Ok(response);
        }



    }
}
