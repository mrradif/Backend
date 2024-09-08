using BLL.Service.Control_Panel.Button_Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Criteria.Control_Panel.Button;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetButtonsWithComponentsController : ControllerBase
    {
        private readonly GetButtonWithComponentService _getButtonWithComponentService;

        public GetButtonsWithComponentsController(GetButtonWithComponentService getButtonWithComponentService)
        {
            _getButtonWithComponentService = getButtonWithComponentService ?? throw new ArgumentNullException(nameof(getButtonWithComponentService));
        }


        // ............................................................ Get All
        // ............................................. Start

        [HttpGet("GetAllButtonWithComponets")]
        public async Task<IActionResult> GetAllButtonWithComponets()
        {
            var response = await _getButtonWithComponentService.GetAllButtonsWithComponentsAsync();
            return Ok(response);
        }


        [HttpGet("GetAllButtonWithComponets2")]
        public async Task<IActionResult> GetAllButtonWithComponets2()
        {
            var response = await _getButtonWithComponentService.GetAllButtonsWithComponentsAsync(b => b.Components);
            return Ok(response);
        }




        [HttpGet("GetButtonWithComponets")]
        public async Task<IActionResult> ActiveButtonWithComponets()
        {
            var response = await _getButtonWithComponentService.GetButtonsWithComponentsAsync();
            return Ok(response);
        }


        [HttpGet("GetButtonWithComponets2")]
        public async Task<IActionResult> ActiveButtonWithComponets2()
        {
            Expression<Func<Button, bool>> predicate = b => b.IsDeleted == false && b.IsActive == true;

            var response = await _getButtonWithComponentService.GetButtonsWithComponentsAsync(
                predicate,
                b => b.Components
            );
            return Ok(response);
        }




        [HttpGet("GetButtonsWithComponentsOrdered")]
        public async Task<IActionResult> GetButtonsWithComponentsOrdered()
        {
            // Define the predicate, orderBy, and includes
            var parameters = (
                predicate: (Expression<Func<Button, bool>>)(b => b.IsDeleted == false), // Example predicate
                orderBy: (Func<IQueryable<Button>, IOrderedQueryable<Button>>)(query => query.OrderBy(b => b.ButtonName)), // Example ordering
                includes: new Expression<Func<Button, object>>[] { b => b.Components } 
            );

            var response = await _getButtonWithComponentService.GetButtonsWithComponentsOrderedAsync(parameters);
            return Ok(response); 
        }



        [HttpGet("GetButtonsWithComponentsOrdered2")]
        public async Task<IActionResult> GetButtonsWithComponentsOrdered2()
        {
            var response = await _getButtonWithComponentService.GetButtonsWithComponentsOrderedAsync();
            return Ok(response);
        }


        [HttpGet("GetButtonsWithComponentsOrdered3")]
        public async Task<IActionResult> GetButtonsWithComponentsOrdered3([FromQuery] ButtonSearchCriteria criteria)
        {
            var response = await _getButtonWithComponentService.GetButtonsWithComponentsOrderedAsync(criteria);
            return Ok(response);
        }


        // ............................................. End
        // ............................................................ Get All





        // ......................................................... Get Single
        // .................................... Start



        [HttpGet("GetSingleButtonWithComponent")]
        public async Task<IActionResult> GetSingleButtonWithComponent([FromQuery] string buttonName)
        {
            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(b => b.ButtonName == buttonName && b.IsDeleted == false);
            return Ok(response);
        }


        [HttpGet("GetSingleButtonWithComponentWithIncludes")]
        public async Task<IActionResult> GetSingleButtonWithComponentWithIncludes(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Button, bool>> predicate = b => b.IsActive == isActive && !b.IsDeleted;

            // Build the includes array based on the passed properties
            Expression<Func<Button, object>>[] includes = includeProperties
                .Select(prop => (Expression<Func<Button, object>>)(b => EF.Property<object>(b, prop)))
                .ToArray();

            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(predicate, includes);
            return Ok(response);
        }


        [HttpGet("GetSingleButtonWithComponentWithIncludes2")]
        public async Task<IActionResult> GetSingleButtonWithComponentWithIncludes2(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            // Define your specific predicate
            Expression<Func<Button, bool>> predicate = b => b.IsActive == isActive && !b.IsDeleted;

            // Define the specific includes that you always want to include
            List<Expression<Func<Button, object>>> specificIncludes = new List<Expression<Func<Button, object>>>
                {
                    b => b.Components
                };

            // Build the includes array based on the passed properties and add them to the specific includes
            specificIncludes.AddRange(
                includeProperties.Select(prop => (Expression<Func<Button, object>>)(b => EF.Property<object>(b, prop)))
            );

            // Convert the List to an array as required by your method signature
            var includesArray = specificIncludes.ToArray();

            // Pass the predicate and includes array to your service method
            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(predicate, includesArray);

            return Ok(response);
        }




        [HttpGet("GetSingleButtonWithComponentWithIncludes3")]
        public async Task<IActionResult> GetSingleButtonWithComponentWithIncludes3(
            [FromQuery] bool isActive,
            [FromQuery] string[] includeProperties)
        {
            // Define your specific predicate
            Expression<Func<Button, bool>> predicate = b => b.IsActive == isActive && !b.IsDeleted;


            // Pass the predicate and includes array to your service method
            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(predicate, b=> b.Components);

            return Ok(response);
        }







        [HttpGet("GetSingleButtonWithComponentByCriteria")]
        public async Task<IActionResult> GetSingleButtonWithComponentByCriteria([FromQuery] ButtonSearchCriteria criteria)
        {
            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(criteria);
            return Ok(response);
        }

        [HttpGet("GetSingleButtonWithComponentByCriteria2")]
        public async Task<IActionResult> GetSingleButtonWithComponentByCriteria2([FromQuery] ButtonSearchCriteria criteria)
        {
            Expression<Func<Button, bool>> predicate = b => b.IsActive == criteria.IsActive;

            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(predicate);
            return Ok(response);
        }



        [HttpPost("GetSingleButtonWithComponentByCriteriaWithIncludes")]
        public async Task<IActionResult> GetSingleButtonWithComponentByCriteriaWithIncludes(
            [FromBody] ButtonSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            // Build the includes array based on the passed properties
            Expression<Func<Button, object>>[] includes = includeProperties
                .Select(prop => (Expression<Func<Button, object>>)(b => EF.Property<object>(b, prop)))
                .ToArray();

            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(criteria, includes);
            return Ok(response);
        }




        [HttpPost("GetSingleButtonWithComponentByCriteriaWithIncludes2")]
        public async Task<IActionResult> GetSingleButtonWithComponentByCriteriaWithIncludes2(
            [FromBody] ButtonSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(criteria, b => b.Components);
            return Ok(response);
        }


        [HttpPost("GetSingleButtonWithComponentByCriteriaWithIncludes3")]
        public async Task<IActionResult> GetSingleButtonWithComponentByCriteriaWithIncludes3(
            [FromBody] ButtonSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            // Define specific includes
            Expression<Func<Button, object>>[] includes = new Expression<Func<Button, object>>[]
            {
                b => b.Components
                // Add other static includes here if needed
                    };

            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(criteria, b => b.Components);
            return Ok(response);
        }


        [HttpPost("GetSingleButtonWithComponentByCriteriaWithIncludes4")]
        public async Task<IActionResult> GetSingleButtonWithComponentByCriteriaWithIncludes4(
            [FromBody] ButtonSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            Expression<Func<Button, bool>> predicate = b => b.IsActive == criteria.IsActive;
            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(predicate, b => b.Components);
            return Ok(response);
        }



        [HttpPost("GetSingleButtonWithComponentByCriteriaWithIncludes5")]
        public async Task<IActionResult> GetSingleButtonWithComponentByCriteriaWithIncludes5(
        [FromBody] ButtonSearchCriteria criteria,
        [FromQuery] string[] includeProperties)
        {
            Expression<Func<Button, bool>> predicate = b => b.IsActive == criteria.IsActive;

            // Define the specific includes that you always want to include
            List<Expression<Func<Button, object>>> specificIncludes = new List<Expression<Func<Button, object>>>
                {
                    b => b.Components
                };

            // Build the includes array based on the passed properties and add them to the specific includes
            specificIncludes.AddRange(
                includeProperties.Select(prop => (Expression<Func<Button, object>>)(b => EF.Property<object>(b, prop)))
            );

            // Convert the List to an array as required by your method signature
            var includesArray = specificIncludes.ToArray();

            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(predicate, includesArray);
            return Ok(response);
        }


        [HttpPost("GetSingleButtonWithComponentByCriteriaWithIncludes6")]
        public async Task<IActionResult> GetSingleButtonWithComponentByCriteriaWithIncludes6(
            [FromBody] ButtonSearchCriteria criteria,
            [FromQuery] string[] includeProperties)
        {
            // Define specific includes
            Expression<Func<Button, object>>[] includes = new Expression<Func<Button, object>>[]
            {
        b => b.Components
        // Add other static includes here if needed
            };

            // Combine specific includes with dynamic includes if needed
            var allIncludes = includes.Concat(includeProperties
                .Select(prop => CreateIncludeExpression(prop))).ToArray();

            var response = await _getButtonWithComponentService.GetSingleButtonWithComponentAsync(criteria, includes);
            return Ok(response);
        }


        // Helper method to create include expressions
        private Expression<Func<Button, object>> CreateIncludeExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(Button), "b");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda<Func<Button, object>>(Expression.Convert(property, typeof(object)), parameter);
            return lambda;
        }

        // .................................... End
        // ......................................................... Get Single

    }
}
