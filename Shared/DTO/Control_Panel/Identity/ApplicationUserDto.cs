
namespace Shared.DTO.Control_Panel.Identity
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDefaultPassword { get; set; }
        public string DefaultPassword { get; set; }
        public bool IsBackupPassword { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public long? OrganizationId { get; set; }
        public long? CompanyId { get; set; }
        public long? DivisionId { get; set; }
        public long? BranchId { get; set; }
    }

}
