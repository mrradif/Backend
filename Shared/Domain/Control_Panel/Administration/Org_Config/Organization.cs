using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Common.Class;


namespace Shared.Domain.Control_Panel.Administration.Org_Config
{
    [Table("tblOrganizations")]
    public class Organization : AuditableEntity, IStateStatus
    {
        [Key]
        public long OrganizationId { get; set; }


        [Required(ErrorMessage = "OrgUniqueId is required")]
        [StringLength(100, ErrorMessage = "OrgUniqueId can't be longer than 100 characters")]
        public string OrgUniqueId { get; set; }



        [Required(ErrorMessage = "OrgCode is required")]
        [StringLength(20, ErrorMessage = "OrgCode can't be longer than 20 characters")]
        public string OrgCode { get; set; }



        [Required(ErrorMessage = "OrganizationName is required")]
        [StringLength(150, ErrorMessage = "OrganizationName can't be longer than 150 characters")]
        public string OrganizationName { get; set; }



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


        public byte[] OrgPic { get; set; }

        [StringLength(250, ErrorMessage = "OrgPicPath can't be longer than 250 characters")]
        public string OrgPicPath { get; set; }

        [StringLength(100, ErrorMessage = "OrgPicFileName can't be longer than 100 characters")]
        public string OrgPicFileName { get; set; }

        [StringLength(50, ErrorMessage = "OrgPicFileExtension can't be longer than 50 characters")]
        public string OrgPicFileExtension { get; set; }

        [StringLength(50, ErrorMessage = "OrgPicContentType can't be longer than 50 characters")]
        public string OrgPicContentType { get; set; }



        [StringLength(250, ErrorMessage = "OrgLogoPath can't be longer than 250 characters")]
        public string OrgLogoPath { get; set; }



        public byte[] ReportPic { get; set; }

        [StringLength(250, ErrorMessage = "ReportPicPath can't be longer than 250 characters")]
        public string ReportPicPath { get; set; }

        [StringLength(100, ErrorMessage = "ReportPicFileName can't be longer than 100 characters")]
        public string ReportPicFileName { get; set; }

        [StringLength(50, ErrorMessage = "ReportPicFileExtension can't be longer than 50 characters")]
        public string ReportPicFileExtension { get; set; }

        [StringLength(50, ErrorMessage = "ReportPicContentType can't be longer than 50 characters")]
        public string ReportPicContentType { get; set; }


        [StringLength(250, ErrorMessage = "ReportLogoPath can't be longer than 250 characters")]
        public string ReportLogoPath { get; set; }



        public long? AppId { get; set; }

        [StringLength(100, ErrorMessage = "AppName can't be longer than 100 characters")]
        public string AppName { get; set; }

        [StringLength(50, ErrorMessage = "StorageName can't be longer than 50 characters")]
        public string StorageName { get; set; }




        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }



        public virtual ICollection<Company> Companies { get; set; }
    }
}
