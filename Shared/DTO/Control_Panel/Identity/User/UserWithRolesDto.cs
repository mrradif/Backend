

namespace Shared.DTO.Control_Panel.Identity.User
{
    public class UserWithRolesDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; } 
        public ICollection<string> Roles { get; set; }
    }
}
