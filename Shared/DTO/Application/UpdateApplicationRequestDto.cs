

namespace Shared.DTO.Application
{
    public class UpdateApplicationRequestDto: CreateApplicationRequestDto
    {
        public long ApplicationId { get; set; }

        // Override StateStatus to make it a required field with no default value
        public new string StateStatus { get; set; }  
    }
}
