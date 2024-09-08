

namespace Shared.DTO.Control_Panel.Identity.Role
{
    public class RoleClaimsDto
    {
        public string RoleId { get; set; }
        public IEnumerable<RoleClaimDto> RoleClaims { get; set; }
    }
}
