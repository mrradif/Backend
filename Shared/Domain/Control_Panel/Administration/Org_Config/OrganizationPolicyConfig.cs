using Shared.Domain.Common.Class;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrganizationPolicyConfig : Cancel
{
    [Key]
    public long OrganizationPolicyConfigId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
    public string OrganizationPolicyConfigName { get; set; }



    [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters")]
    public string Description { get; set; }



    public bool RequireTwoFactorAuthentication { get; set; }
    public bool HasLockoutPolicy { get; set; }


    public long? OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public Organization Organization { get; set; }





    public virtual ICollection<CompanyPolicyConfig> CompanyPolicyConfigs { get; set; }
}
