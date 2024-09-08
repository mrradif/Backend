using Shared.DTO.Control_Panel.Administration.Company;

namespace Shared.DTO.Control_Panel.Administration.Organization.OrganizationWithCompany
{
    public class GetOrganizationWithCompanyResultDto
    {
        public long OrganizationId { get; set; }
        public string OrgUniqueId { get; set; }
        public string OrgCode { get; set; }
        public string OrganizationName { get; set; }
        public string ShortName { get; set; }
        public string SiteThumbnailPath { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractExpireDate { get; set; }
        public string Remarks { get; set; }
        public string OrgLogoPath { get; set; }
        public string ReportLogoPath { get; set; }

        public List<CreateCompanyResultDto> Companies { get; set; }
    }
}
