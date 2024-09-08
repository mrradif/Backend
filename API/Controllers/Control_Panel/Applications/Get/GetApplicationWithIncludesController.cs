using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Applications.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetApplicationWithIncludesController : ControllerBase
    {
        private readonly GetApplicationWithIncludesService _getApplicationWithIncludesService;

        public GetApplicationWithIncludesController(GetApplicationWithIncludesService getApplicationWithIncludesService)
        {
            _getApplicationWithIncludesService = getApplicationWithIncludesService;
        }

        // GET: api/GetApplicationWithIncludes/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllApplications()
        {
            var includes = new Expression<Func<Application, object>>[]
            {
                app => app.Modules,
                app => app.MainMenus,
                app=> app.Buttons
            };

            var result = await _getApplicationWithIncludesService.GetAllApplicationWithIncludesAsync(includes);

            return Ok(result);
        }

        // GET: api/GetApplicationWithIncludes/predicate
        [HttpGet("predicate")]
        public async Task<IActionResult> GetApplicationsWithPredicate([FromQuery] ApplicationSearchCriteria criteria)
        {
            var predicate = (Expression<Func<Application, bool>>)(app => app.ApplicationName.StartsWith(criteria.ApplicationName));

            var includes = new Expression<Func<Application, object>>[]
            {
                app => app.Modules,
                app => app.MainMenus,
                app=> app.Buttons
            };

            var result = await _getApplicationWithIncludesService.GetApplicationWithIncludesAndPredicateAsync(predicate, includes);

            return Ok(result);
        }

        // GET: api/GetApplicationWithIncludes/ordered
        [HttpGet("ordered")]
        public async Task<IActionResult> GetApplicationsOrdered()
        {
            var predicate = (Expression<Func<Application, bool>>)(app => app.IsActive);
            var orderBy = (Func<IQueryable<Application>, IOrderedQueryable<Application>>)(applications => applications.OrderBy(a => a.ApplicationId));

            var includes = new Expression<Func<Application, object>>[]
            {
                app => app.Modules,
                app => app.MainMenus,
                app=> app.Buttons
            };

            var result = await _getApplicationWithIncludesService.GetApplicationWithIncludesAndPredicateAndOrderedAsync((predicate, orderBy, includes));

            return Ok(result);
        }

        // GET: api/GetApplicationWithIncludes/single
        [HttpGet("single")]
        public async Task<IActionResult> GetSingleApplication([FromQuery] ApplicationSearchCriteria criteria)
        {
            var predicate = (Expression<Func<Application, bool>>)(app => app.ApplicationName == criteria.ApplicationName);

            var includes = new Expression<Func<Application, object>>[]
            {
              app => app.Modules,
                app => app.MainMenus,
                app=> app.Buttons
            };

            var result = await _getApplicationWithIncludesService.GetSingleApplicationWithIncludesAsync(predicate, includes);

            return Ok(result);
        }


    }
}


