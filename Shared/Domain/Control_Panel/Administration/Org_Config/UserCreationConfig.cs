using Shared.Domain.Common.Class;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Control_Panel.Administration.Org_Config
{
    public class UserCreationConfig : AuditableEntity, IStateStatus
    {
        [Key]
        public long UserCreationConfigId { get; set; }


        public long OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }



        [ForeignKey("CompanyId")]
        public long CompanyId { get; set; }
        public Company Company { get; set; }


        public long? DivisionId { get; set; }

        [ForeignKey(nameof(DivisionId))]
        public Division Division { get; set; }


        public long? BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; }


        public bool RequiredTwoFactor { get; set; }
        public bool LockoutPolicyEnabled { get; set; }



        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }

    }
}
