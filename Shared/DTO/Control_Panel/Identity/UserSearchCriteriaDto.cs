

namespace Shared.DTO.Control_Panel.Identity
{
    public class UserSearchCriteriaDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public long? OrganizationId { get; set; }
        public long? CompanyId { get; set; }
        public long? DivisionId { get; set; }
        public long? BranchId { get; set; }
    }

}
