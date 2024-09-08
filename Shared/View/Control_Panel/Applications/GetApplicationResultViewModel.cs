using Shared.Domain.Control_Panel.Administration.App_Config;

namespace Shared.View.Control_Panel.Applications
{
    public class GetApplicationResultViewModel
    {
        public long ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationType { get; set; }
        public string StateStatus { get; set; }
    }
}
