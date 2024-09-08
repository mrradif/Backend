using BLL.Service.Control_Panel.Modules.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Modules.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetModuleWithApplicationController : ControllerBase
    {
        private readonly GetModuleWithApplicationService _getModuleWithApplicationService;

        public GetModuleWithApplicationController(GetModuleWithApplicationService getModuleWithApplicationService)
        {
            _getModuleWithApplicationService = getModuleWithApplicationService ?? throw new ArgumentNullException(nameof(getModuleWithApplicationService));
        }

        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        [HttpGet("GetAllModuleWithApplication")]
        public async Task<IActionResult> GetAllModulesWithComponents()
        {
            var response = await _getModuleWithApplicationService.GetAllModulesWithApplicationAsync();
            return Ok(response);
        }

        [HttpGet("GetAllModuleWithApplication2")]
        public async Task<IActionResult> GetAllModulesWithApplication2()
        {
            var response = await _getModuleWithApplicationService.GetAllModulesWithApplicationAsync(e => e.IsDeleted == false);
            return Ok(response);
        }


        [HttpGet("GetModuleWithApplication")]
        public async Task<IActionResult> GetModulesWithApplication()
        {
            Expression<Func<Module, bool>> predicate = m => m.IsDeleted == false && m.IsActive == true;

            var response = await _getModuleWithApplicationService.GetModuleWithApplicationAsync(
                predicate,
                m => m.Components
            );
            return Ok(response);
        }


        [HttpGet("GetModuleWithApplication2")]
        public async Task<IActionResult> GetModulesWithComponents2()
        {
            var response = await _getModuleWithApplicationService.GetModuleWithApplicationAsync();
            return Ok(response);
        }




        [HttpGet("GetModuleWithApplicationOrdered")]
        public async Task<IActionResult> GetModuleWithApplicationOrdered()
        {
            var parameters = (
                predicate: (Expression<Func<Module, bool>>)(m => m.IsDeleted == false), // Example predicate
                orderBy: (Func<IQueryable<Module>, IOrderedQueryable<Module>>)(query => query.OrderBy(m => m.ModuleName)), // Example ordering
                includes: new Expression<Func<Module, object>>[] { m => m.Components }
            );

            var response = await _getModuleWithApplicationService.GetModulesWithApplicationOrderedAsync(parameters);
            return Ok(response);
        }


        [HttpGet("GetModuleWithApplicationOrdered2")]
        public async Task<IActionResult> GetModuleWithApplicationOrdered2()
        {
            var response = await _getModuleWithApplicationService.GetModulesWithApplicationOrderedAsync();
            return Ok(response);
        }

        [HttpGet("GetModuleWithApplicationOrdered3")]
        public async Task<IActionResult> GetModuleWithApplicationOrdered3([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleWithApplicationService.GetModulesWithApplicationOrderedAsync(criteria);
            return Ok(response);
        }




        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        [HttpGet("GetSingleModuleWithApplication")]
        public async Task<IActionResult> GetSingleModuleWithApplication([FromQuery] string moduleName)
        {
            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(m => m.ModuleName == moduleName && !m.IsDeleted);
            return Ok(response);
        }


        [HttpGet("GetSingleModuleWithApplications")]
        public async Task<IActionResult> GetSingleModuleWithApplications(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, bool>> predicate = m => m.IsActive == isActive && !m.IsDeleted;

            Expression<Func<Module, object>>[] includes = includeProperties
                .Select(prop => (Expression<Func<Module, object>>)(m => EF.Property<object>(m, prop)))
                .ToArray();

            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(predicate, includes);
            return Ok(response);
        }


        [HttpGet("GetSingleModuleWithApplications2")]
        public async Task<IActionResult> GetSingleModuleWithApplications2(
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

            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(predicate, includesArray);
            return Ok(response);
        }


        [HttpGet("GetSingleModuleWithApplications3")]
        public async Task<IActionResult> GetSingleModuleWithApplications3(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, bool>> predicate = m => m.IsActive == isActive && !m.IsDeleted;

            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(predicate, m => m.Components);
            return Ok(response);
        }




        [HttpGet("GetSingleModuleWithApplicationByCriteria")]
        public async Task<IActionResult> GetSingleModuleWithApplicationByCriteria([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(criteria);
            return Ok(response);
        }

        [HttpGet("GetSingleModuleWithApplicationByCriteria2")]
        public async Task<IActionResult> GetSingleModuleWithApplicationByCriteria2([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            Expression<Func<Module, bool>> predicate = m => m.IsActive == criteria.IsActive;

            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(predicate);
            return Ok(response);
        }





        [HttpPost("GetSingleModuleWithApplication")]
        public async Task<IActionResult> GetSingleModuleWithApplication(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, object>>[] includes = includeProperties
                .Select(prop => (Expression<Func<Module, object>>)(a => EF.Property<object>(a, prop)))
            .ToArray();

            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(criteria, includes);
            return Ok(response);
        }


        [HttpPost("GetSingleModuleWithApplication2")]
        public async Task<IActionResult> GetSingleModuleWithApplication2(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(criteria, a => a.Components);
            return Ok(response);
        }


        [HttpPost("GetSingleModuleWithApplication3")]
        public async Task<IActionResult> GetSingleModuleWithApplication3(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                a => a.Components
            };

            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(criteria, includes);
            return Ok(response);
        }



        [HttpPost("GetSingleModuleWithApplication4")]
        public async Task<IActionResult> GetSingleModuleWithApplication4(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, bool>> predicate = a => a.IsActive == criteria.IsActive;
            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(predicate, a => a.Components);
            return Ok(response);
        }



        [HttpPost("GetSingleModuleWithApplication5")]
        public async Task<IActionResult> GetSingleModuleWithApplication5(
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

            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(predicate, includesArray);
            return Ok(response);
        }


        [HttpPost("GetSingleModuleWithApplication6")]
        public async Task<IActionResult> GetSingleModuleWithApplication6(
            [FromBody] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                a => a.Components
            };

            var response = await _getModuleWithApplicationService.GetSingleModuleWithApplicationAsync(criteria, includes);
            return Ok(response);
        }




        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>

    }
}
