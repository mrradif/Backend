using Microsoft.AspNetCore.Identity;
using Shared.Domain.Common.Interface;
using System.ComponentModel.DataAnnotations;


namespace Shared.Domain.Control_Panel.Identity
{
    public class ApplicationRole : IdentityRole<Guid>, IOrganizationAndCompanyEntity,IAuditableEntity
    {
        public bool IsActive { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public bool? IsSysadmin { get; set; }
        public bool? IsGroupAdmin { get; set; }
        public bool? IsCompanyAdmin { get; set; }
        public bool? IsBranchAdmin { get; set; }

        public long? OrganizationId { get; set; }
        public long? CompanyId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }   
        public DateTime? UpdatedDate { get; set; }

        public string DeletedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? DeletedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
