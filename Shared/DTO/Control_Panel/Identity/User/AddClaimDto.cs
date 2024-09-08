using Shared.DTO.Control_Panel.Identity.Role;
using System.ComponentModel.DataAnnotations;


namespace Shared.DTO.Control_Panel.Identity.User
{
    public class AddClaimDto
    {
        [Required]
        public string UserId { get; set; } // Assuming UserId is a string in your application

        [Required]
        public List<ClaimDto> Claims { get; set; }
    }
}
