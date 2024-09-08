using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Applications.Get
{
    public class GetApplicationsAndModulesWithPaginationService
    {
        private readonly IGetGenericRepoWithPagination<Application, ControlPanelDbContext, GetApplicationWithModuleResultViewModel, long> _getApplicationWithPaginationWithModuleRepository;

        public GetApplicationsAndModulesWithPaginationService(
             IGetGenericRepoWithPagination<Application, ControlPanelDbContext, GetApplicationWithModuleResultViewModel, long> getApplicationWithPaginationWithModuleRepository
            )
        {
            _getApplicationWithPaginationWithModuleRepository = getApplicationWithPaginationWithModuleRepository;
        }


        // With Pagination With Includes

        public async Task<PaginationResponse<PaginatedList<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithPaginationAndIncludesAsync(int pageNumber, int pageSize)
        {
            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(pageNumber, pageSize, a => a.Modules);
            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithPaginationAndIncludesWithOrderByAsync(
            int pageNumber,
            int pageSize)
        {
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationName);

            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(
                pageNumber,
                pageSize,
                orderBy, 
                a => a.Modules 
            );

            return response;
        }


        public async Task<PaginationResponse<PaginatedList<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithPaginationAndIncludesWithOrderByAsync(
        int pageNumber,
        int pageSize, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy)
        {

            if (orderBy == null)
            {
                orderBy = q => q.OrderBy(app => app.ApplicationId); 
            }

            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(
                pageNumber,
                pageSize,
                orderBy, 
                a => a.Modules 
            );

            return response;
        }




        public async Task<PaginationResponse<PaginatedList<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithPaginationAndIncludesAsync(ApplicationSearchCriteria criteria, int pageNumber, int pageSize)
        {
            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive);

            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(predicate, pageNumber, pageSize, a => a.Modules);
            return response;
        }




        public async Task<PaginationResponse<PaginatedList<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithPaginationIncludesAndOrderingAsync(
            ApplicationSearchCriteria criteria,
            int pageNumber,
            int pageSize,
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy)
        {
            // Define the filter criteria
            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive);

            if (orderBy == null)
            {
                orderBy = q => q.OrderBy(app => app.ApplicationId); 
            }

            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(
                predicate,
                orderBy,
                pageNumber,
                pageSize,
                a => a.Modules
            );

            return response;
        }





        public async Task<PaginationResponse<PaginatedList<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithPaginationIncludesAndOrderingAsync(
            ApplicationSearchCriteria criteria,
            int pageNumber,
            int pageSize)
        {
            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive);

            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationId);

            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(
                predicate,
                orderBy,
                pageNumber,
                pageSize,
                a => a.Modules
            );

            return response;
        }


    }
}
