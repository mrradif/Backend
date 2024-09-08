
namespace Shared.DTO.Modules
{
    public class UpdateModuleRequestDto:CreateModuleRequestDto
    {
        public long ModuleId { get; set; }

        public new string StateStatus { get; set; }
    }
}
