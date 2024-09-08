
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.Common.Class;
using AutoMapper;


namespace Shared.Domain.Control_Panel.Administration.Org_Config
{
    public class Division : AuditableEntity, IStateStatus
    {
        [Key]
        public long DivisionId { get; set; }

        [Required, StringLength(100)]
        public string DivisionName { get; set; }

        [Required, StringLength(5)]
        public string ShortName { get; set; }

        [StringLength(30)]
        public string DIVCode { get; set; }



        [ForeignKey("CompanyId")]
        public long? CompanyId { get; set; }
        public Company Company { get; set; }



        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }


        public virtual ICollection<Branch> Branches { get; set; }

    }
}
