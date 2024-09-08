using Microsoft.AspNetCore.Identity;
using Shared.Domain.Common.Interface;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    [StringLength(150)]
    public string FullName { get; set; }

    public bool IsActive { get; set; }

    public bool IsDefaultPassword { get; set; }

    [StringLength(200)]
    public string DefaultPassword { get; set; }

    public bool IsBackupPassword { get; set; }

    public string BackupPasswordHash { get; set; }

    public string EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public long? OrganizationId { get; set; }
    public long? CompanyId { get; set; }
    public long? DivisionId { get; set; }
    public long? BranchId { get; set; }

    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }



    public virtual ICollection<Button> Buttons { get; set; }

}
