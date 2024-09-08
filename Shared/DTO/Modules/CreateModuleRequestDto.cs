
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Modules
{
    public class CreateModuleRequestDto
    {
        public string ModuleName { get; set; }

        public long? ApplicationId { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; } = "Pending";
    }
}
