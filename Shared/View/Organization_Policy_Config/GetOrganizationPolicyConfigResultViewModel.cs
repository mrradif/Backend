

namespace Shared.View.Organization_Policy_Config
{
    public class GetOrganizationPolicyConfigResultViewModel
    {
        public long OrganizationPolicyConfigId { get; set; }
        public string OrganizationPolicyConfigName { get; set; }
        public string Description { get; set; }
        public bool RequiresTwoFactorAuthentication { get; set; }
        public bool HasLockoutPolicy { get; set; }
        public bool IsActive { get; set; }
        public string StateStatus { get; set; }
    }
}
