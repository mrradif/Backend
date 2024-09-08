using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Control_Panel.Organization_Policy_Config;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using Shared.View.Organization_Policy_Config;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Organization_Policy_Config_Service
{
    public class GetOrganizationPolicyConfigService
    {
        private readonly IMapper _mapper;

        private readonly IGetGenericRepoWithPagination<OrganizationPolicyConfig, ControlPanelDbContext, GetOrganizationPolicyConfigResultViewModel, long> _getOrganizationPolicyConfigRepository;

        public GetOrganizationPolicyConfigService(
            IMapper mapper,
            IGetGenericRepoWithPagination<OrganizationPolicyConfig, ControlPanelDbContext, GetOrganizationPolicyConfigResultViewModel, long> getOrganizationPolicyConfigRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _getOrganizationPolicyConfigRepository = getOrganizationPolicyConfigRepository;
        }

        // With Pagination
        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationAsync(int pageNumber, int pageSize)
        {
            var response = await _getOrganizationPolicyConfigRepository.GetAllAsync(pageNumber, pageSize);
            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationAsync(OrganizationPolicyConfigSearchCriteria criteria, int pageNumber, int pageSize)
        {
            Expression<Func<OrganizationPolicyConfig, bool>> predicate = config =>
                (string.IsNullOrEmpty(criteria.OrganizationPolicyConfigName) || config.OrganizationPolicyConfigName.Contains(criteria.OrganizationPolicyConfigName)) &&
                (!config.IsActive || criteria.IsActive);

            var response = await _getOrganizationPolicyConfigRepository.GetAllAsync(predicate, pageNumber, pageSize);
            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationWithOrderByAsync(OrganizationPolicyConfigSearchCriteria criteria, int pageNumber, int pageSize)
        {
            try
            {
                Expression<Func<OrganizationPolicyConfig, bool>> predicate = config =>
                    (string.IsNullOrEmpty(criteria.OrganizationPolicyConfigName) || config.OrganizationPolicyConfigName.Contains(criteria.OrganizationPolicyConfigName)) &&
                    (!config.IsActive || criteria.IsActive);

                Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy = q => q.OrderBy(config => config.OrganizationPolicyConfigId);

                var response = await _getOrganizationPolicyConfigRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize);

                if (response.Success)
                {
                    return new PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>(response.Data);
                }
                return new PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>(response.Message);
            }
            catch (Exception ex)
            {
                return new PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>(ex.Message);
            }
        }

        public async Task<PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>> GetOrganizationPolicyConfigsWithPaginationWithOrderByAsync(OrganizationPolicyConfigSearchCriteria criteria, int pageNumber, int pageSize, Func<IQueryable<OrganizationPolicyConfig>, IOrderedQueryable<OrganizationPolicyConfig>> orderBy)
        {
            try
            {
                Expression<Func<OrganizationPolicyConfig, bool>> predicate = config =>
                    (string.IsNullOrEmpty(criteria.OrganizationPolicyConfigName) || config.OrganizationPolicyConfigName.Contains(criteria.OrganizationPolicyConfigName)) &&
                    config.IsActive == criteria.IsActive;

                if (orderBy == null)
                {
                    orderBy = q => q.OrderBy(config => config.OrganizationPolicyConfigId);
                }

                var response = await _getOrganizationPolicyConfigRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize);

                if (response.Success)
                {
                    return new PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>(response.Data);
                }
                return new PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>(response.Message);
            }
            catch (Exception ex)
            {
                return new PaginationResponse<PaginatedList<GetOrganizationPolicyConfigResultViewModel>>(ex.Message);
            }
        }
    }
}
