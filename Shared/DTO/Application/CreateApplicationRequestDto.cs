using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Application
{
    public class CreateApplicationRequestDto
    {
        [Required(ErrorMessage = "Application Name is required.")]
        [StringLength(100, ErrorMessage = "Application Name cannot exceed 100 characters.")]
        public string ApplicationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Application Type is required.")]
        [StringLength(50, ErrorMessage = "Application Type cannot exceed 50 characters.")]
        public string ApplicationType { get; set; } = string.Empty;


        public virtual string StateStatus { get; set; } = "Pending";
    }
}
