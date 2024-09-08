using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company.CompanyWithDivision;
using Shared.DTO.Control_Panel.Administration.Search;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Company_Service
{
    public class GetCompanyIncludesDivisionService
    {
        private readonly IGetGenericRepo<Company, ControlPanelDbContext, GetCompanyWithDivisionResultDto, long> _getCompanyRepository;
        private readonly IMapper _mapper;

        public GetCompanyIncludesDivisionService(IGetGenericRepo<Company, ControlPanelDbContext, GetCompanyWithDivisionResultDto, long> getCompanyRepository, IMapper mapper)
        {
            _getCompanyRepository = getCompanyRepository ?? throw new ArgumentNullException(nameof(getCompanyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public async Task<Response<IEnumerable<GetCompanyWithDivisionResultDto>>> GetAllCompaniesWithDivisionsAsync()
        {
            var response = new Response<IEnumerable<GetCompanyWithDivisionResultDto>>();

            try
            {
                // Retrieve companies with related divisions
                var companiesResponse = await _getCompanyRepository.GetAllAsync(org => org.Divisions);

                if (companiesResponse.Success)
                {
                    // Map to OrganizationWithCompanyDtoResult
                    var companyDtos = _mapper.Map<IEnumerable<GetCompanyWithDivisionResultDto>>(companiesResponse.Data);

                    response.Success = true;
                    response.Data = companyDtos;
                    response.Message = $"{companyDtos.Count()} companiess retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = companiesResponse.Message;
                }
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = $"Error retrieving companies with divisions: {ex.Message}";
            }

            return response;
        }



        public async Task<Response<IEnumerable<GetCompanyWithDivisionResultDto>>> GetAllCompaniesAndDivisionsAndPredicateAsync(CompanySearchCriteria criteria)
        {
            var response = new Response<IEnumerable<GetCompanyWithDivisionResultDto>>();

            try
            {
                // Build predicate based on search criteria
                Expression<Func<Company, bool>> predicate = com =>
                    (string.IsNullOrEmpty(criteria.CompanyName) || com.CompanyName.Contains(criteria.CompanyName) || com.CompanyCode.Contains(criteria.CompanyName)) &&
                    (!criteria.IsActive || com.IsActive);


                // Retrieve companies with related divisions
                var companiesResponse = await _getCompanyRepository.GetAllAsync(predicate, org => org.Divisions);

                if (companiesResponse.Success)
                {
                    var companyDtos = _mapper.Map<IEnumerable<GetCompanyWithDivisionResultDto>>(companiesResponse.Data);

                    response.Success = true;
                    response.Data = companyDtos;
                    response.Message = $"{companyDtos.Count()} companies retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = companiesResponse.Message;
                }
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = $"Error retrieving companies with divisions: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<IEnumerable<GetCompanyWithDivisionResultDto>>> GetAllCompaniesAndDivisionsAndPredicateAndOrderByAsync(CompanySearchCriteria criteria)
        {
            var response = new Response<IEnumerable<GetCompanyWithDivisionResultDto>>();

            try
            {
                // Build predicate based on search criteria
                Expression<Func<Company, bool>> predicate = com =>
                    (string.IsNullOrEmpty(criteria.CompanyName) || com.CompanyName.Contains(criteria.CompanyName) || com.CompanyCode.Contains(criteria.CompanyName)) &&
                    (!criteria.IsActive || com.IsActive);


                Func<IQueryable<Company>, IOrderedQueryable<Company>> orderBy = null;

                orderBy = q => q.OrderBy(com => com.CompanyId);

                var companiesResponse = await _getCompanyRepository.GetAllAsync(predicate, orderBy, org => org.Divisions);

                if (companiesResponse.Success)
                {
                    var companyDtos = _mapper.Map<IEnumerable<GetCompanyWithDivisionResultDto>>(companiesResponse.Data);

                    response.Success = true;
                    response.Data = companyDtos;
                    response.Message = $"{companyDtos.Count()} companies retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = companiesResponse.Message;
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
        public async Task<Response<GetCompanyWithDivisionResultDto>> GetSingleCompaniesWithDivisionsAndPredicateAsync(CompanySearchCriteria criteria)
        {
            var response = new Response<GetCompanyWithDivisionResultDto>();

            try
            {
                // Build predicate based on search criteria
                Expression<Func<Company, bool>> predicate = com =>
                     (string.IsNullOrEmpty(criteria.CompanyName) || com.CompanyName.Contains(criteria.CompanyName) || com.CompanyCode.Contains(criteria.CompanyName)) &&
                     (!criteria.IsActive || com.IsActive);


                // Retrieve organizations with related companies
                var organizationsResponse = await _getCompanyRepository.GetSingleAsync(predicate, org => org.Divisions);

                if (organizationsResponse.Success)
                {
                    // Map to OrganizationWithCompanyDtoResult
                    var organizationDtos = _mapper.Map<GetCompanyWithDivisionResultDto>(organizationsResponse.Data);

                    response.Success = true;
                    response.Data = organizationDtos;
                    response.Message = "Companies retrieved successfully.";
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
                response.Message = $"Error retrieving comapnies with divisions: {ex.Message}";
            }

            return response;
        }
    }
}
