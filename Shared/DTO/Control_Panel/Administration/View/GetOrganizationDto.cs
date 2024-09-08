
namespace Shared.DTO.Control_Panel.Administration.View
{
    public class GetOrganizationDto
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
        public byte[] OrgPic { get; set; }
        public string OrgImageFormat { get; set; }
        public string OrgLogoPath { get; set; }
        public byte[] ReportPic { get; set; }
        public string ReportImageFormat { get; set; }
        public string ReportLogoPath { get; set; }
        public long? AppId { get; set; }
        public string AppName { get; set; }
        public string StorageName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
