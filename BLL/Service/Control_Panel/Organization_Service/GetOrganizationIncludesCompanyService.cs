
using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Organization.OrganizationWithCompany;
using Shared.DTO.Control_Panel.Administration.Search;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Organization_Service
{
    public class GetOrganizationIncludesCompanyService
    {
        private readonly IGetGenericRepo<Organization, ControlPanelDbContext, GetOrganizationWithCompanyResultDto, long> _getOrganizationRepository;
        private readonly IMapper _mapper;

        public GetOrganizationIncludesCompanyService(IGetGenericRepo<Organization, ControlPanelDbContext, GetOrganizationWithCompanyResultDto, long> getOrganizationRepository, IMapper mapper)
        {
            _getOrganizationRepository = getOrganizationRepository ?? throw new ArgumentNullException(nameof(getOrganizationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public async Task<Response<IEnumerable<GetOrganizationWithCompanyResultDto>>> GetAllOrganizationsWithCompaniesAsync()
        {
            var response = new Response<IEnumerable<GetOrganizationWithCompanyResultDto>>();

            try
            {
                // Retrieve organizations with related companies
                var organizationsResponse = await _getOrganizationRepository.GetAllAsync(org => org.Companies);

                if (organizationsResponse.Success)
                {
                    // Map to OrganizationWithCompanyDtoResult
                    var organizationDtos = _mapper.Map<IEnumerable<GetOrganizationWithCompanyResultDto>>(organizationsResponse.Data);

                    response.Success = true;
                    response.Data = organizationDtos;
                    response.Message = $"{organizationDtos.Count()} organizations retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = organizationsResponse.Message;
                }
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = $"Error retrieving organizations with companies: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<IEnumerable<GetOrganizationWithCompanyResultDto>>> GetAllOrganizationsWithCompaniesAndPredicateAsync(OrganizationSearchCriteria criteria)
        {
            var response = new Response<IEnumerable<GetOrganizationWithCompanyResultDto>>();

            try
            {
                // Build predicate based on search criteria
                Expression<Func<Organization, bool>> predicate = org =>
                    (string.IsNullOrEmpty(criteria.OrganizationName) || org.OrganizationName.Contains(criteria.OrganizationName) || org.OrgCode.Contains(criteria.OrganizationName)) &&
                    (!criteria.IsActive || org.IsActive);


                // Retrieve organizations with related companies
                var organizationsResponse = await _getOrganizationRepository.GetAllAsync(predicate, org => org.Companies);

                if (organizationsResponse.Success)
                {
                    // Map to OrganizationWithCompanyDtoResult
                    var organizationDtos = _mapper.Map<IEnumerable<GetOrganizationWithCompanyResultDto>>(organizationsResponse.Data);

                    response.Success = true;
                    response.Data = organizationDtos;
                    response.Message = $"{organizationDtos.Count()} organizations retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = organizationsResponse.Message;
                }
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = $"Error retrieving organizations with companies: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<IEnumerable<GetOrganizationWithCompanyResultDto>>> GetAllOrganizationsWithCompaniesAndPredicateAndOrderByAsync(OrganizationSearchCriteria criteria)
        {
            var response = new Response<IEnumerable<GetOrganizationWithCompanyResultDto>>();

            try
            {
                // Build predicate based on search criteria
                Expression<Func<Organization, bool>> predicate = org =>
                    (string.IsNullOrEmpty(criteria.OrganizationName) || org.OrganizationName.Contains(criteria.OrganizationName) || org.OrgCode.Contains(criteria.OrganizationName)) &&
                    (!criteria.IsActive || org.IsActive);

                // Define ordering logic if necessary
                Func<IQueryable<Organization>, IOrderedQueryable<Organization>> orderBy = null;

                if (!string.IsNullOrEmpty(criteria.OrganizationName))
                {
                    switch (criteria.OrganizationName.ToLower())
                    {
                        case "organizationname":
                            orderBy = q => q.OrderBy(org => org.OrganizationName);
                            break;
                        // Add more cases for other fields if needed
                        default:
                            break;
                    }
                }

                // Retrieve organizations with related companies
                var organizationsResponse = await _getOrganizationRepository.GetAllAsync(predicate, orderBy, org => org.Companies);

                if (organizationsResponse.Success)
                {
                    // Map to OrganizationWithCompanyDtoResult
                    var organizationDtos = _mapper.Map<IEnumerable<GetOrganizationWithCompanyResultDto>>(organizationsResponse.Data);

                    response.Success = true;
                    response.Data = organizationDtos;
                    response.Message = $"{organizationDtos.Count()} organizations retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = organizationsResponse.Message;
                }
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = $"Error retrieving organizations with companies: {ex.Message}";
            }

            return response;
        }




        // Get Single 
        public async Task<Response<GetOrganizationWithCompanyResultDto>> GetSingleOrganizationsWithCompaniesAndPredicateAsync(OrganizationSearchCriteria criteria)
        {
            var response = new Response<GetOrganizationWithCompanyResultDto>();

            try
            {
                // Build predicate based on search criteria
                //Expression<Func<Organization, bool>> predicate = org =>
                //    (string.IsNullOrEmpty(criteria.OrganizationName) || org.OrganizationName.Contains(criteria.OrganizationName) || org.OrgCode.Contains(criteria.OrganizationName)) &&
                //    (!criteria.IsActive || org.IsActive);

                Expression<Func<Organization, bool>> predicate = org => criteria.OrganizationId == org.OrganizationId;

                // Retrieve organizations with related companies
                var organizationsResponse = await _getOrganizationRepository.GetSingleAsync(predicate, org => org.Companies);

                if (organizationsResponse.Success)
                {
                    // Map to OrganizationWithCompanyDtoResult
                    var organizationDtos = _mapper.Map<GetOrganizationWithCompanyResultDto>(organizationsResponse.Data);

                    response.Success = true;
                    response.Data = organizationDtos;
                    response.Message = "Organizations retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = organizationsResponse.Message;
                }
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = $"Error retrieving organizations with companies: {ex.Message}";
            }

            return response;
        }


    }
}
