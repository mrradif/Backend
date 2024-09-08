using BLL.Service.Control_Panel.Component_Service;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Control_Panel.Search_Component;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Components
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetComponentsController : ControllerBase
    {
        private readonly GetComponentService _getComponentService;

        public GetComponentsController(GetComponentService getComponentService)
        {
            _getComponentService = getComponentService ?? throw new ArgumentNullException(nameof(getComponentService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComponents()
        {
            var response = await _getComponentService.GetAllComponentsAsync();
            return Ok(response);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveComponents()
        {
            var response = await _getComponentService.GetComponentsAsync();
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetComponentsByCriteria([FromQuery] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentService.GetComponentsByCriteriaAsync(criteria);
            return Ok(response);
        }


        [HttpGet("search2")]
        public async Task<IActionResult> GetComponentsByCriteria2([FromQuery] ComponentSearchCriteria criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Component, bool>> predicate = component =>
                (string.IsNullOrEmpty(criteria.ComponentName) || component.ComponentName.Contains(criteria.ComponentName)) &&
                (!criteria.IsActive || component.IsActive);

            var response = await _getComponentService.GetComponentsByCriteriaAsync(predicate);
            return Ok(response);
        }



        [HttpGet("search/order")]
        public async Task<IActionResult> GetComponentsWithOrdering([FromQuery] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentService.GetComponentsWithOrderingAsync(criteria);
            return Ok(response);
        }


        [HttpGet("search/order2")]
        public async Task<IActionResult> GetComponentsWithOrdering2([FromQuery] ComponentSearchCriteria criteria)
        {
            var predicate = BuildPredicate(criteria);
            var orderBy = BuildOrderBy(criteria);
            var response = await _getComponentService.GetComponentsWithOrderingAsync(predicate, orderBy);

            return Ok(response);
        }




        // Get Single Start
        [HttpGet("SearchById")]
        public async Task<IActionResult> GetComponentById([FromQuery] long componentId)
        {
            var response = await _getComponentService.GetSingleComponentByIdAsync(componentId);
            return Ok(response);
        }


        [HttpGet("single")]
        public async Task<IActionResult> GetComponentByCriteria([FromQuery] ComponentSearchCriteria criteria)
        {
            var response = await _getComponentService.GetSingleComponentByCriteriaAsync(criteria);
            return Ok(response);
        }

        [HttpGet("single2")]
        public async Task<IActionResult> GetComponentByCriteria2([FromQuery] ComponentSearchCriteria criteria)
        {
            var predicate = BuildPredicate(criteria);
            var response = await _getComponentService.GetSingleComponentByCriteriaAsync(predicate);
            return Ok(response);

        }




        // Functions Start
        private Expression<Func<Component, bool>> BuildPredicate(ComponentSearchCriteria criteria)
        {
            return component =>
                (string.IsNullOrEmpty(criteria.ComponentName) || component.ComponentName.Contains(criteria.ComponentName)) &&
                (!criteria.IsActive || component.IsActive);
        }

        private Func<IQueryable<Component>, IOrderedQueryable<Component>> BuildOrderBy(ComponentSearchCriteria criteria)
        {
            // Customize sorting based on your criteria
            return q => q.OrderByDescending(component => component.ComponentId);
        }
    }
}
