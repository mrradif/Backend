
using System.Linq.Expressions;
using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Control_Panel.Organization_Policy_Config;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using Shared.View.Organization_Policy_Config;

namespace BLL.Service.Control_Panel.Organization_Policy_Config_Service
{
    public class GetOrganizationPolicyConfigWithCompanyPolicyConfigService
    {
        private readonly IMapper _mapper;

        private readonly IGetGenericRepoWithPagination<OrganizationPolicyConfig, ControlPanelDbContext, GetOrganizationPolicyConfigWithCompaniesResultViewModel, long> _getOrganizationPolicyConfigWithCompaniesPaginationRepository;

        public GetOrganizationPolicyConfigWithCompanyPolicyConfigService(
            IMapper mapper,
            IGetGenericRepoWithPagination<OrganizationPolicyConfig, ControlPanelDbContext, GetOrganizationPolicyConfigWithCompaniesResultViewModel, long> getOrganizationPolicyConfigWithCompaniesPaginationRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _getOrganizationPolicyConfigWithCompaniesPaginationRepository = getOrganizationPolicyConfigWithCompaniesPaginationRepository;
        }

        // With Pagination and Includes
        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigWithCompaniesResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationAndIncludesAsync(int pageNumber, int pageSize)
        {
            var response = await _getOrganizationPolicyConfigWithCompaniesPaginationRepository.GetAllAsync(pageNumber, pageSize, o => o.CompanyPolicyConfigs);
            return response;
        }

        // With Pagination, Includes, and OrderBy
        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigWithCompaniesResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationAndIncludesWithOrderByAsync(int pageNumber, int pageSize)
        {
            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q => q.OrderBy(orgPolicy => orgPolicy.OrganizationPolicyConfigName);

            var response = await _getOrganizationPolicyConfigWithCompaniesPaginationRepository.GetAllAsync(pageNumber, pageSize, orderBy, o => o.CompanyPolicyConfigs);

            return response;
        }

        // With Pagination, Includes, OrderBy, and Custom Ordering
        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigWithCompaniesResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationAndIncludesWithOrderByAsync(int pageNumber, int pageSize, Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy)
        {
            if (orderBy == null)
            {
                orderBy = q => q.OrderBy(orgPolicy => orgPolicy.OrganizationPolicyConfigId);
            }

            var response = await _getOrganizationPolicyConfigWithCompaniesPaginationRepository.GetAllAsync(pageNumber, pageSize, orderBy, o => o.CompanyPolicyConfigs);

            return response;
        }

        // With Pagination, Includes, and Search Criteria
        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigWithCompaniesResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationAndIncludesAsync(OrganizationPolicyConfigSearchCriteria criteria, int pageNumber, int pageSize)
        {
            Expression<Func<OrganizationPolicyConfig, bool>> predicate = orgPolicy =>
                (string.IsNullOrEmpty(criteria.OrganizationPolicyConfigName) || orgPolicy.OrganizationPolicyConfigName.Contains(criteria.OrganizationPolicyConfigName)) &&
                (!orgPolicy.IsActive || criteria.IsActive);

            var response = await _getOrganizationPolicyConfigWithCompaniesPaginationRepository.GetAllAsync(predicate, pageNumber, pageSize, o => o.CompanyPolicyConfigs);
            return response;
        }

        // With Pagination, Includes, OrderBy, and Search Criteria
        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigWithCompaniesResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationIncludesAndOrderingAsync(OrganizationPolicyConfigSearchCriteria criteria, int pageNumber, int pageSize, Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy)
        {
            Expression<Func<OrganizationPolicyConfig, bool>> predicate = orgPolicy =>
                (string.IsNullOrEmpty(criteria.OrganizationPolicyConfigName) || orgPolicy.OrganizationPolicyConfigName.Contains(criteria.OrganizationPolicyConfigName)) &&
                (!orgPolicy.IsActive || criteria.IsActive);

            if (orderBy == null)
            {
                orderBy = q => q.OrderBy(orgPolicy => orgPolicy.OrganizationPolicyConfigId);
            }

            var response = await _getOrganizationPolicyConfigWithCompaniesPaginationRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize, o => o.CompanyPolicyConfigs);

            return response;
        }

        // With Pagination, Includes, and Search Criteria with Default Ordering
        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigWithCompaniesResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationIncludesAndOrderingAsync(OrganizationPolicyConfigSearchCriteria criteria, int pageNumber, int pageSize)
        {
            Expression<Func<OrganizationPolicyConfig, bool>> predicate = orgPolicy =>
                (string.IsNullOrEmpty(criteria.OrganizationPolicyConfigName) || orgPolicy.OrganizationPolicyConfigName.Contains(criteria.OrganizationPolicyConfigName)) &&
                (!orgPolicy.IsActive || criteria.IsActive);

            Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q => q.OrderBy(orgPolicy => orgPolicy.OrganizationPolicyConfigId);

            var response = await _getOrganizationPolicyConfigWithCompaniesPaginationRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize, o => o.CompanyPolicyConfigs);

            return response;
        }
    }
}
