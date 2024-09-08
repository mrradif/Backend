

namespace Shared.View.Employee.Employee_Information
{
    public class GetEmployeeInformationResultViewModel
    {
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Salutation { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string OfficeEmail { get; set; }
        public string OfficeMobile { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfConfirmation { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string StateStatus { get; set; }
    }
}
