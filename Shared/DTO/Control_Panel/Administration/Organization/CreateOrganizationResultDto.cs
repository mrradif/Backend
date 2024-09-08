using Shared.DTO.Control_Panel.Administration.Company;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Control_Panel.Administration.Organization
{
    public class CreateOrganizationResultDto
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
        public string OrgImageFormat { get; set; }
        public string ReportImageFormat { get; set; }

        public string OrgPicPath { get; set; }
        [StringLength(100, ErrorMessage = "OrgPicFileName can't be longer than 100 characters")]
        public string OrgPicFileName { get; set; }
        [StringLength(50, ErrorMessage = "OrgPicFileExtension can't be longer than 50 characters")]
        public string OrgPicFileExtension { get; set; }
        [StringLength(50, ErrorMessage = "OrgPicContentType can't be longer than 50 characters")]
        public string OrgPicContentType { get; set; }

        public string ReportPicPath { get; set; }
        [StringLength(100, ErrorMessage = "ReportPicFileName can't be longer than 100 characters")]
        public string ReportPicFileName { get; set; }
        [StringLength(50, ErrorMessage = "ReportPicFileExtension can't be longer than 50 characters")]
        public string ReportPicFileExtension { get; set; }
        [StringLength(50, ErrorMessage = "ReportPicContentType can't be longer than 50 characters")]
        public string ReportPicContentType { get; set; }

        public string StateStatus { get; set; }

        // public List<CreateCompanyResultDto> Companies { get; set; }
    }
}
