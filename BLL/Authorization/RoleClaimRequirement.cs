using Microsoft.AspNetCore.Authorization;


namespace BLL.Authorization
{
    public class RoleClaimRequirement : IAuthorizationRequirement
    {
        public string Role { get; }
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public RoleClaimRequirement(string role, string claimType, string claimValue)
        {
            Role = role;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }

}
