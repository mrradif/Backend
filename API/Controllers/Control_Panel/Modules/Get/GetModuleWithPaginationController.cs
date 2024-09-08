using BLL.Service.Control_Panel.Modules.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using Shared.View.Control_Panel.Modules;

namespace API.Controllers.Control_Panel.Modules.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetModuleWithPaginationController : ControllerBase
    {
        private readonly GetModuleWithPaginationService _getModuleWithPaginationService;

        public GetModuleWithPaginationController(
            GetModuleWithPaginationService getModuleWithPaginationService
            )
        {
            _getModuleWithPaginationService = getModuleWithPaginationService;
        }




        // ..................................................................................................
        // ............................................................. Get All Modules With Pagination
        // .......................................... Starting

        [HttpGet("ModulesWithPagination")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> GetModulesWithPaginationAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getModuleWithPaginationService.GetModulesWithPaginationAsync(pageNumber, pageSize);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }




        // .......................................................Get Modules with pagination and search criteria
        [HttpGet("ModulesWithPaginationAndCriteria")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> GetModulesWithPaginationAndCriteriaAsync(
            [FromQuery] ModuleSearchCriteriaWithPagination criteria)
        {
            var response = await _getModuleWithPaginationService.GetModulesWithPaginationAsync(criteria, criteria.PageNumber, criteria.PageSize);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        // .......................................................Get Modules with pagination, search criteria, and Order By
        [HttpGet("ModulesWithPaginationCriteriaAndOrderBy")]
        public async Task<IActionResult> GetModulesWithPagination(
            [FromQuery] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 2)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 2;

            var response = await _getModuleWithPaginationService.GetModulesWithPaginationAsync(criteria, pageNumber, pageSize);

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

        // ............................................ Get Modules with pagination, search criteria, and Order By in Controller
        [HttpGet("ModulesWithPaginationCriteriaAndOrderByInController")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> ModulesWithPaginationCriteriaAndOrderByInController(
            [FromQuery] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 2)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 2;

            try
            {
                // Define fixed ordering logic
                Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(mod => mod.ModuleId);

                var response = await _getModuleWithPaginationService.GetModulesWithPaginationWithOrderByAsync(
                    criteria,
                    pageNumber,
                    pageSize,
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

        // ...................................... ...... Get Modules with pagination, search criteria, ordering, and sorting
        [HttpGet("ModulesWithPaginationCriteriaAndOrderByAndSorting")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> ModulesWithPaginationCriteriaAndOrderByAndSorting(
            [FromQuery] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string sortBy = "ModuleId", // Default sorting field
            [FromQuery] bool ascending = true) // Default sorting order
        {
            try
            {
                // Define ordering logic based on query parameters
                Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q =>
                {
                    switch (sortBy)
                    {
                        case "ModuleName":
                            return ascending ? q.OrderBy(mod => mod.ModuleName) : q.OrderByDescending(mod => mod.ModuleName);
                        case "ModuleId":
                        default:
                            return ascending ? q.OrderBy(mod => mod.ModuleId) : q.OrderByDescending(mod => mod.ModuleId);
                    }
                };

                var response = await _getModuleWithPaginationService.GetModulesWithPaginationWithOrderByAsync(
                    criteria,
                    pageNumber,
                    pageSize,
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

        // .......................................... End
        // ............................................................. Get All Modules With Pagination










        // ................................................................................................
        // ................................................. Get Modules With Pagination with Includes
        // ..................................... Starting

        // .............................. Get modules with pagination and includes
        [HttpGet("ModulesWithPaginationAndIncludes")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> GetModulesWithPaginationAndIncludesAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getModuleWithPaginationService.GetModulesWithPaginationAndIncludesAsync(pageNumber, pageSize);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        // ................................. Get modules with pagination, includes, and default ordering
        [HttpGet("ModulesWithPaginationAndIncludesWithOrderBy")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> GetModulesWithPaginationAndIncludesWithOrderByAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            // Define default ordering logic
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(mod => mod.ModuleName);

            var response = await _getModuleWithPaginationService.GetModulesWithPaginationAndIncludesWithOrderByAsync(
                pageNumber,
                pageSize,
                orderBy
            );

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        // .................................. Get modules with pagination, includes, and a custom ordering function
        [HttpGet("ModulesWithPaginationAndIncludesWithCustomOrderBy")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> GetModulesWithPaginationAndIncludesWithCustomOrderByAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string orderByProperty) // Example: "ModuleName" or "ModuleId"
        {
            // Define ordering logic based on the query parameter
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q =>
            {
                switch (orderByProperty?.ToLower())
                {
                    case "modulename":
                        return q.OrderBy(mod => mod.ModuleName);
                    case "moduleid":
                        return q.OrderBy(mod => mod.ModuleId);
                    default:
                        return q.OrderBy(mod => mod.ModuleId); // Default ordering
                }
            };

            var response = await _getModuleWithPaginationService.GetModulesWithPaginationAndIncludesWithOrderByAsync(
                pageNumber,
                pageSize,
                orderBy
            );

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        // ............................. Get modules with pagination, includes, and search criteria without ordering
        [HttpGet("ModulesWithPaginationIncludes")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> GetModulesWithPaginationIncludesAsync(
            [FromQuery] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getModuleWithPaginationService.GetModulesWithPaginationAndIncludesAsync(
                criteria,
                pageNumber,
                pageSize
            );

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        // .......................... Get modules with pagination, includes, ordering, and search criteria
        [HttpGet("ModulesWithPaginationIncludesAndOrdering")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> GetModulesWithPaginationIncludesAndOrderingAsync(
            [FromQuery] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            // Define ordering logic
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(mod => mod.ModuleId);

            var response = await _getModuleWithPaginationService.GetModulesWithPaginationIncludesAndOrderingAsync(
                criteria,
                pageNumber,
                pageSize,
                orderBy
            );

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        // ............................ Get modules with pagination, includes, search criteria, and custom ordering
        [HttpGet("ModulesWithPaginationIncludesAndCustomOrdering")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetModuleResultViewModel>>>> GetModulesWithPaginationIncludesAndCustomOrderingAsync(
            [FromQuery] ModuleSearchCriteriaWithPagination criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string orderByProperty) // Example: "ModuleName" or "ModuleId"
        {
            // Define custom ordering logic based on the query parameter
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q =>
            {
                switch (orderByProperty?.ToLower())
                {
                    case "modulename":
                        return q.OrderBy(mod => mod.ModuleName);
                    case "moduleid":
                        return q.OrderBy(mod => mod.ModuleId);
                    default:
                        return q.OrderBy(mod => mod.ModuleId); // Default ordering
                }
            };

            var response = await _getModuleWithPaginationService.GetModulesWithPaginationIncludesAndOrderingAsync(
                criteria,
                pageNumber,
                pageSize,
                orderBy
            );

            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        // ..................................... End
        // ................................................. Get Modules With Pagination with Includes 



    }
}
