
using System.ComponentModel.DataAnnotations;


namespace Shared.View.Company_Policy_Config
{
    public class GetCompanyPolicyConfigResultViewModel
    {
        public long CompanyPolicyConfigId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
        public string CompanyPolicyConfigName { get; set; }

        [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters")]
        public string Description { get; set; }

        public bool RequiresTwoFactorAuthentication { get; set; }
        public bool HasLockoutPolicy { get; set; }

        public long? OrganizationId { get; set; }
        public string OrganizationName { get; set; }  // Assuming you want to display the organization name

        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }  // Assuming you want to display the company name

        public long? OrganizationPolicyConfigId { get; set; }
        public string OrganizationPolicyConfigName { get; set; }  // Assuming you want to display the organization policy config name

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }
    }
}
