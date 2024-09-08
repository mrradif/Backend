

using Shared.DTO.Control_Panel.Administration.Company;

namespace Shared.DTO.Control_Panel.Administration.View
{
    public class CompanyAddRangeResult
    {
        public List<CreateCompanyResultDto> ExistingCompanies { get; set; }

        public IEnumerable<CreateCompanyResultDto> AddedCompanies { get; set; }
    }
}
