using BLL.Service.Control_Panel.Component_Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Criteria.Control_Panel.Search_Component;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Components
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetComponentsWithApplicationAndModuleController : ControllerBase
    {
        private readonly GetComponentWithApplicationAndModuleService _getComponentWithApplicationAndModuleService;

        public GetComponentsWithApplicationAndModuleController(GetComponentWithApplicationAndModuleService getComponentWithApplicationAndModuleService)
        {
            _getComponentWithApplicationAndModuleService = getComponentWithApplicationAndModuleService ?? throw new ArgumentNullException(nameof(getComponentWithApplicationAndModuleService));
        }

        // ............................................................ Get All
        // ............................................. Start

        [HttpGet("GetAllComponentsWithApplicationsAndModules")]
        public async Task<IActionResult> GetAllComponentsWithApplicationsAndModules()
        {
            var response = await _getComponentWithApplicationAndModuleService.GetAllComponentsWithApplicationsAndModulesAsync();
            return Ok(response);
        }


        //Overloaded method to accept dynamic includes
        [HttpGet("GetAllComponentsWithApplicationsAndModules2")]
        public async Task<IActionResult> GetAllComponentsWithApplicationsAndModules2()
        {
            var response = await _getComponentWithApplicationAndModuleService.GetAllComponentsWithApplicationsAndModulesAsync(
                c => c.Application, c => c.Module);
            return Ok(response);
        }



        [HttpGet("GetActiveComponentsWithApplicationsAndModules")]
        public async Task<IActionResult> GetActiveComponentsWithApplicationsAndModules()
        {
            Expression<Func<Component, bool>> predicate = c => !c.IsDeleted && c.IsActive;

            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                predicate,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpGet("GetActiveComponentsWithApplicationsAndModules2")]
        public async Task<IActionResult> GetActiveComponentsWithApplicationsAndModules2()
        {
            Expression<Func<Component, bool>> predicate = c => !c.IsDeleted && c.IsActive;

            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                predicate,
                c => c.Application
            );
            return Ok(response);
        }

        [HttpGet("GetComponentsWithApplicationsAndModulesOrdered")]
        public async Task<IActionResult> GetComponentsWithApplicationsAndModulesOrdered()
        {
            var parameters = (
                predicate: (Expression<Func<Component, bool>>)(c => !c.IsDeleted),
                orderBy: (Func<IQueryable<Component>, IOrderedQueryable<Component>>)(query => query.OrderBy(c => c.ComponentName)),
                includes: new Expression<Func<Component, object>>[] { c => c.Application, c => c.Module }
            );

            var response = await _getComponentWithApplicationAndModuleService.GetComponentsWithApplicationsAndModulesOrderedAsync(parameters);
            return Ok(response);
        }

        [HttpGet("GetComponentsWithApplicationsAndModulesOrdered2")]
        public async Task<IActionResult> GetComponentsWithApplicationsAndModulesOrdered2()
        {
            var response = await _getComponentWithApplicationAndModuleService.GetComponentsWithApplicationsAndModulesOrderedAsync();
            return Ok(response);
        }

        [HttpGet("GetComponentsWithApplicationsAndModulesOrdered3")]
        public async Task<IActionResult> GetComponentsWithApplicationsAndModulesOrdered3([FromQuery] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetComponentsWithApplicationsAndModulesOrderedAsync(criteria);
            return Ok(response);
        }

        // ............................................. End
        // ............................................................ Get All




        // ......................................................... Get Single
        // .................................... Start

        [HttpGet("GetSingleComponentWithApplicationAndModule")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModule([FromQuery] string componentName)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                c => c.ComponentName == componentName && !c.IsDeleted,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpGet("GetSingleComponentWithApplicationAndModuleWithIncludes")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleWithIncludes(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Component, bool>> predicate = c => c.IsActive == isActive && !c.IsDeleted;

            var includes = includeProperties
                .Select(prop => (Expression<Func<Component, object>>)(c => EF.Property<object>(c, prop)))
                .ToArray();

            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                predicate,
                includes
            );
            return Ok(response);
        }

        [HttpGet("GetSingleComponentWithApplicationAndModuleWithIncludes2")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleWithIncludes2(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Component, bool>> predicate = c => c.IsActive == isActive && !c.IsDeleted;

            var specificIncludes = new List<Expression<Func<Component, object>>>
            {
                c => c.Application,  // Assuming you need Application included
                c => c.Module
            };

            specificIncludes.AddRange(
                includeProperties.Select(prop => (Expression<Func<Component, object>>)(c => EF.Property<object>(c, prop)))
            );

            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                predicate,
                specificIncludes.ToArray()
            );
            return Ok(response);
        }

        [HttpGet("GetSingleComponentWithApplicationAndModuleWithIncludes3")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleWithIncludes3(
            [FromQuery] bool isActive)
        {
            Expression<Func<Component, bool>> predicate = c => c.IsActive == isActive && !c.IsDeleted;

            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                predicate,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpGet("GetSingleComponentWithApplicationAndModuleByCriteria")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleByCriteria([FromQuery] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpGet("GetSingleComponentWithApplicationAndModuleByCriteria2")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleByCriteria2([FromQuery] ComponentSearchCriteria criteria)
        {
            Expression<Func<Component, bool>> predicate = c => c.IsActive == criteria.IsActive;

            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                predicate,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpPost("GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes(
            [FromBody] ComponentSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            var includes = includeProperties
                .Select(prop => (Expression<Func<Component, object>>)(c => EF.Property<object>(c, prop)))
                .ToArray();

            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                includes
            );
            return Ok(response);
        }

        [HttpPost("GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes2")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes2(
            [FromBody] ComponentSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpPost("GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes3")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes3(
            [FromBody] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpPost("GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes4")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes4(
            [FromBody] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                c => c.IsActive == criteria.IsActive,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpPost("GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes5")]
        public async Task<IActionResult> GetSingleComponentWithApplicationAndModuleByCriteriaWithIncludes5(
            [FromBody] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        // .................................... End
        // ......................................................... Get Single



        // .................................................. Get With Search Criteria
        // ..................................... Start

        [HttpGet("GetComponentsBySearchCriteria")]
        public async Task<IActionResult> GetComponentsBySearchCriteria([FromQuery] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpGet("GetComponentsBySearchCriteria2")]
        public async Task<IActionResult> GetComponentsBySearchCriteria2([FromQuery] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpPost("GetComponentsBySearchCriteriaWithIncludes")]
        public async Task<IActionResult> GetComponentsBySearchCriteriaWithIncludes(
            [FromBody] ComponentSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            var includes = includeProperties
                .Select(prop => (Expression<Func<Component, object>>)(c => EF.Property<object>(c, prop)))
                .ToArray();

            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                includes
            );
            return Ok(response);
        }

        [HttpPost("GetComponentsBySearchCriteriaWithIncludes2")]
        public async Task<IActionResult> GetComponentsBySearchCriteriaWithIncludes2(
            [FromBody] ComponentSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpPost("GetComponentsBySearchCriteriaWithIncludes3")]
        public async Task<IActionResult> GetComponentsBySearchCriteriaWithIncludes3(
            [FromBody] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        [HttpPost("GetComponentsBySearchCriteriaWithIncludes4")]
        public async Task<IActionResult> GetComponentsBySearchCriteriaWithIncludes4(
            [FromBody] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentWithApplicationAndModuleService.GetSingleComponentWithApplicationAndModuleAsync(
                criteria,
                c => c.Application, c => c.Module
            );
            return Ok(response);
        }

        // ..................................... End
        // .................................................. Get With Search Criteria

    }
}
