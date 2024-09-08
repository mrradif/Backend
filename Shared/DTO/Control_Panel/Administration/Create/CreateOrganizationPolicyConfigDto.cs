
using System.ComponentModel.DataAnnotations;


namespace Shared.DTO.Control_Panel.Administration.Create
{
    public class CreateOrganizationPolicyConfigDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
        public string OrganizationConfigName { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(50, ErrorMessage = "Code can't be longer than 50 characters")]
        public string Code { get; set; }

        [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters")]
        public string Description { get; set; }

        public bool RequiresTwoFactorAuthentication { get; set; }
        public bool HasLockoutPolicy { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
