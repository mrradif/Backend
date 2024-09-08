using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;


namespace Shared.DTO.Control_Panel.Administration.Update.Policy_Config
{
    public class UpdateOrganizationWithCompaniesDto
    {
        public CreateOrganizationPolicyConfigRequestDto OrganizationDto { get; set; }
        public List<CreateCompanyPolicyConfigRequestDto> CompanyDtos { get; set; }
    }
}
