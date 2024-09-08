

using Shared.Domain.Common.Class;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Control_Panel.Administration.Organization_Policy_Config
{
    public class CreateOrganizationPolicyConfigRequestDto:AuditableEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
        public string OrganizationPolicyConfigName { get; set; }


        public long OrganizationId { get; set; }



        [StringLength(255, ErrorMessage = "Description can't be longer than 255 characters")]
        public string Description { get; set; }



        public bool RequiresTwoFactorAuthentication { get; set; }


        public bool HasLockoutPolicy { get; set; }


        public bool IsActive { get; set; }


        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }


    }
}
