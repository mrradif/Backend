using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Microsoft.Extensions.Logging;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.DTO.Control_Panel.Administration.Search;
using System.Linq.Expressions;


namespace BLL.Service.Control_Panel.Company_Service
{
    public class GetCompanyService
    {
        private readonly IGetGenericRepo<Company, ControlPanelDbContext, GetCompanyResultDto, long> _getCompanyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCompanyService> _logger;

        public GetCompanyService(IGetGenericRepo<Company, ControlPanelDbContext, GetCompanyResultDto, long> getCompanyRepository, IMapper mapper, ILogger<GetCompanyService> logger)
        {
            _getCompanyRepository = getCompanyRepository ?? throw new ArgumentNullException(nameof(getCompanyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<IEnumerable<GetCompanyResultDto>>> GetAllCompaniesAsync()
        {
            var response = await _getCompanyRepository.GetAllAsync();
            return response;
        }

        public async Task<Response<IEnumerable<GetCompanyResultDto>>> GetCompaniesByCriteriaAsync(CompanySearchCriteria criteria)
        {
            Expression<Func<Company, bool>> predicate = comp =>
                string.IsNullOrEmpty(criteria.CompanyName) || comp.CompanyName.Contains(criteria.CompanyName) || comp.CompanyCode.Contains(criteria.CompanyName);

            var response = await _getCompanyRepository.GetAllAsync(predicate);
            return response;
        }

        public async Task<Response<IEnumerable<GetCompanyResultDto>>> GetCompaniesWithOrderingAsync(CompanySearchCriteria criteria)
        {
            Expression<Func<Company, bool>> predicate = comp =>
                string.IsNullOrEmpty(criteria.CompanyName) || comp.CompanyName.Contains(criteria.CompanyName) || comp.CompanyCode.Contains(criteria.CompanyName);

            Func<IQueryable<Company>, IOrderedQueryable<Company>> orderBy = null;

            if (!string.IsNullOrEmpty(criteria.CompanyName))
            {
                switch (criteria.CompanyName.ToLower())
                {
                    case "companyname":
                        orderBy = q => q.OrderBy(comp => comp.CompanyName);
                        break;
                    // Add more cases for other fields if needed
                    default:
                        break;
                }
            }

            var response = await _getCompanyRepository.GetAllAsync(predicate, orderBy);
            return response;
        }


    }
}
