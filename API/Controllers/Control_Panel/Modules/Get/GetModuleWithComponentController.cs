using BLL.Service.Control_Panel.Applications.Get;
using BLL.Service.Control_Panel.Modules.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Criteria.Application;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Modules.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetModuleWithComponentController : ControllerBase
    {
        private readonly GetModuleWithComponentsService _getModuleWithComponentService;

        public GetModuleWithComponentController(GetModuleWithComponentsService getModuleWithComponentService)
        {
            _getModuleWithComponentService = getModuleWithComponentService ?? throw new ArgumentNullException(nameof(getModuleWithComponentService));
        }

        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        [HttpGet("GetAllModulesWithComponents")]
        public async Task<IActionResult> GetAllModulesWithComponents()
        {
            var response = await _getModuleWithComponentService.GetAllModulesWithComponentsAsync();
            return Ok(response);
        }

        [HttpGet("GetAllModulesWithComponents2")]
        public async Task<IActionResult> GetAllModulesWithComponents2()
        {
            var response = await _getModuleWithComponentService.GetAllModulesWithComponentsAsync(e=> e.IsDeleted == false);
            return Ok(response);
        }


        [HttpGet("GetModulesWithComponents")]
        public async Task<IActionResult> GetModulesWithComponents()
        {
            Expression<Func<Module, bool>> predicate = m => m.IsDeleted == false && m.IsActive == true;

            var response = await _getModuleWithComponentService.GetModulesWithComponentsAsync(
                predicate,
                m => m.Components
            );
            return Ok(response);
        }


        [HttpGet("GetModulesWithComponents2")]
        public async Task<IActionResult> GetModulesWithComponents2()
        {
            var response = await _getModuleWithComponentService.GetModulesWithComponentsAsync();
            return Ok(response);
        }




        [HttpGet("GetModulesWithComponentsOrdered")]
        public async Task<IActionResult> GetModulesWithComponentsOrdered()
        {
            var parameters = (
                predicate: (Expression<Func<Module, bool>>)(m => m.IsDeleted == false), // Example predicate
                orderBy: (Func<IQueryable<Module>, IOrderedQueryable<Module>>)(query => query.OrderBy(m => m.ModuleName)), // Example ordering
                includes: new Expression<Func<Module, object>>[] { m => m.Components }
            );

            var response = await _getModuleWithComponentService.GetModulesWithComponentsOrderedAsync(parameters);
            return Ok(response);
        }



        [HttpGet("GetModulesWithComponentsOrdered2")]
        public async Task<IActionResult> GetModulesWithComponentsOrdered2()
        {
            var response = await _getModuleWithComponentService.GetModulesWithComponentsOrderedAsync();
            return Ok(response);
        }

        [HttpGet("GetModulesWithComponentsOrdered3")]
        public async Task<IActionResult> GetModulesWithComponentsOrdered3([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleWithComponentService.GetModulesWithComponentsOrderedAsync(criteria);
            return Ok(response);
        }




        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        [HttpGet("GetSingleModuleWithComponent")]
        public async Task<IActionResult> GetSingleModuleWithComponent([FromQuery] string moduleName)
        {
            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(m => m.ModuleName == moduleName && !m.IsDeleted);
            return Ok(response);
        }



        [HttpGet("GetSingleModuleWithComponents")]
        public async Task<IActionResult> GetSingleModuleWithComponents(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, bool>> predicate = m => m.IsActive == isActive && !m.IsDeleted;

            Expression<Func<Module, object>>[] includes = includeProperties
                .Select(prop => (Expression<Func<Module, object>>)(m => EF.Property<object>(m, prop)))
                .ToArray();

            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(predicate, includes);
            return Ok(response);
        }

        [HttpGet("GetSingleModuleWithComponents2")]
        public async Task<IActionResult> GetSingleModuleWithComponents2(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, bool>> predicate = m => m.IsActive == isActive && !m.IsDeleted;

            List<Expression<Func<Module, object>>> specificIncludes = new List<Expression<Func<Module, object>>>
            {
                m => m.Components
            };

            specificIncludes.AddRange(
                includeProperties.Select(prop => (Expression<Func<Module, object>>)(m => EF.Property<object>(m, prop)))
            );

            var includesArray = specificIncludes.ToArray();

            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(predicate, includesArray);
            return Ok(response);
        }

        [HttpGet("GetSingleModuleWithComponents3")]
        public async Task<IActionResult> GetSingleModuleWithComponents3(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, bool>> predicate = m => m.IsActive == isActive && !m.IsDeleted;

            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(predicate, m => m.Components);
            return Ok(response);
        }




        [HttpGet("GetSingleModuleWithComponentByCriteria")]
        public async Task<IActionResult> GetSingleModuleWithComponentByCriteria([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(criteria);
            return Ok(response);
        }

        [HttpGet("GetSingleModuleWithComponentByCriteria2")]
        public async Task<IActionResult> GetSingleModuleWithComponentByCriteria2([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            Expression<Func<Module, bool>> predicate = m => m.IsActive == criteria.IsActive;

            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(predicate);
            return Ok(response);
        }





        [HttpPost("GetSingleModuleWithComponent")]
        public async Task<IActionResult> GetSingleModuleWithComponent(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, object>>[] includes = includeProperties
                .Select(prop => (Expression<Func<Module, object>>)(a => EF.Property<object>(a, prop)))
            .ToArray();

            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(criteria, includes);
            return Ok(response);
        }


        [HttpPost("GetSingleModuleWithComponent2")]
        public async Task<IActionResult> GetSingleModuleWithComponent2(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(criteria, a => a.Components);
            return Ok(response);
        }

        [HttpPost("GetSingleModuleWithComponent3")]
        public async Task<IActionResult> GetSingleModuleWithComponent3(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                a => a.Components
            };

            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(criteria, includes);
            return Ok(response);
        }

        [HttpPost("GetSingleModuleWithComponent4")]
        public async Task<IActionResult> GetSingleModuleWithComponent4(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, bool>> predicate = a => a.IsActive == criteria.IsActive;
            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(predicate, a => a.Components);
            return Ok(response);
        }

        [HttpPost("GetSingleModuleWithComponent5")]
        public async Task<IActionResult> GetSingleModuleWithComponent5(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, bool>> predicate = a => a.IsActive == criteria.IsActive;

            List<Expression<Func<Module, object>>> specificIncludes = new List<Expression<Func<Module, object>>>
                {
                    a => a.Components
                };

            specificIncludes.AddRange(
                includeProperties.Select(prop => (Expression<Func<Module, object>>)(a => EF.Property<object>(a, prop)))
            );
            var includesArray = specificIncludes.ToArray();

            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(predicate, includesArray);
            return Ok(response);
        }

        [HttpPost("GetSingleModuleWithComponent6")]
        public async Task<IActionResult> GetSingleModuleWithComponent6(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                a => a.Components
            };

            var response = await _getModuleWithComponentService.GetSingleModuleWithComponenAsync(criteria, includes);
            return Ok(response);
        }




        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>



    }
}
