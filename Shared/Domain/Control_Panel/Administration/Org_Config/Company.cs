
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Common.Class;

namespace Shared.Domain.Control_Panel.Administration.Org_Config
{
    [Table("tblCompanies")]
    public class Company : AuditableEntity, IStateStatus
    {
        [Key]
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "ComUniqueId is required")]
        [StringLength(100, ErrorMessage = "ComUniqueId can't be longer than 100 characters")]
        public string ComUniqueId { get; set; }

        [Required(ErrorMessage = "CompanyName is required")]
        [StringLength(100, ErrorMessage = "CompanyName can't be longer than 100 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "CompanyCode is required")]
        [StringLength(50, ErrorMessage = "CompanyCode can't be longer than 50 characters")]
        public string CompanyCode { get; set; }

        [StringLength(50, ErrorMessage = "ShortName can't be longer than 50 characters")]
        public string ShortName { get; set; }

        [StringLength(100, ErrorMessage = "SiteThumbnailPath can't be longer than 100 characters")]
        public string SiteThumbnailPath { get; set; }

        [StringLength(150, ErrorMessage = "Address can't be longer than 150 characters")]
        public string Address { get; set; }

        [StringLength(150, ErrorMessage = "Email can't be longer than 150 characters")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "PhoneNumber can't be longer than 50 characters")]
        public string PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "MobileNumber can't be longer than 50 characters")]
        public string MobileNumber { get; set; }

        [StringLength(50, ErrorMessage = "Fax can't be longer than 50 characters")]
        public string Fax { get; set; }

        [StringLength(100, ErrorMessage = "Website can't be longer than 100 characters")]
        public string Website { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractExpireDate { get; set; }

        [StringLength(200, ErrorMessage = "Remarks can't be longer than 200 characters")]
        public string Remarks { get; set; }

        public byte[] CompanyPic { get; set; }

        [StringLength(250, ErrorMessage = "CompanyLogoPath can't be longer than 250 characters")]
        public string CompanyLogoPath { get; set; }

        [StringLength(50, ErrorMessage = "CompanyImageFormat can't be longer than 50 characters")]
        public string CompanyImageFormat { get; set; }

        public byte[] ReportPic { get; set; }

        [StringLength(250, ErrorMessage = "ReportLogoPath can't be longer than 250 characters")]
        public string ReportLogoPath { get; set; }

        [StringLength(50, ErrorMessage = "ReportImageFormat can't be longer than 50 characters")]
        public string ReportImageFormat { get; set; }

        [StringLength(300, ErrorMessage = "AttendanceDeviceLocation can't be longer than 300 characters")]
        public string AttendanceDeviceLocation { get; set; }



        public long? OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }



        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }



        public virtual ICollection<Division> Divisions { get; set; }
    }
}
