
using Shared.View.Control_Panel.Applications;

namespace Shared.View.Control_Panel.Modules
{
    public class GetModuleWithApplicationResultViewModel: GetModuleResultViewModel
    {
        public GetApplicationResultViewModel Application { get; set; }

        public long ApplicationId { get; set; }
        public string ApplicationName { get; set; }
    }
}
