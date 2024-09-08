

namespace Shared.DTO.Control_Panel.Identity.Token
{
    public class TokenUserDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public List<UserClaimDto> RolesClaims { get; set; }
        public List<UserClaimDto> UserClaims { get; set; }
    }

    public class UserClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
