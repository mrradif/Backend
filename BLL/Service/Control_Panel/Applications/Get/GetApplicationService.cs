using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Applications.Get
{
    public class GetApplicationService
    {
        private readonly IGetGenericRepo<Application, ControlPanelDbContext, GetApplicationResultViewModel, long> _getApplicationRepository;
        private readonly IMapper _mapper;


        public GetApplicationService(
            IGetGenericRepo<Application, ControlPanelDbContext, GetApplicationResultViewModel, long> getApplicationRepository,
            IMapper mapper
        )
        {
            _getApplicationRepository = getApplicationRepository ?? throw new ArgumentNullException(nameof(getApplicationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetAllApplicationsAsync()
        {
            var response = await _getApplicationRepository.GetAllAsync();
            return response;
        }



        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetApplicationsAsync Overload Start

        public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsAsync()
        {
            var response = await _getApplicationRepository.GetAllAsync(b => b.IsDeleted == false);
            return response;
        }

        //public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsAsync()
        //{
        //    Expression<Func<Application, bool>> predicate = app => app.IsDeleted == false;

        //    var response = await _getApplicationRepository.GetAllAsync(predicate);
        //    return response;
        //}

        //private readonly Expression<Func<Application, bool>> _defaultPredicate = app => app.IsDeleted == false;

        //public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsAsync()
        //{
        //    var response = await _getApplicationRepository.GetAllAsync(_defaultPredicate);
        //    return response;
        //}

        //private Expression<Func<Application, bool>> GetDefaultPredicate()
        //{
        //    return app => app.IsDeleted == false;
        //}

        //public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsAsync()
        //{
        //    var response = await _getApplicationRepository.GetAllAsync(GetDefaultPredicate());
        //    return response;
        //}


        public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsAsync(Expression<Func<Application, bool>> predicate)
        {
            var response = await _getApplicationRepository.GetAllAsync(predicate);
            return response;
        }


        // ................................................ GetApplicationsAsync Overload End
        // -------------------------------------------------------------------------------- >>>




        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetApplicationsByCriteriaAsync 

        public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsByCriteriaAsync(ApplicationSearchCriteria criteria)
        {
            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive) &&
                (string.IsNullOrEmpty(criteria.ApplicationType) || app.ApplicationType.Contains(criteria.ApplicationType));

            var response = await _getApplicationRepository.GetAllAsync(predicate);
            return response;
        }


        // ................................................ GetApplicationsByCriteriaAsync 
        // -------------------------------------------------------------------------------- >>>







        // -------------------------------------------------------------------------------- >>>
        // .................... GetApplicationsWithPredicateAndOrderAsync Overload Start


        public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsWithPredicateAndOrderAsync(ApplicationSearchCriteria criteria)
        {
            Expression<Func<Application, bool>> predicate = button =>
                string.IsNullOrEmpty(criteria.ApplicationName) || button.ApplicationName.Contains(criteria.ApplicationName);

            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderByDescending(button => button.ApplicationId);

            var response = await _getApplicationRepository.GetAllAsync(predicate, orderBy);
            return response;
        }


        public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsWithPredicateAndOrderAsync(ApplicationSearchCriteria criteria, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = null)
        {
            Expression<Func<Application, bool>> predicate = button =>
                string.IsNullOrEmpty(criteria.ApplicationName) || button.ApplicationName.Contains(criteria.ApplicationName);

            var response = await _getApplicationRepository.GetAllAsync(predicate, orderBy);
            return response;
        }


        public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsWithPredicateAndOrderAsync(Expression<Func<Application, bool>> predicate, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = null)
        {
            var response = await _getApplicationRepository.GetAllAsync(predicate, orderBy);
            return response;
        }




        //public async Task<Response<IEnumerable<GetApplicationResultViewModel>>> GetApplicationsWithPredicateAndOrderAsync(ApplicationSearchCriteria criteria)
        //{
        //    Expression<Func<Application, bool>> predicate = app =>
        //        (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
        //        (!criteria.IsActive || app.IsActive);

        //    Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = BuildOrderBy(criteria);

        //    var response = await _getApplicationRepository.GetAllAsync(predicate, orderBy);
        //    return response;
        //}

        //private Func<IQueryable<Application>, IOrderedQueryable<Application>> BuildOrderBy(ApplicationSearchCriteria criteria)
        //{
        //    return q => q.OrderByDescending(app => app.ApplicationId);
        //}




        // ...................... GetApplicationsWithPredicateAndOrderAsync Overload End
        // -------------------------------------------------------------------------------- >>>


        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>






        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start



        public async Task<Response<GetApplicationResultViewModel>> GetSingleApplicationById(long applicationId)
        {
            return await _getApplicationRepository.GetSingleAsync(applicationId);
        }


        public async Task<Response<GetApplicationResultViewModel>> GetSingleApplicationById(Expression<Func<Application, bool>> predicate)
        {
            return await _getApplicationRepository.GetSingleAsync(predicate);
        }


        public async Task<Response<GetApplicationResultViewModel>> GetSingleApplicationByCriteriaAsync(ApplicationSearchCriteria criteria)
        {
            Expression<Func<Application, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || app.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || app.IsActive);

            var response = await _getApplicationRepository.GetSingleAsync(predicate);
            return response;
        }

        public async Task<Response<GetApplicationResultViewModel>> GetSingleApplicationByCriteriaAsync(Expression<Func<Application, bool>> predicate)
        {
            return await _getApplicationRepository.GetSingleAsync(predicate);
        }

        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>
    }
}
