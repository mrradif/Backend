using Microsoft.AspNetCore.Authorization;


namespace BLL.Authorization
{
    public class RoleClaimHandler : AuthorizationHandler<RoleClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleClaimRequirement requirement)
        {
            if (context.User.IsInRole(requirement.Role))
            {
                var hasClaim = context.User.Claims.Any(c => c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue);
                if (hasClaim)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

}
