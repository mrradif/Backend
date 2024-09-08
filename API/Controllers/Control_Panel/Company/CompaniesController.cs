using BLL.Service.Control_Panel.Company_Service;
using BLL.Service.Control_Panel.Delete;
using BLL.Service.Control_Panel.Update;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.DTO.Control_Panel.Administration.Delete;
using Shared.DTO.Control_Panel.Administration.Search;
using Shared.DTO.Control_Panel.Administration.Update;

namespace API.Controllers.Control_Panel.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly GetCompanyService _getCompanyService;
        private readonly CreateCompanyService _createCompanyService;
        private readonly UpdateCompanyService _updateCompanyService;
        private readonly DeleteCompanyService _deleteCompanyService;

        public CompaniesController(
            GetCompanyService getCompanyService,
            CreateCompanyService createCompanyService,
            UpdateCompanyService updateCompanyService,
            DeleteCompanyService deleteCompanyService)
        {
            _getCompanyService = getCompanyService;
            _createCompanyService = createCompanyService;
            _updateCompanyService = updateCompanyService;
            _deleteCompanyService = deleteCompanyService;
        }

        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var response = await _getCompanyService.GetAllCompaniesAsync();

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet("GetAllCompaniesWithCriteria")]
        public async Task<IActionResult> GetAllCompaniesWithCriteria([FromQuery] CompanySearchCriteria criteria)
        {
            var response = await _getCompanyService.GetCompaniesByCriteriaAsync(criteria);

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }


        [HttpPost]
        [Route("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] CreateCompanyRequestDto createCompanyDto)
        {
            var response = await _createCompanyService.AddCompanyAsync(createCompanyDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpPost]
        [Route("AddCompanyWithExists")]
        public async Task<IActionResult> AddCompanyWithExists([FromBody] CreateCompanyRequestDto createCompanyDto)
        {
            var response = await _createCompanyService.AddCompanyWithExistsAsync(createCompanyDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        [HttpPost]
        [Route("AddCompanies")]
        public async Task<IActionResult> AddRange([FromBody] IEnumerable<CreateCompanyRequestDto> createCompanyDtos)
        {
            if (createCompanyDtos == null || !createCompanyDtos.Any())
            {
                return BadRequest("No company data provided.");
            }

            var response = await _createCompanyService.CreateCompaniesAsync(createCompanyDtos);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }



        [HttpPut]
        [Route("UpdateCompany")]
        public async Task<ActionResult<Response<CreateCompanyResultDto>>> UpdateCompany([FromBody] UpdateCompanyDto updateCompanyDto)
        {
            var response = await _updateCompanyService.UpdateCompanyAsync(updateCompanyDto);

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
        [Route("UpdateCompanies")]
        public async Task<ActionResult<Response<UpdateRangeResult<CreateCompanyResultDto>>>> UpdateCompaniesBatch(IEnumerable<UpdateCompanyDto> updateCompanyDtos)
        {
            var response = await _updateCompanyService.UpdateCompaniesAsync(updateCompanyDtos);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }



        [HttpDelete("DeleteCompanyById/{id}")]
        public async Task<IActionResult> DeleteCompany(long id)
        {
            try
            {
                var response = await _deleteCompanyService.DeleteCompanyWithPredicateAsync(id);

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
        [Route("DeleteCompaniesById")]
        public async Task<IActionResult> DeleteCompanies([FromBody] IEnumerable<long> ids)
        {
            try
            {
                var response = await _deleteCompanyService.DeleteCompaniesAsync(ids);

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
        [Route("DeleteCompaniesByDto")]
        public async Task<IActionResult> DeleteCompaniesByDto([FromBody] IEnumerable<DeleteCompanyDto> dtos)
        {
            try
            {
                var response = await _deleteCompanyService.DeleteCompaniesByDtoAsync(dtos);

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

    }
}
