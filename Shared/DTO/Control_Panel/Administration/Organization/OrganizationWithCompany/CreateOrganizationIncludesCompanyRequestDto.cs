using Shared.DTO.Control_Panel.Administration.Company;

namespace Shared.DTO.Control_Panel.Administration.Organization.OrganizationWithCompany
{
    public class CreateOrganizationIncludesCompanyRequestDto
    {
        public CreateOrganizationRequestDto Organization { get; set; }

        public ICollection<CreateCompanyRequestDto> Companies { get; set; } = new List<CreateCompanyRequestDto>();
    }
}
