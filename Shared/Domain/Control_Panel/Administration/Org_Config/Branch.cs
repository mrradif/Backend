using Shared.Domain.Common.Class;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shared.Domain.Control_Panel.Administration.Org_Config
{
    public class Branch : AuditableEntity
    {
        [Key]
        public long BranchId { get; set; }
        [StringLength(100)]
        public string BranchUniqueId { get; set; }
        [Required, StringLength(100)]
        public string BranchName { get; set; }
        [Required, StringLength(5)]
        public string ShortName { get; set; }
        [StringLength(50)]
        public string BranchCode { get; set; }
        [StringLength(20), DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }
        [StringLength(200), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(20), DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }
  


        public long? DivisionId { get; set; }

        [ForeignKey(nameof(DivisionId))]
        public Division Division { get; set; }



        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }


        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }
}
