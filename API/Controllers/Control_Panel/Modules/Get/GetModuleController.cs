using BLL.Service.Control_Panel.Modules.Get;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Modules.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetModuleController : ControllerBase
    {
        private readonly GetModuleService _getModuleService;

        public GetModuleController(GetModuleService getModuleService)
        {
            _getModuleService = getModuleService ?? throw new ArgumentNullException(nameof(getModuleService));
        }

        // -------------------------------------------------------------------------------- >>>
        // ................................................ Get All Start

        [HttpGet("modules/all")]
        public async Task<IActionResult> GetAllModules()
        {
            var response = await _getModuleService.GetAllModulesAsync();
            return Ok(response);
        }

        // ................................................ Overload Start

        [HttpGet("modules/active")]
        public async Task<IActionResult> GetActiveModules()
        {
            var response = await _getModuleService.GetModulesAsync();
            return Ok(response);
        }

        [HttpGet("modules/deleted")]
        public async Task<IActionResult> GetDeletedModules()
        {
            var response = await _getModuleService.GetModulesAsync(e => e.IsDeleted == true);
            return Ok(response);
        }

        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>

        // ................................................ Overload Start

        [HttpGet("modules/search")]
        public async Task<IActionResult> GetModulesByCriteria([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleService.GetModulesByCriteriaAsync(criteria);
            return Ok(response);
        }

        [HttpGet("modules/search-by-name")]
        public async Task<IActionResult> GetModulesByName([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Module, bool>> predicate = module =>
                string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName);

            var response = await _getModuleService.GetModulesByCriteriaAsync(predicate);
            return Ok(response);
        }

        [HttpGet("modules/search-by-name-and-type")]
        public async Task<IActionResult> GetModulesByNameAndType([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Module, bool>> predicate = module =>
                string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName);

            var response = await _getModuleService.GetModulesByCriteriaAsync(predicate);
            return Ok(response);
        }

        [HttpGet("modules/search-by-exact-name")]
        public async Task<IActionResult> GetModulesByExactName([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleService.GetModulesByCriteriaAsync(e => e.ModuleName == "SomeExactName");
            return Ok(response);
        }

        [HttpGet("modules/search-by-predicate")]
        public async Task<IActionResult> GetModulesByCustomPredicate([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var predicate = CreateSearchPredicate(criteria);
            var response = await _getModuleService.GetSingleModuleByCriteriaAsync(predicate);
            return Ok(response);
        }

        private Expression<Func<Module, bool>> CreateSearchPredicate(ModuleSearchCriteriaWithPagination criteria)
        {
            return module => module.ModuleName.Contains("SomePredicateName");
        }

        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>

        // ................................................ Overload Start

        [HttpGet("modules/search-with-ordering")]
        public async Task<IActionResult> GetModulesWithOrdering([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleService.GetModulesWithOrderingAsync(criteria);
            return Ok(response);
        }

        [HttpGet("modules/search-with-ordering2")]
        public async Task<IActionResult> GetModulesWithOrdering2([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleService.GetModulesWithOrdering2Async(criteria);
            return Ok(response);
        }

        [HttpGet("modules/search-with-predicate-and-ordering")]
        public async Task<IActionResult> GetModulesWithPredicateAndOrdering([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var predicate = BuildPredicate(criteria);
            var orderBy = BuildOrderBy(criteria);
            var response = await _getModuleService.GetModulesWithOrderingAsync(predicate, orderBy);
            return Ok(response);
        }

        [HttpGet("modules/search-with-predicate-and-ordering-1")]
        public async Task<IActionResult> GetModulesWithPredicateAndOrdering1([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Module, bool>> predicate = module =>
                string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName);

            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderByDescending(button => button.ModuleId);
            var response = await _getModuleService.GetModulesWithOrderingAsync(predicate, orderBy);
            return Ok(response);
        }

        [HttpGet("modules/search-with-predicate-and-ordering-2")]
        public async Task<IActionResult> GetModulesWithPredicateAndOrdering2([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Module, bool>> predicate = module =>
                string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName);

            var orderBy = BuildOrderBy(criteria);
            var response = await _getModuleService.GetModulesWithOrderingAsync(predicate, orderBy);
            return Ok(response);
        }

        [HttpGet("modules/search-with-custom-ordering-only")]
        public async Task<IActionResult> GetModulesWithCustomOrderingOnly([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var orderBy = BuildOrderBy(criteria);
            var response = await _getModuleService.GetModulesWithOrderingAsync(criteria, orderBy);
            return Ok(response);
        }

        private Expression<Func<Module, bool>> BuildPredicate(ModuleSearchCriteriaWithPagination criteria)
        {
            return module =>
                string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName);
        }

        private Func<IQueryable<Module>, IOrderedQueryable<Module>> BuildOrderBy(ModuleSearchCriteriaWithPagination criteria)
        {
            return q => q.OrderByDescending(module => module.ModuleId);
        }

        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>

        // -------------------------------------------------------------------------------- >>>
        // ................................................ Get Single Start

        [HttpGet("modules/get-by-id")]
        public async Task<IActionResult> GetModuleById([FromQuery] long moduleId)
        {
            var response = await _getModuleService.GetSingleModuleByIdAsync(moduleId);
            return Ok(response);
        }

        // ................................................ Get Single Overload Start

        [HttpGet("modules/get-by-criteria")]
        public async Task<IActionResult> GetModuleByCriteria([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleService.GetSingleModuleByCriteriaAsync(criteria);
            return Ok(response);
        }

        [HttpGet("modules/get-by-predicate")]
        public async Task<IActionResult> GetModuleByPredicate([FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var predicate = SearchSinglePredicate(criteria);
            var response = await _getModuleService.GetSingleModuleByCriteriaAsync(predicate);
            return Ok(response);
        }

        private Expression<Func<Module, bool>> SearchSinglePredicate(ModuleSearchCriteriaWithPagination criteria)
        {
            return module =>
                string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName);
        }

        // ................................................ Get Single Overload End
        // -------------------------------------------------------------------------------- >>>

        // ................................................ Get Single End
        // -------------------------------------------------------------------------------- >>>
    }
}
