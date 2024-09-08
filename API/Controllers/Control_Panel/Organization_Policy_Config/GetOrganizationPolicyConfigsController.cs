using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Control_Panel.Organization_Policy_Config;
using Shared.DTO.Common;
using Shared.View.Organization_Policy_Config;
using Shared.Helper.Pagination;
using BLL.Service.Control_Panel.Organization_Policy_Config_Service;

namespace API.Controllers.Control_Panel.Organization_Policy_Config
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrganizationPolicyConfigsController : ControllerBase
    {
        private readonly GetOrganizationPolicyConfigService _getOrganizationPolicyConfigService;

        public GetOrganizationPolicyConfigsController(GetOrganizationPolicyConfigService getOrganizationPolicyConfigService)
        {
            _getOrganizationPolicyConfigService = getOrganizationPolicyConfigService ?? throw new ArgumentNullException(nameof(getOrganizationPolicyConfigService));
        }

        // Get Organization Policy Configs with Pagination
        [HttpGet("WithPagination")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>>> GetOrganizationPolicyConfigsWithPaginationAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getOrganizationPolicyConfigService.GetOrganizationPolicyConfigsWithPaginationAsync(pageNumber, pageSize);
            return Ok(response);
        }

        // Get Organization Policy Configs with Pagination and Search Criteria
        [HttpGet("WithPaginationAndCriteria")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>>> GetOrganizationPolicyConfigsWithPaginationAndCriteriaAsync(
            [FromQuery] OrganizationPolicyConfigSearchCriteria criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var response = await _getOrganizationPolicyConfigService.GetOrganizationPolicyConfigsWithPaginationAsync(criteria, pageNumber, pageSize);
            return Ok(response);
        }


        // Get Organization Policy Configs with Pagination, Search Criteria, and Fixed Order By
        [HttpGet("WithPaginationCriteriaAndOrderBy")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>>> GetOrganizationPolicyConfigsWithPaginationCriteriaAndOrderByAsync(
            [FromQuery] OrganizationPolicyConfigSearchCriteria criteria,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q => q.OrderBy(config => config.OrganizationPolicyConfigId);

            var response = await _getOrganizationPolicyConfigService.GetOrganizationPolicyConfigsWithPaginationWithOrderByAsync(criteria, pageNumber, pageSize, orderBy);
            return Ok(response);
        }

        // Get Organization Policy Configs with Pagination, Search Criteria, and Custom Order By
        [HttpGet("WithPaginationCriteriaAndCustomOrderBy")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>>> GetOrganizationPolicyConfigsWithPaginationCriteriaAndCustomOrderByAsync(
            [FromQuery] OrganizationPolicyConfigSearchCriteria criteria,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "OrganizationPolicyConfigId", // Default sorting field
            [FromQuery] bool ascending = true) // Default sorting order
        {
            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q =>
            {
                switch (sortBy)
                {
                    case "OrganizationPolicyConfigName":
                        return ascending ? q.OrderBy(config => config.OrganizationPolicyConfigName) : q.OrderByDescending(config => config.OrganizationPolicyConfigName);
                    case "OrganizationPolicyConfigId":
                    default:
                        return ascending ? q.OrderBy(config => config.OrganizationPolicyConfigId) : q.OrderByDescending(config => config.OrganizationPolicyConfigId);
                }
            };

            var response = await _getOrganizationPolicyConfigService.GetOrganizationPolicyConfigsWithPaginationWithOrderByAsync(criteria, pageNumber, pageSize, orderBy);
            return Ok(response);
        }

        // Get Organization Policy Configs with Pagination, Search Criteria, and Fixed Order By in Controller
        [HttpGet("WithPaginationCriteriaAndOrderByInController")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>>> GetOrganizationPolicyConfigsWithPaginationCriteriaAndOrderByInControllerAsync(
            [FromQuery] OrganizationPolicyConfigSearchCriteria criteria,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q => q.OrderBy(config => config.OrganizationPolicyConfigId);

            var response = await _getOrganizationPolicyConfigService.GetOrganizationPolicyConfigsWithPaginationWithOrderByAsync(criteria, pageNumber, pageSize, orderBy);
            return Ok(response);
        }

        // Get Organization Policy Configs with Pagination, Search Criteria, Order By, and Sorting
        [HttpGet("WithPaginationCriteriaAndOrderByAndSorting")]
        public async Task<ActionResult<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>>> GetOrganizationPolicyConfigsWithPaginationCriteriaAndOrderByAndSortingAsync(
            [FromQuery] OrganizationPolicyConfigSearchCriteria criteria,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "OrganizationPolicyConfigId", // Default sorting field
            [FromQuery] bool ascending = true) // Default sorting order
        {
            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q =>
            {
                switch (sortBy)
                {
                    case "OrganizationPolicyConfigName":
                        return ascending ? q.OrderBy(config => config.OrganizationPolicyConfigName) : q.OrderByDescending(config => config.OrganizationPolicyConfigName);
                    case "OrganizationPolicyConfigId":
                    default:
                        return ascending ? q.OrderBy(config => config.OrganizationPolicyConfigId) : q.OrderByDescending(config => config.OrganizationPolicyConfigId);
                }
            };

            var response = await _getOrganizationPolicyConfigService.GetOrganizationPolicyConfigsWithPaginationWithOrderByAsync(criteria, pageNumber, pageSize, orderBy);
            return Ok(response);
        }
    }
}
