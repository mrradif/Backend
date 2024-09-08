namespace Shared.DTO.Control_Panel.Administration.Organization_Policy_Config
{
    public class GetOrganizationPolicyConfigResultDto
    {
        public long OrganizationPolicyConfigId { get; set; }
        public string OrganizationPolicyConfigName { get; set; }

        public string Description { get; set; }

        public bool RequiresTwoFactorAuthentication { get; set; }

        public bool HasLockoutPolicy { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string StateStatus { get; set; }



        // public ICollection<CompanyPolicyConfigDtoResult> Companies { get; set; }

        // public ICollection<CompanyPolicyConfigDtoResult> Companies { get; set; } = new List<CompanyPolicyConfigDtoResult>();
    }
}
