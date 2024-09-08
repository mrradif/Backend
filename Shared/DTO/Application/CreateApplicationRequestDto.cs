namespace Shared.DTO.Application
{
    public class CreateApplicationRequestDto
    {
        public string ApplicationName { get; set; } = string.Empty;
        public string ApplicationType { get; set; } = string.Empty;
        public virtual string StateStatus { get; set; } = "Pending";
    }
}
