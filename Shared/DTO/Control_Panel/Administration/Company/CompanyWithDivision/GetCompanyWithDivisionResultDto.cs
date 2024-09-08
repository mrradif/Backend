using Shared.Domain.Control_Panel.Administration.Org_Config;

namespace Shared.DTO.Control_Panel.Administration.Company.CompanyWithDivision
{
    public class GetCompanyWithDivisionResultDto: CreateCompanyResultDto
    {
        public List<Division> Divisions { get; set; }
    }
}
