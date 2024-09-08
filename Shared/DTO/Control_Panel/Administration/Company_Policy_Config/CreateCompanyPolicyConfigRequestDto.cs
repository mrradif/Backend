
using Shared.Domain.Common.Class;
using System.ComponentModel.DataAnnotations;


namespace Shared.DTO.Control_Panel.Administration.Company_Policy_Config
{
    public class CreateCompanyPolicyConfigRequestDto: AuditableEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
        public string CompanyPolicyConfigName { get; set; }



        [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters")]
        public string Description { get; set; }

        public bool RequiresTwoFactorAuthentication { get; set; }
        public bool HasLockoutPolicy { get; set; }

        public bool IsActive { get; set; }


        public long OrganizationId { get; set; }
        public long CompanyId { get; set; }


        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }

        public long? OrganizationPolicyConfigId { get; set; }
    }
}
