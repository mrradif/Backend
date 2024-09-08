using BLL.Service.Control_Panel.Modules.Get;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Modules.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetModuleWithIncludesController : ControllerBase
    {
        private readonly GetModuleWithIncludesService _getModuleWithIncludesService;


        public GetModuleWithIncludesController(GetModuleWithIncludesService getModuleWithIncludesService)
        {
            _getModuleWithIncludesService = getModuleWithIncludesService;
        }

        // GET: api/GetModuleWithIncludes/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllModules()
        {
            var includes = new Expression<Func<Module, object>>[]
            {
                module => module.MainMenus,
                module => module.Components,
                Module => Module.Buttons
            };

            var result = await _getModuleWithIncludesService.GetAllModuleWithIncludesAsync(includes);

            return Ok(result);
        }



        // GET: api/GetModuleWithIncludes/predicate
        [HttpGet("predicate")]
        public async Task<IActionResult> GetModulesWithPredicate()
        {
            var predicate = (Expression<Func<Module, bool>>)(module => module.ModuleName.StartsWith("Test"));

            var includes = new Expression<Func<Module, object>>[]
            {
                module => module.Components
            };

            var result = await _getModuleWithIncludesService.GetModuleWithIncludesAndPredicateAsync(predicate, includes);


            return Ok(result);
        }


        // GET: api/GetModuleWithIncludes/ordered
        [HttpGet("ordered")]
        public async Task<IActionResult> GetModulesOrdered()
        {
            var predicate = (Expression<Func<Module, bool>>)(module => module.IsActive);
            var orderBy = (Func<IQueryable<Module>, IOrderedQueryable<Module>>)(modules => modules.OrderBy(m => m.ModuleId));

            var includes = new Expression<Func<Module, object>>[]
            {
                module => module.MainMenus,
                module => module.Components,
                module => module.Buttons
            };

            var result = await _getModuleWithIncludesService.GetModuleWithIncludesAndPredicateAndOrderedAsync((predicate, orderBy, includes));



            return Ok(result);
        }



        // GET: api/GetModuleWithIncludes/single
        [HttpGet("single")]
        public async Task<IActionResult> GetSingleModule([FromQuery] ModuleSearchCriteria criteria)
        {
            var predicate = (Expression<Func<Module, bool>>)(module => module.ModuleId == criteria.ModuleId);

            var includes = new Expression<Func<Module, object>>[]
            {
                module => module.MainMenus,
                module => module.Components,
                module => module.Buttons
            };

            var result = await _getModuleWithIncludesService.GetSingleModuleWithIncludesAsync(predicate, includes);

            return Ok(result);
        }



    }
}
