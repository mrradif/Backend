


namespace Shared.DTO.Control_Panel.Identity.User
{
    public class UserWithRolesAndClaimsDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<UserClaimDto> Claims { get; set; }
        public List<string> Roles { get; set; }
    }
}
