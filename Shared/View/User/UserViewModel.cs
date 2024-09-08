namespace Shared.View.User
{
    public class UserViewModel
    {
        public long UserLoginInformationId { get; set; }
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string EmployeeId { get; set; }
        public string EmployeeCode { get; set; }

        public long? OrganizationId { get; set; }
        public long? CompanyId { get; set; }
        public long? DivisionId { get; set; }
        public long? BranchId { get; set; }

        public List<string> Roles { get; set; }
        public List<string> UserClaims { get; set; }
        public List<string> RoleClaims { get; set; }
    }
}
