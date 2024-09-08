using Shared.Domain.Common.Class;
using Shared.Domain.Common.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Control_Panel.Administration.Org_Config
{
    public class CompanyPolicyConfig : AuditableEntity, IStateStatus
    {
        [Key]
        public long CompanyPolicyConfigId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
        public string CompanyPolicyConfigName { get; set; }


        [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters")]
        public string Description { get; set; }



        public bool RequiresTwoFactorAuthentication { get; set; }
        public bool HasLockoutPolicy { get; set; }


        public long? OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }


        public long? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }


        public long? OrganizationPolicyConfigId { get; set; }

        [ForeignKey(nameof(OrganizationPolicyConfigId))]
        public OrganizationPolicyConfig OrganizationPolicyConfig { get; set; }



        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }


    }
}
