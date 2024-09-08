
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Control_Panel.Administration.Component
{
    public class CreateComponentRequestDto
    {
        [Required(ErrorMessage = "ComponentName is required.")]
        [StringLength(100, ErrorMessage = "ComponentName cannot be longer than 100 characters.")]
        public string ComponentName { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }

        public virtual string StateStatus { get; set; } = "Pending";
    }
}
