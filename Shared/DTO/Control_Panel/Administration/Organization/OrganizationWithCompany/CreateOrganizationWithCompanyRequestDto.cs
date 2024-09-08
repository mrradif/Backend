using Shared.DTO.Control_Panel.Administration.Company;

namespace Shared.DTO.Control_Panel.Administration.Organization.OrganizationWithCompany
{
    public class CreateOrganizationWithCompanyRequestDto
    {
        public CreateOrganizationRequestDto Organization { get; set; }

        public IEnumerable<CreateCompanyRequestDto> Companies { get; set; } = new List<CreateCompanyRequestDto>();
    }
}
