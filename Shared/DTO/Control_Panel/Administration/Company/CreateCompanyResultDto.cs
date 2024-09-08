namespace Shared.DTO.Control_Panel.Administration.Company
{
    public class CreateCompanyResultDto
    {
        public long CompanyId { get; set; }
        public string ComUniqueId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
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
        public byte[] CompanyPic { get; set; }
        public string CompanyLogoPath { get; set; }
        public string CompanyImageFormat { get; set; }
        public byte[] ReportPic { get; set; }
        public string ReportLogoPath { get; set; }
        public string ReportImageFormat { get; set; }
        public string AttendanceDeviceLocation { get; set; }
        public long OrganizationId { get; set; }
    }
}
