using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Organization;
using Shared.DTO.Control_Panel.Administration.Search;
using System.Linq.Expressions;


namespace BLL.Service.Control_Panel.Organization_Service
{
    public class GetOrganizationService
    {
        private readonly IGetGenericRepo<Organization, ControlPanelDbContext, GetOrganizationResultDto, long> _getOrganizationRepository;
        private readonly IMapper _mapper;

        public GetOrganizationService(IGetGenericRepo<Organization, ControlPanelDbContext, GetOrganizationResultDto, long> getOrganizationRepository, IMapper mapper)
        {
            _getOrganizationRepository = getOrganizationRepository ?? throw new ArgumentNullException(nameof(getOrganizationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<Response<IEnumerable<GetOrganizationResultDto>>> GetAllOrganizationsAsync()
        {
            var response = await _getOrganizationRepository.GetAllAsync();
            return response;
        }


        public async Task<Response<IEnumerable<GetOrganizationResultDto>>> GetOrganizationsAsync()
        {
            var response = await _getOrganizationRepository.GetAllAsync(e => e.IsDeleted == false);
            return response;
        }




        public async Task<Response<IEnumerable<GetOrganizationResultDto>>> GetOrganizationsByCriteriaAsync(OrganizationSearchCriteria criteria)
        {
            Expression<Func<Organization, bool>> predicate = org =>
                (string.IsNullOrEmpty(criteria.OrganizationName) || org.OrganizationName.Contains(criteria.OrganizationName) || org.OrgCode.Contains(criteria.OrganizationName)) &&
                (!criteria.IsActive || org.IsActive);

            var response = await _getOrganizationRepository.GetAllAsync(predicate);
            return response;
        }



        public async Task<Response<IEnumerable<GetOrganizationResultDto>>> GetOrganizationsByCriteriaAsync(Expression<Func<Organization, bool>> predicate)
        {
            return await _getOrganizationRepository.GetAllAsync(predicate);

        }



        public async Task<Response<IEnumerable<GetOrganizationResultDto>>> GetOrganizationsWithOrderingAsync(OrganizationSearchCriteria criteria)
        {
            Expression<Func<Organization, bool>> predicate = org =>
                (string.IsNullOrEmpty(criteria.OrganizationName) || org.OrganizationName.Contains(criteria.OrganizationName) || org.OrgCode.Contains(criteria.OrganizationName)) &&
                (!criteria.IsActive || org.IsActive);

            Func<IQueryable<Organization>, IOrderedQueryable<Organization>> orderBy = null;

            orderBy = q => q.OrderByDescending(org => org.OrganizationId);


            var response = await _getOrganizationRepository.GetAllAsync(predicate, orderBy);
            return response;
        }






        // Get Single


        public async Task<Response<GetOrganizationResultDto>> GetSingleOrganizationById(long organizationId)
        {
            return await _getOrganizationRepository.GetSingleAsync(organizationId);
        }


        public async Task<Response<GetOrganizationResultDto>> GetSingleOrganizationsByCriteriaAsync(OrganizationSearchCriteria criteria)
        {
            Expression<Func<Organization, bool>> predicate = org =>
                (string.IsNullOrEmpty(criteria.OrganizationName) || org.OrganizationName.Contains(criteria.OrganizationName) || org.OrgCode.Contains(criteria.OrganizationName)) &&
                (!criteria.IsActive || org.IsActive);

            var response = await _getOrganizationRepository.GetSingleAsync(predicate);
            return response;
        }


        public async Task<Response<GetOrganizationResultDto>> GetSingleOrganizationsByCriteriaAsync(Expression<Func<Organization, bool>> predicate)
        {
            return await _getOrganizationRepository.GetSingleAsync(predicate);
        }



    }
}
