using BLL.Service.Control_Panel.Button_Service;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Control_Panel.Button;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetButtonsController : ControllerBase
    {
        private readonly GetButtonService _getButtonService;

        public GetButtonsController(GetButtonService getButtonService)
        {
            _getButtonService = getButtonService ?? throw new ArgumentNullException(nameof(getButtonService));
        }

        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start


        [HttpGet]
        [Route("buttons/all")]
        public async Task<IActionResult> GetAllButtons()
        {
            var response = await _getButtonService.GetAllButtonsAsync();
            return Ok(response);
        }


        // -------------------------------------------------------------------------------- >>>
        // ................................................ Overload Start

        [HttpGet]
        [Route("buttons/active")]
        public async Task<IActionResult> GetButtons()
        {
            var response = await _getButtonService.GetButtonsAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("buttons/deleted")]
        public async Task<IActionResult> GetDeletedButtons()
        {
            var response = await _getButtonService.GetButtonsAsync(e => e.IsDeleted == true);
            return Ok(response);
        }

        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>





        // -------------------------------------------------------------------------------- >>>
        // ................................................ Overload Start

        [HttpGet]
        [Route("buttons/search")]
        public async Task<IActionResult> GetButtonsByCriteria([FromQuery] ButtonSearchCriteria criteria)
        {
            var response = await _getButtonService.GetButtonsByCriteriaAsync(criteria);
            return Ok(response);
        }

        [HttpGet]
        [Route("buttons/search-by-name")]
        public async Task<IActionResult> GetButtonsByNameCriteria([FromQuery] ButtonSearchCriteria criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Button, bool>> predicate = button =>
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));

            var response = await _getButtonService.GetButtonsByCriteriaAsync(predicate);
            return Ok(response);
        }


        [HttpGet("buttons/search-by-button-and-component-name")]
        public async Task<IActionResult> GetButtonWithComponents([FromQuery] ButtonSearchCriteria criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Button, bool>> predicate = button =>
                (string.IsNullOrEmpty(criteria.ComponentsName) || button.ComponentName.Contains(criteria.ComponentsName)) &&
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));

            var response = await _getButtonService.GetButtonsByCriteriaAsync(predicate);
            return Ok(response);
        }


        [HttpGet]
        [Route("buttons/search-by-exact-name")]
        public async Task<IActionResult> GetButtonsByExactName([FromQuery] ButtonSearchCriteria criteria)
        {
            var response = await _getButtonService.GetButtonsByCriteriaAsync(e => e.ButtonName == "Add");
            return Ok(response);
        }



        [HttpGet]
        [Route("buttons/search-by-predicate")]
        public async Task<IActionResult> GetButtonsByCustomPredicate([FromQuery] ButtonSearchCriteria criteria)
        {
            var predicate = CreateSearchPredicate(criteria);
            var response = await _getButtonService.GetSingleButtonByCriteriaAsync(predicate);
            return Ok(response);
        }

        private Expression<Func<Button, bool>> CreateSearchPredicate(ButtonSearchCriteria criteria)
        {
            return button => (button.ButtonName.Contains("Add"));
        }


        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>


        // -------------------------------------------------------------------------------- >>>
        // ................................................ Overload Start

        [HttpGet("buttons/search-with-ordering")]
        public async Task<IActionResult> GetButtonsWithOrdering([FromQuery] ButtonSearchCriteria criteria)
        {
            var response = await _getButtonService.GetButtonsWithOrderingAsync(criteria);
            return Ok(response);
        }


        [HttpGet("buttons/search-with-ordering2")]
        public async Task<IActionResult> GetButtonsWithOrdering2([FromQuery] ButtonSearchCriteria criteria)
        {
            var response = await _getButtonService.GetButtonsWithOrdering2Async(criteria);
            return Ok(response);
        }


        [HttpGet("buttons/search-with-predicate-and-ordering")]
        public async Task<IActionResult> GetButtonsWithPredicateAndOrdering([FromQuery] ButtonSearchCriteria criteria)
        {
            var predicate = BuildPredicate(criteria);
            var orderBy = BuildOrderBy(criteria);
            var response = await _getButtonService.GetButtonsWithOrderingAsync(predicate, orderBy);
            return Ok(response);
        }

        [HttpGet("buttons/search-with-component-predicate-and-ordering-1")]
        public async Task<IActionResult> GetButtonsWithComponentPredicateAndOrdering1([FromQuery] ButtonSearchCriteria criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Button, bool>> predicate = button =>
                (string.IsNullOrEmpty(criteria.ComponentsName) || button.ComponentName.Contains(criteria.ComponentsName)) &&
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));

            Func<IQueryable<Button>, IOrderedQueryable<Button>> orderBy = q => q.OrderByDescending(button => button.ButtonId);
            var response = await _getButtonService.GetButtonsWithOrderingAsync(predicate, orderBy);
            return Ok(response);
        }

        [HttpGet("buttons/search-with-component-predicate-and-ordering-2")]
        public async Task<IActionResult> GetButtonsWithComponentPredicateAndOrdering2([FromQuery] ButtonSearchCriteria criteria)
        {
            // Construct predicate based on criteria
            Expression<Func<Button, bool>> predicate = button =>
                (string.IsNullOrEmpty(criteria.ComponentsName) || button.ComponentName.Contains(criteria.ComponentsName)) &&
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));

            var orderBy = BuildOrderBy(criteria);
            var response = await _getButtonService.GetButtonsWithOrderingAsync(predicate, orderBy);
            return Ok(response);
        }

        [HttpGet("buttons/search-with-custom-ordering-only")]
        public async Task<IActionResult> GetButtonsWithCustomOrderingOnly([FromQuery] ButtonSearchCriteria criteria)
        {
            var orderBy = BuildOrderBy(criteria);
            var response = await _getButtonService.GetButtonsWithOrderingAsync(criteria, orderBy);
            return Ok(response);
        }


        private Expression<Func<Button, bool>> BuildPredicate(ButtonSearchCriteria criteria)
        {
            return button =>
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));
        }

        private Func<IQueryable<Button>, IOrderedQueryable<Button>> BuildOrderBy(ButtonSearchCriteria criteria)
        {
            return q => q.OrderByDescending(button => button.ButtonId);
        }

        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>



        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>





        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start


        [HttpGet("buttons/get-by-id")]
        public async Task<IActionResult> GetButtonById([FromQuery] long buttonId)
        {
            var response = await _getButtonService.GetSingleButtonByIdAsync(buttonId);
            return Ok(response);
        }

        // -------------------------------------------------------------------------------- >>>
        // ................................................ Get Single Overload Start

        [HttpGet("buttons/get-by-criteria")]
        public async Task<IActionResult> GetButtonByCriteria([FromQuery] ButtonSearchCriteria criteria)
        {
            var response = await _getButtonService.GetSingleButtonByCriteriaAsync(criteria);
            return Ok(response);
        }

        [HttpGet("buttons/get-by-predicate")]
        public async Task<IActionResult> GetButtonByPredicate([FromQuery] ButtonSearchCriteria criteria)
        {
            var predicate = SearchSinglePredicate(criteria);
            var response = await _getButtonService.GetSingleButtonByCriteriaAsync(predicate);
            return Ok(response);
        }

        private Expression<Func<Button, bool>> SearchSinglePredicate(ButtonSearchCriteria criteria)
        {
            return button =>
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));
        }


        // ................................................ Get Single Overload End
        // -------------------------------------------------------------------------------- >>>




        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>

    }
}
