using BLL.Service.Control_Panel.Applications.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Applications.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetApplicationWithModulesController : ControllerBase
    {
        private readonly GetApplicationWithModuleService _getApplicationWithModuleService;

        public GetApplicationWithModulesController(GetApplicationWithModuleService getApplicationWithModuleService)
        {
            _getApplicationWithModuleService = getApplicationWithModuleService ?? throw new ArgumentNullException(nameof(getApplicationWithModuleService));
        }


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start


        [HttpGet("GetAllApplicationsWithModules")]
        public async Task<IActionResult> GetAllApplicationsWithModules()
        {
            var response = await _getApplicationWithModuleService.GetAllApplicationWithModulesAsync();
            return Ok(response);
        }

        [HttpGet("GetAllApplicationsWithModules2")]
        public async Task<IActionResult> GetAllApplicationsWithModules2()
        {
            var response = await _getApplicationWithModuleService.GetAllApplicationWithModulesAsync(a => a.Modules);
            return Ok(response);
        }


        [HttpGet("GetApplicationsWithModules")]
        public async Task<IActionResult> ActiveApplicationsWithModules()
        {
            var response = await _getApplicationWithModuleService.GetApplicationWithModulesAsync();
            return Ok(response);
        }

        [HttpGet("GetApplicationsWithModules2")]
        public async Task<IActionResult> ActiveApplicationsWithModules2()
        {
            Expression<Func<Application, bool>> predicate = a => a.IsDeleted == false && a.IsActive == true;

            var response = await _getApplicationWithModuleService.GetApplicationWithModulesAsync(
                predicate,
                a => a.Modules
            );
            return Ok(response);
        }



        [HttpGet("GetApplicationsWithModulesOrdered")]
        public async Task<IActionResult> GetApplicationsWithModulesOrdered()
        {
            var parameters = (
                predicate: (Expression<Func<Application, bool>>)(a => a.IsDeleted == false), // Example predicate
                orderBy: (Func<IQueryable<Application>, IOrderedQueryable<Application>>)(query => query.OrderBy(a => a.ApplicationName)), // Example ordering
                includes: new Expression<Func<Application, object>>[] { a => a.Modules }
            );

            var response = await _getApplicationWithModuleService.GetApplicationsWithModulesOrderedAsync(parameters);
            return Ok(response);
        }

        [HttpGet("GetApplicationsWithModulesOrdered2")]
        public async Task<IActionResult> GetApplicationsWithModulesOrdered2()
        {
            var response = await _getApplicationWithModuleService.GetApplicationsWithModulesOrderedAsync();
            return Ok(response);
        }

        [HttpGet("GetApplicationsWithModulesOrdered3")]
        public async Task<IActionResult> GetApplicationsWithModulesOrdered3([FromQuery] ApplicationSearchCriteria criteria)
        {
            var response = await _getApplicationWithModuleService.GetApplicationsWithModulesOrderedAsync(criteria);
            return Ok(response);
        }




        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>





        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        [HttpGet("GetSingleApplicationWithModule")]
        public async Task<IActionResult> GetSingleApplicationWithModule([FromQuery] string applicationName)
        {
            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(a => a.ApplicationName == applicationName && a.IsDeleted == false);
            return Ok(response);
        }



        [HttpGet("GetSingleApplicationWithModuleWithIncludes")]
        public async Task<IActionResult> GetSingleApplicationWithModuleWithIncludes(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Application, bool>> predicate = a => a.IsActive == isActive && !a.IsDeleted;

            // Build the includes array based on the passed properties
            Expression<Func<Application, object>>[] includes = includeProperties
                .Select(prop => (Expression<Func<Application, object>>)(a => EF.Property<object>(a, prop)))
                .ToArray();

            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(predicate, includes);
            return Ok(response);
        }

        [HttpGet("GetSingleApplicationWithModuleWithIncludes2")]
        public async Task<IActionResult> GetSingleApplicationWithModuleWithIncludes2(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Application, bool>> predicate = a => a.IsActive == isActive && !a.IsDeleted;

            List<Expression<Func<Application, object>>> specificIncludes = new List<Expression<Func<Application, object>>>
                {
                    a => a.Modules
                };

            specificIncludes.AddRange(
                includeProperties.Select(prop => (Expression<Func<Application, object>>)(a => EF.Property<object>(a, prop)))
            );

            var includesArray = specificIncludes.ToArray();

            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(predicate, includesArray);
            return Ok(response);
        }

        [HttpGet("GetSingleApplicationWithModuleWithIncludes3")]
        public async Task<IActionResult> GetSingleApplicationWithModuleWithIncludes3(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Application, bool>> predicate = a => a.IsActive == isActive && !a.IsDeleted;

            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(predicate, a => a.Modules);

            return Ok(response);
        }





        [HttpGet("GetSingleApplicationWithModuleByCriteria")]
        public async Task<IActionResult> GetSingleApplicationWithModuleByCriteria([FromQuery] ApplicationSearchCriteria criteria)
        {
            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(criteria);
            return Ok(response);
        }

        [HttpGet("GetSingleApplicationWithModuleByCriteria2")]
        public async Task<IActionResult> GetSingleApplicationWithModuleByCriteria2([FromQuery] ApplicationSearchCriteria criteria)
        {
            Expression<Func<Application, bool>> predicate = a => a.IsActive == criteria.IsActive;

            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(predicate);
            return Ok(response);
        }




        [HttpPost("GetSingleApplicationWithModuleByCriteriaWithIncludes")]
        public async Task<IActionResult> GetSingleApplicationWithModuleByCriteriaWithIncludes(
            [FromBody] ApplicationSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Application, object>>[] includes = includeProperties
                .Select(prop => (Expression<Func<Application, object>>)(a => EF.Property<object>(a, prop)))
                .ToArray();

            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(criteria, includes);
            return Ok(response);
        }

        [HttpPost("GetSingleApplicationWithModuleByCriteriaWithIncludes2")]
        public async Task<IActionResult> GetSingleApplicationWithModuleByCriteriaWithIncludes2(
            [FromBody] ApplicationSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(criteria, a => a.Modules);
            return Ok(response);
        }

        [HttpPost("GetSingleApplicationWithModuleByCriteriaWithIncludes3")]
        public async Task<IActionResult> GetSingleApplicationWithModuleByCriteriaWithIncludes3(
            [FromBody] ApplicationSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Application, object>>[] includes = new Expression<Func<Application, object>>[]
            {
                a => a.Modules
            };

            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(criteria, includes);
            return Ok(response);
        }

        [HttpPost("GetSingleApplicationWithModuleByCriteriaWithIncludes4")]
        public async Task<IActionResult> GetSingleApplicationWithModuleByCriteriaWithIncludes4(
            [FromBody] ApplicationSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Application, bool>> predicate = a => a.IsActive == criteria.IsActive;
            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(predicate, a => a.Modules);
            return Ok(response);
        }

        [HttpPost("GetSingleApplicationWithModuleByCriteriaWithIncludes5")]
        public async Task<IActionResult> GetSingleApplicationWithModuleByCriteriaWithIncludes5(
            [FromBody] ApplicationSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Application, bool>> predicate = a => a.IsActive == criteria.IsActive;

            List<Expression<Func<Application, object>>> specificIncludes = new List<Expression<Func<Application, object>>>
                {
                    a => a.Modules
                };

            specificIncludes.AddRange(
                includeProperties.Select(prop => (Expression<Func<Application, object>>)(a => EF.Property<object>(a, prop)))
            );

            var includesArray = specificIncludes.ToArray();

            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(predicate, includesArray);
            return Ok(response);
        }

        [HttpPost("GetSingleApplicationWithModuleByCriteriaWithIncludes6")]
        public async Task<IActionResult> GetSingleApplicationWithModuleByCriteriaWithIncludes6(
            [FromBody] ApplicationSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Application, object>>[] includes = new Expression<Func<Application, object>>[]
            {
                a => a.Modules
            };

            var response = await _getApplicationWithModuleService.GetSingleApplicationWithModuleAsync(criteria, includes);
            return Ok(response);
        }

        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>

    }
}
