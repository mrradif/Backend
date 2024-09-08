using System;

namespace Shared.DTO.Control_Panel.Administration.Organization
{
    public class CreateOrganizationRequestDto
    {
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
        public bool IsActive { get; set; }


        // Additional properties to store file metadata
        public string OrgPicPath { get; set; }
        public string OrgPicFileName { get; set; }
        public string OrgPicFileExtension { get; set; }
        public string OrgPicContentType { get; set; }
        public string ReportPicPath { get; set; }
        public string ReportPicFileName { get; set; }
        public string ReportPicFileExtension { get; set; }
        public string ReportPicContentType { get; set; }
    }
}
