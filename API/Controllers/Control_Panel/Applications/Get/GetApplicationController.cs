using BLL.Service.Control_Panel.Applications.Get;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Applications.Get
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetApplicationController : ControllerBase
    {
        private readonly GetApplicationService _getApplicationService;

        public GetApplicationController(GetApplicationService getApplicationService)
        {
            _getApplicationService = getApplicationService ?? throw new ArgumentNullException(nameof(getApplicationService));
        }

        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllApplications()
        {
            var response = await _getApplicationService.GetAllApplicationsAsync();
            return Ok(response);
        }



        // -------------------------------------------------------------------------------- >>>
        // ................................................ Overload Start

        [HttpGet]
        [Route("GetApplications")]
        public async Task<IActionResult> GetApplications()
        {
            var response = await _getApplicationService.GetApplicationsAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetActiveApplications")]
        public async Task<IActionResult> GetActiveApplications()
        {
            var response = await _getApplicationService.GetApplicationsAsync(e => e.IsDeleted == false && e.IsActive == true);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetApprovedApplications")]
        public async Task<IActionResult> GetApprovedApplications()
        {
            var response = await _getApplicationService.GetApplicationsAsync(e => e.IsApproved == true && e.IsActive == true && e.IsDeleted == false);
            return Ok(response);
        }



        [HttpGet]
        [Route("GetRejectedApplications")]
        public async Task<IActionResult> GetRejectedApplications()
        {
            var response = await _getApplicationService.GetApplicationsAsync(GetRejectedPredicate());
            return Ok(response);
        }

        private Expression<Func<Application, bool>> GetRejectedPredicate()
        {
            return app => app.IsRejected == true && app.IsActive == false && app.IsDeleted == false;
        }



        [HttpGet]
        [Route("GetCancelApplications")]
        public async Task<IActionResult> GetCancelApplications()
        {
            var response = await _getApplicationService.GetApplicationsAsync(e => e.IsCancelled == true && e.IsActive == false);
            return Ok(response);
        }



        [HttpGet]
        [Route("GetInActiveApplications")]
        public async Task<IActionResult> GetInActiveApplications()
        {
            Expression<Func<Application, bool>> predicate = e => e.IsDeleted == false && e.IsActive == false;

            var response = await _getApplicationService.GetApplicationsAsync(predicate);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetDeletedApplications")]
        public async Task<IActionResult> GetDeletedApplications()
        {
            var response = await _getApplicationService.GetApplicationsAsync(_defaultPredicate);
            return Ok(response);
        }

        private readonly Expression<Func<Application, bool>> _defaultPredicate = app => app.IsDeleted == true && app.IsActive == false;



        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>



        // -------------------------------------------------------------------------------- >>>
        // ................................................ Overload Start

        [HttpGet]
        [Route("GetApplicationsByCriteria")]
        public async Task<IActionResult> GetApplicationsByCriteria([FromQuery] ApplicationSearchCriteria criteria)
        {
            var response = await _getApplicationService.GetApplicationsByCriteriaAsync(criteria);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetApplicationsByPredicate")]
        public async Task<IActionResult> GetApplicationsByPredicate([FromQuery] ApplicationSearchCriteria criteria)
        {
            var predicate = CreateSearchPredicate(criteria);

            var response = await _getApplicationService.GetSingleApplicationByCriteriaAsync(predicate);
            return Ok(response);
        }

        private Expression<Func<Application, bool>> CreateSearchPredicate(ApplicationSearchCriteria criteria)
        {
            return application => application.ApplicationName.Contains(criteria.ApplicationName);
        }


        [HttpGet]
        [Route("GetApplicationsByName")]
        public async Task<IActionResult> GetApplicationsByName([FromQuery] ApplicationSearchCriteria criteria)
        {
            Expression<Func<Application, bool>> predicate = application =>
                string.IsNullOrEmpty(criteria.ApplicationName) || application.ApplicationName.Contains(criteria.ApplicationName);

            var response = await _getApplicationService.GetApplicationsAsync(predicate);
            return Ok(response);
        }

        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>





        // -------------------------------------------------------------------------------- >>>
        // ................................................ Overload Start

        [HttpGet("GetApplicationsWithPredicateAndOrder")]
        public async Task<IActionResult> GetApplicationsWithPredicateAndOrder([FromQuery] ApplicationSearchCriteria criteria)
        {
            var response = await _getApplicationService.GetApplicationsWithPredicateAndOrderAsync(criteria);
            return Ok(response);
        }


        [HttpGet("GetApplicationsWithPredicateAndOrderBy")]
        public async Task<IActionResult> GetApplicationsWithPredicateAndOrderBy([FromQuery] ApplicationSearchCriteria criteria)
        {
            var predicate = BuildPredicate(criteria);
            var orderBy = BuildOrderBy(criteria);
            var response = await _getApplicationService.GetApplicationsWithPredicateAndOrderAsync(predicate, orderBy);
            return Ok(response);
        }

        private Expression<Func<Application, bool>> BuildPredicate(ApplicationSearchCriteria criteria)
        {
            return application =>
                string.IsNullOrEmpty(criteria.ApplicationName) || application.ApplicationName.Contains(criteria.ApplicationName);
        }

        private Func<IQueryable<Application>, IOrderedQueryable<Application>> BuildOrderBy(ApplicationSearchCriteria criteria)
        {
            return q => q.OrderByDescending(application => application.ApplicationId);
        }




        [HttpGet("GetApplicationsWithPredicateAndOrderByOnly")]
        public async Task<IActionResult> GetApplicationsWithPredicateAndOrderByOnly([FromQuery] ApplicationSearchCriteria criteria)
        {
            var orderBy = BuildOrderBy(criteria);
            var response = await _getApplicationService.GetApplicationsWithPredicateAndOrderAsync(criteria, orderBy);
            return Ok(response);
        }


        // ................................................ Overload End
        // -------------------------------------------------------------------------------- >>>


        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>




        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        [HttpGet("GetApplicationById")]
        public async Task<IActionResult> GetApplicationById([FromQuery] long applicationId)
        {
            var response = await _getApplicationService.GetSingleApplicationById(applicationId);
            return Ok(response);
        }



        // -------------------------------------------------------------------------------- >>>
        // ................................................ Get Single Overload Start

        [HttpGet("GetApplicationByCriteria")]
        public async Task<IActionResult> GetApplicationByCriteria([FromQuery] ApplicationSearchCriteria criteria)
        {
            var response = await _getApplicationService.GetSingleApplicationByCriteriaAsync(criteria);
            return Ok(response);
        }

        [HttpGet("GetApplicationByPredicate")]
        public async Task<IActionResult> GetApplicationByPredicate([FromQuery] ApplicationSearchCriteria criteria)
        {
            var predicate = SearchSinglePredicate(criteria);
            var response = await _getApplicationService.GetSingleApplicationByCriteriaAsync(predicate);
            return Ok(response);
        }

        private Expression<Func<Application, bool>> SearchSinglePredicate(ApplicationSearchCriteria criteria)
        {
            return application =>
                string.IsNullOrEmpty(criteria.ApplicationName) || application.ApplicationName.Contains(criteria.ApplicationName);
        }



        // ................................................ Get Single Overload End
        // -------------------------------------------------------------------------------- >>>

        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>
    }
}
