using DAL.Context.Control_Panel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Control_Panel.Identity;
using Shared.View.User;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


public static class UserHelper
{
    private static IHttpContextAccessor _httpContextAccessor;
    private static Func<IServiceScope> _serviceScopeFactory;


    public static void Configure(IHttpContextAccessor httpContextAccessor, Func<IServiceScope> serviceScopeFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public static UserViewModel AppUser()
    {
        using (var scope = _serviceScopeFactory())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<ControlPanelDbContext>();

            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                return null;
            }

            var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return null;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();

            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                return null;
            }

            JwtSecurityToken parsedToken;
            try
            {
                parsedToken = tokenHandler.ReadJwtToken(token) as JwtSecurityToken;
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return null;
            }

            var username = parsedToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            var user = userManager.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            // Fetch user login information
            var userLoginInfo = dbContext.tblUserLoginInformations
                .OrderByDescending(u => u.UserLoginInformationId)
                .FirstOrDefault(u => u.UserId == user.Id && u.Token == token);

            if (userLoginInfo == null)
            {
                return null;
            }

            // Fetch user claims
            var userClaims = userManager.GetClaimsAsync(user).Result.Select(c => c.Type + ":" + c.Value).ToList();

            // Fetch roles
            var roles = userManager.GetRolesAsync(user).Result.ToList();

            // Fetch role claims
            var roleClaims = new List<string>();
            foreach (var role in roles)
            {
                var roleEntity = roleManager.Roles.FirstOrDefault(r => r.Name == role);
                if (roleEntity != null)
                {
                    var claims = roleManager.GetClaimsAsync(roleEntity).Result;
                    roleClaims.AddRange(claims.Select(c => c.Type + ":" + c.Value));
                }
            }

            return new UserViewModel
            {
                UserLoginInformationId = userLoginInfo.UserLoginInformationId,
                FullName = user.FullName,
                EmployeeId = user.EmployeeId,
                OrganizationId = user.OrganizationId,
                CompanyId = user.CompanyId,
                DivisionId = user.DivisionId,
                BranchId = user.BranchId,
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles,
                UserClaims = userClaims,
                RoleClaims = roleClaims
            };
        }
    }


    public static string StorageName()
    {
        try
        {
            var currentUser = AppUser();

            using (var scope = _serviceScopeFactory())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ControlPanelDbContext>();

                var organization = dbContext.tblOrganizations
                    .FirstOrDefault(o => o.OrganizationId == currentUser.OrganizationId);

                if (organization == null)
                {
                    throw new InvalidOperationException("Organization not found for current user.");
                }

                return organization.StorageName;
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            throw new InvalidOperationException("Error retrieving storage name.", ex);
        }
    }
}
