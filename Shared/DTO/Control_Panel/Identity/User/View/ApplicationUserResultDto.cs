
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Control_Panel.Identity.User.View
{
    public class ApplicationUserResultDto
    {
        public string Id { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        public bool IsBackupPassword { get; set; }

        public string EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public long? OrganizationId { get; set; }
        public long? CompanyId { get; set; }
        public long? DivisionId { get; set; }
        public long? BranchId { get; set; }

        public bool IsActive { get; set; }

    }
}
