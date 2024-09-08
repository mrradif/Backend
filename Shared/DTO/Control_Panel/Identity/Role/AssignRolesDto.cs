

namespace Shared.DTO.Control_Panel.Identity.Role
{
    public class AssignRolesDto
    {
        public string UserId { get; set; }
        public IEnumerable<string> RoleIds { get; set; }
    }
}
