

using AutoMapper;
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
    public class GetApplicationWithPaginationService
    {
        private readonly IMapper _mapper;

        private readonly IGetGenericRepoWithPagination<Application, ControlPanelDbContext, GetApplicationResultViewModel, long> _getApplicationWithPaginationRepository;


        public GetApplicationWithPaginationService(
            IMapper mapper,
            IGetGenericRepoWithPagination<Application, ControlPanelDbContext, GetApplicationResultViewModel, long> getApplicationWithPaginationRepository

            )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _getApplicationWithPaginationRepository = getApplicationWithPaginationRepository;

        }

        public async Task<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>> GetApplicationsWithPaginationAsync(int pageNumber, int pageSize)
        {
            var response = await _getApplicationWithPaginationRepository.GetAllAsync(pageNumber, pageSize);
            return response;
        }



        public async Task<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>> GetApplicationsWithPaginationAsync(ApplicationSearchCriteriaWithPagination criteria, int pageNumber, int pageSize)
        {
            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive);

            var response = await _getApplicationWithPaginationRepository.GetAllAsync(predicate, pageNumber, pageSize);
            return response;
        }



        public async Task<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>> GetApplicationsWithPaginationAsync(Expression<Func<Application, bool>> predicate, int pageNumber, int pageSize)
        {
            var response = await _getApplicationWithPaginationRepository.GetAllAsync(predicate, pageNumber, pageSize);
            return response;
        }




        public async Task<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>> GetApplicationsWithPaginationAndOrderByAsync(Expression<Func<Application, bool>> predicate, int pageNumber, int pageSize)
        {

            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationId);

            var response = await _getApplicationWithPaginationRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize);

            return response;

        }


        public async Task<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>> GetApplicationsWithPaginationAndOrderByAsync(ApplicationSearchCriteriaWithPagination criteria, int pageNumber, int pageSize)
        {

            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive);

            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(app => app.ApplicationId);

            var response = await _getApplicationWithPaginationRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize);

            return response;

        }



        public async Task<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>> GetApplicationsWithPaginationAndOrderByAsync(ApplicationSearchCriteriaWithPagination criteria, int pageNumber, int pageSize, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy)
        {
           
            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive);

            if (orderBy == null)
            {
                orderBy = q => q.OrderBy(app => app.ApplicationId);
            }
            var response = await _getApplicationWithPaginationRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize);

            return response;
            
        }



        public async Task<PaginationResponse<PaginatedList<GetApplicationResultViewModel>>> GetApplicationsWithPaginationAndOrderByAsync(Expression<Func<Application, bool>> predicate, int pageNumber, int pageSize, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy)
        {

            if (orderBy == null)
            {
                orderBy = q => q.OrderBy(app => app.ApplicationId);
            }

            var response = await _getApplicationWithPaginationRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize);

            return response;

        }




    }

}
