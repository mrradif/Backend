using BLL.Service.Control_Panel.Applications.Get;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Applications.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetApplicationWithPagiantionController : ControllerBase
    {
        private readonly GetApplicationWithPaginationService _getApplicationWithPaginationService;
        public GetApplicationWithPagiantionController(
            GetApplicationWithPaginationService getApplicationWithPaginationService
            )
        {
            _getApplicationWithPaginationService = getApplicationWithPaginationService;
        }


        // ..................................................................................................
        // ............................................................. Get All Applications With Pagination
        // .......................................... Starting



        [HttpGet("GetApplications")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplications(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAsync(pageNumber, pageSize);

            return Ok(response);
        }


        [HttpGet("GetApplicationsWithCriteria")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithCriteria(
            [FromQuery] ApplicationSearchCriteriaWithPagination criteria)

        {
            Expression<Func<Application, bool>> predicate = app =>
               (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
               (!criteria.IsActive || app.IsActive);

            var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAsync(predicate, criteria.PageNumber, criteria.PageSize);

            return Ok(response);
        }



        [HttpGet("GetApplicationsWithPredicate")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithPredicate(
        [FromQuery] ApplicationSearchCriteriaWithPagination criteria)

        {
            var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAsync(criteria, criteria.PageNumber, criteria.PageSize);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }



        [HttpGet("GetApplicationsWithPredicateAndOrder")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithPredicateAndOrder(
            [FromQuery] ApplicationSearchCriteriaWithPagination criteria)
        {
            if (criteria.PageNumber < 1)
                criteria.PageNumber = 1;

            if (criteria.PageSize < 1)
                criteria.PageNumber = 2;

            try
            {
                Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationId);

                var response = await _getApplicationWithPaginationService.GetApplicationsWithPaginationAndOrderByAsync(
                    criteria,
                    criteria.PageNumber,
                    criteria.PageSize,
                    orderBy
                );

                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





        [HttpGet("GetApplicationsWithCriteriaAndOrder")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>>> GetApplicationsWithCriteriaAndOrder(
        [FromQuery] ApplicationSearchCriteriaWithPagination criteria)
        {
            if (criteria.PageNumber < 1)
                criteria.PageNumber = 1;

            if (criteria.PageSize < 1)
                criteria.PageSize = 10; 

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

                if (response.Success)
                {
                    return Ok(response);
                }

                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
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



