using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;



namespace BLL.Service.Control_Panel.Applications.Get
{
    public class GetApplicationIncludesWithPaginationService
    {
        private readonly IGetGenericRepoWithPagination<Application, ControlPanelDbContext, GetApplicationWithIncludesResultViewModel, long> _getApplicationWithPaginationWithModuleRepository;

        public GetApplicationIncludesWithPaginationService(
            IGetGenericRepoWithPagination<Application, ControlPanelDbContext, GetApplicationWithIncludesResultViewModel, long> getApplicationWithPaginationWithModuleRepository)
        {
            _getApplicationWithPaginationWithModuleRepository = getApplicationWithPaginationWithModuleRepository;
        }



        public async Task<PaginationResponse<PaginatedList<GetApplicationWithIncludesResultViewModel>>> GetApplicationsWithPaginationAndIncludesAsync(
            int pageNumber, int pageSize, params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(pageNumber, pageSize, includes);
            return response;
        }


        public async Task<PaginationResponse<PaginatedList<GetApplicationWithIncludesResultViewModel>>> GetApplicationsWithPaginationAndIncludesWithOrderByAsync(
            int pageNumber, int pageSize, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy, params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(pageNumber, pageSize, orderBy, includes);
            return response;
        }


        public async Task<PaginationResponse<PaginatedList<GetApplicationWithIncludesResultViewModel>>> GetApplicationsWithPaginationAndIncludesAsync(
            Expression<Func<Application, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(predicate, pageNumber, pageSize, includes);
            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetApplicationWithIncludesResultViewModel>>> GetApplicationsWithPaginationIncludesAndOrderingAsync(
            Expression<Func<Application, bool>> predicate, int pageNumber, int pageSize, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy, params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationWithPaginationWithModuleRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize, includes);
            return response;
        }


    }
}
