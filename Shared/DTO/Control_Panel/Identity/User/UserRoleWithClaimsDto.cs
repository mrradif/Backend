
namespace Shared.DTO.Control_Panel.Identity.User
{
    public class UserRoleWithClaimsDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public List<UserRoleClaimDto> Claims { get; set; }
    }
}
