using BLL.Service.Employee.Employee_Information;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Employee.Employee_Information;

namespace API.Controllers.Employee.Employee_Information
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeInformationsController : ControllerBase
    {
        private readonly GetEmployeeInformationService _getEmployeeInformationService;

        public EmployeeInformationsController(GetEmployeeInformationService getEmployeeInformationService)
        {
            _getEmployeeInformationService = getEmployeeInformationService ?? throw new ArgumentNullException(nameof(getEmployeeInformationService));
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _getEmployeeInformationService.GetAllEmployeeInformationAsync();
            return Ok(response);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveEmployees()
        {
            var response = await _getEmployeeInformationService.GetActiveEmployeeInformationAsync();
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetEmployeesByCriteria([FromQuery] EmployeeSearchCriteria criteria)
        {
            var response = await _getEmployeeInformationService.GetEmployeeInformationByCriteriaAsync(criteria);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(long id)
        {
            var response = await _getEmployeeInformationService.GetSingleEmployeeByIdAsync(id);
            return Ok(response);
        }

        [HttpGet("search/single")]
        public async Task<IActionResult> GetSingleEmployeeByCriteria([FromQuery] EmployeeSearchCriteria criteria)
        {
            var response = await _getEmployeeInformationService.GetSingleEmployeeByCriteriaAsync(criteria);
            return Ok(response);
        }
    }
}
