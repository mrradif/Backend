

namespace Shared.DTO.Control_Panel.Identity.Role
{
    public class RoleWithClaimsDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ClaimDto> Claims { get; set; }
    }
}
