using BLL.Service.Control_Panel.Organization_Policy_Config_Service;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Control_Panel.Organization_Policy_Config;


namespace API.Controllers.Control_Panel.Organization_Policy_Config
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrganizationPolicyConfigsWithCompanyPolicyConfigsController : ControllerBase
    {
        private readonly GetOrganizationPolicyConfigWithCompanyPolicyConfigService _organizationPolicyConfigWithCompanyService;

        public GetOrganizationPolicyConfigsWithCompanyPolicyConfigsController(GetOrganizationPolicyConfigWithCompanyPolicyConfigService organizationPolicyConfigWithCompanyService)
        {
            _organizationPolicyConfigWithCompanyService = organizationPolicyConfigWithCompanyService;
        }

        // 1. Get with pagination and includes
        [HttpGet("GetWithPaginationAndIncludes")]
        public async Task<IActionResult> GetWithPaginationAndIncludes([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _organizationPolicyConfigWithCompanyService.GetOrganizationPolicyConfigsWithPaginationAndIncludesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // 2. Get with pagination, includes, and default ordering
        [HttpGet("GetWithPaginationAndIncludesAndOrderBy")]
        public async Task<IActionResult> GetWithPaginationAndIncludesAndOrderBy([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            // Define default ordering logic
            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q => q.OrderBy(orgPolicy => orgPolicy.OrganizationPolicyConfigName);

            var result = await _organizationPolicyConfigWithCompanyService.GetOrganizationPolicyConfigsWithPaginationAndIncludesWithOrderByAsync(pageNumber, pageSize, orderBy);
            return Ok(result);
        }

        // 3. Get with pagination, includes, and custom ordering
        [HttpGet("GetWithPaginationAndIncludesAndCustomOrderBy")]
        public async Task<IActionResult> GetWithPaginationAndIncludesAndCustomOrderBy([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string orderByField)
        {
            // Define custom ordering logic based on the query parameter
            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = query =>
            {
                return orderByField switch
                {
                    "OrganizationPolicyConfigName" => query.OrderBy(q => q.OrganizationPolicyConfigName),
                    _ => query.OrderBy(q => q.OrganizationPolicyConfigId),
                };
            };

            var result = await _organizationPolicyConfigWithCompanyService.GetOrganizationPolicyConfigsWithPaginationAndIncludesWithOrderByAsync(pageNumber, pageSize, orderBy);
            return Ok(result);
        }

        // 4. Search with pagination and includes (no ordering)
        [HttpPost("SearchWithPaginationAndIncludes")]
        public async Task<IActionResult> SearchWithPaginationAndIncludes([FromBody] OrganizationPolicyConfigSearchCriteria criteria, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _organizationPolicyConfigWithCompanyService.GetOrganizationPolicyConfigsWithPaginationAndIncludesAsync(criteria, pageNumber, pageSize);
            return Ok(result);
        }

        // 5. Search with pagination, includes, and default ordering
        [HttpPost("SearchWithPaginationAndIncludesAndOrderBy")]
        public async Task<IActionResult> SearchWithPaginationAndIncludesAndOrderBy([FromBody] OrganizationPolicyConfigSearchCriteria criteria, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            // Define default ordering logic
            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q => q.OrderBy(orgPolicy => orgPolicy.OrganizationPolicyConfigId);

            var result = await _organizationPolicyConfigWithCompanyService.GetOrganizationPolicyConfigsWithPaginationIncludesAndOrderingAsync(criteria, pageNumber, pageSize, orderBy);
            return Ok(result);
        }

        // 6. Search with pagination, includes, and custom ordering
        [HttpPost("SearchWithPaginationAndIncludesAndCustomOrderBy")]
        public async Task<IActionResult> SearchWithPaginationAndIncludesAndCustomOrderBy([FromBody] OrganizationPolicyConfigSearchCriteria criteria, [FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string orderByField)
        {
            // Define custom ordering logic based on the query parameter
            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = query =>
            {
                return orderByField switch
                {
                    "OrganizationPolicyConfigName" => query.OrderBy(q => q.OrganizationPolicyConfigName),
                    _ => query.OrderBy(q => q.OrganizationPolicyConfigId),
                };
            };

            var result = await _organizationPolicyConfigWithCompanyService.GetOrganizationPolicyConfigsWithPaginationIncludesAndOrderingAsync(criteria, pageNumber, pageSize, orderBy);
            return Ok(result);
        }
    }
}
