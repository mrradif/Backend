using BLL.Service.Control_Panel.Applications.Get;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Applications.Get
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetApplicationIncludesWithPaginationController : ControllerBase
    {
        private readonly GetApplicationIncludesWithPaginationService _service;

        public GetApplicationIncludesWithPaginationController(GetApplicationIncludesWithPaginationService service)
        {
            _service = service;
        }


        [HttpGet("GetApplicationsWithPaginationAndIncludes")]
        public async Task<IActionResult> GetApplicationsWithPaginationAndIncludes([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var includes = new Expression<Func<Application, object>>[]
            {
                app => app.Modules,
                app => app.MainMenus,
                app => app.Buttons
            };

            var response = await _service.GetApplicationsWithPaginationAndIncludesAsync(pageNumber, pageSize, includes);
            return Ok(response);
        }


        [HttpGet("GetApplicationsWithPaginationAndIncludesWithOrderBy")]
        public async Task<IActionResult> GetApplicationsWithPaginationAndIncludesWithOrderBy([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var orderBy = (Func<IQueryable<Application>, IOrderedQueryable<Application>>)(applications => applications.OrderBy(a => a.ApplicationId));

            var includes = new Expression<Func<Application, object>>[]
            {
                app => app.Modules,
                app => app.MainMenus,
                app => app.Buttons
            };

            var response = await _service.GetApplicationsWithPaginationAndIncludesWithOrderByAsync(pageNumber, pageSize, orderBy, includes);
            return Ok(response);
        }


        [HttpPost("GetApplicationsWithPaginationAndIncludesWithPredicate")]
        public async Task<IActionResult> GetApplicationsWithPaginationAndIncludesWithPredicate([FromBody] ApplicationSearchCriteria applicationSearchCriteria, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {

            // Build the predicate based on the search criteria
            //Expression<Func<Application, bool>> predicate = app =>
            //    (string.IsNullOrEmpty(applicationSearchCriteria.ApplicationName) || app.ApplicationName.Contains(applicationSearchCriteria.ApplicationName));

            // Build the predicate based on the search criteria using var
            var predicate = (Expression<Func<Application, bool>>)(app =>
                (string.IsNullOrEmpty(applicationSearchCriteria.ApplicationName) || app.ApplicationName.Contains(applicationSearchCriteria.ApplicationName)));

            var includes = new Expression<Func<Application, object>>[]
            {
                app => app.Modules,
                app => app.MainMenus,
                app => app.Buttons
            };

            var response = await _service.GetApplicationsWithPaginationAndIncludesAsync(predicate, pageNumber, pageSize, includes);
            return Ok(response);
        }



        [HttpPost("GetApplicationsWithPaginationIncludesAndOrderBy")]
        public async Task<IActionResult> GetApplicationsWithPaginationIncludesAndOrderBy([FromBody] ApplicationSearchCriteria applicationSearchCriteria, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            // Build the predicate based on the search criteria using var
            var predicate = (Expression<Func<Application, bool>>)(app =>
                (string.IsNullOrEmpty(applicationSearchCriteria.ApplicationName) || app.ApplicationName.Contains(applicationSearchCriteria.ApplicationName)));

            var orderBy = (Func<IQueryable<Application>, IOrderedQueryable<Application>>)(applications => applications.OrderBy(a => a.ApplicationId));

            var includes = new Expression<Func<Application, object>>[]
            {
                app => app.Modules,
                app => app.MainMenus,
                app => app.Buttons
            };

            var response = await _service.GetApplicationsWithPaginationIncludesAndOrderingAsync(predicate, pageNumber, pageSize, orderBy, includes);

            return Ok(response);
        }


    }
}
