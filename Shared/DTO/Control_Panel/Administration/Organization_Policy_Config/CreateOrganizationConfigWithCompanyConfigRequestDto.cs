using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;


namespace Shared.DTO.Control_Panel.Administration.Organization_Policy_Config
{
    public class CreateOrganizationConfigWithCompanyConfigRequestDto
    {
        public CreateOrganizationPolicyConfigRequestDto OrganizationDto { get; set; }
        public IEnumerable<CreateCompanyPolicyConfigRequestDto> CompanyDtos { get; set; }
    }
}
