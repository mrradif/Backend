
namespace Shared.DTO.Control_Panel.Identity.Role
{
    public class UpdateRoleDto
    {
        public string RoleId { get; set; }
        public string NewRoleName { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool? IsSysadmin { get; set; }
        public bool? IsGroupAdmin { get; set; }
        public bool? IsCompanyAdmin { get; set; }
        public bool? IsBranchAdmin { get; set; }
        public long? OrganizationId { get; set; }
        public long? CompanyId { get; set; }
    }
}
