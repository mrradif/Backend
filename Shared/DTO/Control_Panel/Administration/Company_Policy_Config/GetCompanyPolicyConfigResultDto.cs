namespace Shared.DTO.Control_Panel.Administration.Company_Policy_Config
{
    public class GetCompanyPolicyConfigResultDto
    {
        public long CompanyPolicyConfigId { get; set; }

        public string CompanyPolicyConfigName { get; set; }


        public string Description { get; set; }

        public bool RequiresTwoFactorAuthentication { get; set; }

        public bool HasLockoutPolicy { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string StateStatus { get; set; }

        public long? OrganizationPolicyConfigId { get; set; }
    }
}
