using BLL.Service.Control_Panel.Applications.Get;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.Helper.Action_Result;
using System.Linq.Expressions;


namespace API.Controllers.Control_Panel.Applications.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetApplicationWithPaginationController : ControllerBase
    {
        private readonly GetApplicationWithPaginationService _getApplicationWithPaginationService;

        public GetApplicationWithPaginationController(
            GetApplicationWithPaginationService getApplicationWithPaginationService
        )
        {
            _getApplicationWithPaginationService = getApplicationWithPaginationService;
        }

        // ..................................................................................................
        // ............................................................. Get All Applications With Pagination
        // .......................................... Starting

        [HttpGet("GetApplications")]
        public async Task<IActionResult> GetApplications(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAsync(pageNumber, pageSize);

            return ActionResultHelper.HandlePaginationResponse(response);
        }

        [HttpGet("GetApplicationsWithCriteria")]
        public async Task<IActionResult> GetApplicationsWithCriteria(
            [FromQuery] ApplicationSearchCriteriaWithPagination criteria)
        {
            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive);

            var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAsync(predicate, criteria.PageNumber, criteria.PageSize);

            return ActionResultHelper.HandlePaginationResponse(response);
        }

        [HttpGet("GetApplicationsWithPredicate")]
        public async Task<IActionResult> GetApplicationsWithPredicate(
            [FromQuery] ApplicationSearchCriteriaWithPagination criteria)
        {
            var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAsync(criteria, criteria.PageNumber, criteria.PageSize);

            return ActionResultHelper.HandlePaginationResponse(response);
        }

        [HttpGet("GetApplicationsWithPredicateAndOrder")]
        public async Task<IActionResult> GetApplicationsWithPredicateAndOrder(
            [FromQuery] ApplicationSearchCriteriaWithPagination criteria)
        {

            if (criteria.PageNumber < 1) criteria.PageNumber = 1;
            if (criteria.PageSize < 1) criteria.PageSize = 10; 

            try
            {
                Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationId);

                var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAndOrderByAsync(
                    criteria,
                    criteria.PageNumber,
                    criteria.PageSize,
                    orderBy
                );

                return ActionResultHelper.HandlePaginationResponse(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetApplicationsWithCriteriaAndOrder")]
        public async Task<IActionResult> GetApplicationsWithCriteriaAndOrder(
            [FromQuery] ApplicationSearchCriteriaWithPagination criteria)
        {

            if (criteria.PageNumber < 1) criteria.PageNumber = 1;
            if (criteria.PageSize < 1) criteria.PageSize = 10;

            try
            {
                Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationId);

                Expression<Func<Application, bool>> predicate = CreateSearchPredicate(criteria);

                var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAndOrderByAsync(
                    predicate,
                    criteria.PageNumber,
                    criteria.PageSize,
                    orderBy
                );

                return ActionResultHelper.HandlePaginationResponse(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private Expression<Func<Application, bool>> CreateSearchPredicate(ApplicationSearchCriteriaWithPagination criteria)
        {
            return application =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || application.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || application.IsActive);
        }

        // .......................................... End
        // ............................................................. Get All Applications With Pagination
    }
}
