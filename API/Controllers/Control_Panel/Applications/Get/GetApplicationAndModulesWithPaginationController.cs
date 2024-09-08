using BLL.Service.Control_Panel.Applications.Get;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using Shared.View.Control_Panel.Applications;

namespace API.Controllers.Control_Panel.Applications.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetApplicationAndModulesWithPaginationController : ControllerBase
    {
        private readonly GetApplicationsAndModulesWithPaginationService _getApplicationsAndModulesWithPaginationService;
        public GetApplicationAndModulesWithPaginationController(
            GetApplicationsAndModulesWithPaginationService getApplicationsAndModulesWithPaginationService
            )
        {
            _getApplicationsAndModulesWithPaginationService = getApplicationsAndModulesWithPaginationService;
        }



        // ................................................................................................
        // ................................................. Get Application With Pagination with Includes 
        // ..................................... Starting

        // .............................. Get applications with pagination and includes
        [HttpGet("ApplicationWithPaginationAndIncludes")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithPaginationAndIncludesAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getApplicationsAndModulesWithPaginationService.GetApplicationsWithPaginationAndIncludesAsync(pageNumber, pageSize);
            return Ok(response);
        }



        // ................................. Get applications with pagination, includes, and default ordering
        [HttpGet("ApplicationWithPaginationAndIncludesWithOrderBy")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithPaginationAndIncludesWithOrderByAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            // Define default ordering logic
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationName);

            var response = await _getApplicationsAndModulesWithPaginationService.GetApplicationsWithPaginationAndIncludesWithOrderByAsync(
                pageNumber,
                pageSize,
                orderBy
            );

            return Ok(response);
        }



        // .................................. Get applications with pagination, includes, and a custom ordering function
        [HttpGet("ApplicationWithPaginationAndIncludesWithCustomOrderBy")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithPaginationAndIncludesWithCustomOrderByAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string orderByProperty) // Example: "ApplicationName" or "ApplicationId"
        {
            // Define ordering logic based on the query parameter
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q =>
            {
                switch (orderByProperty?.ToLower())
                {
                    case "applicationname":
                        return q.OrderBy(app => app.ApplicationName);
                    case "applicationid":
                        return q.OrderBy(app => app.ApplicationId);
                    default:
                        return q.OrderBy(app => app.ApplicationId); // Default ordering
                }
            };

            var response = await _getApplicationsAndModulesWithPaginationService.GetApplicationsWithPaginationAndIncludesWithOrderByAsync(
                pageNumber,
                pageSize,
                orderBy
            );

            return Ok(response);
        }







        // ............................. Get applications with pagination, includes, and search criteria without ordering
        [HttpGet("ApplicationWithPaginationIncludes")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithPaginationIncludesAsync(
            [FromQuery] ApplicationSearchCriteria criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getApplicationsAndModulesWithPaginationService.GetApplicationsWithPaginationAndIncludesAsync(
                criteria,
                pageNumber,
                pageSize
            );

            return Ok(response);
        }




        // .......................... Get applications with pagination, includes, ordering, and search criteria
        [HttpGet("ApplicationWithPaginationIncludesAndOrdering")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithPaginationIncludesAndOrderingAsync(
            [FromQuery] ApplicationSearchCriteria criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            // Define ordering logic
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationId);

            var response = await _getApplicationsAndModulesWithPaginationService.GetApplicationsWithPaginationIncludesAndOrderingAsync(
                criteria,
                pageNumber,
                pageSize,
                orderBy
            );

            return Ok(response);
        }



        // ............................ Get applications with pagination, includes, search criteria, and custom ordering
        [HttpGet("ApplicationWithPaginationIncludesAndCustomOrdering")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithPaginationIncludesAndCustomOrderingAsync(
            [FromQuery] ApplicationSearchCriteria criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string orderByProperty) // Example: "ApplicationName" or "ApplicationId"
        {
            // Define custom ordering logic based on the query parameter
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q =>
            {
                switch (orderByProperty?.ToLower())
                {
                    case "applicationname":
                        return q.OrderBy(app => app.ApplicationName);
                    case "applicationid":
                        return q.OrderBy(app => app.ApplicationId);
                    default:
                        return q.OrderBy(app => app.ApplicationId); // Default ordering
                }
            };

            var response = await _getApplicationsAndModulesWithPaginationService.GetApplicationsWithPaginationIncludesAndOrderingAsync(
                criteria,
                pageNumber,
                pageSize,
                orderBy
            );

            return Ok(response);
        }

        // ..................................... End
        // ................................................. Get Application With Pagination with Includes 

    }
}
