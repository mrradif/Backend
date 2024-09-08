
using Shared.View.Control_Panel.Applications;
using Shared.View.Control_Panel.Modules;

namespace Shared.View.Control_Panel.Components
{
    public class GetComponentWithApplicationAndModuleResultViewModel: GetComponentResultViewModel
    {
        public GetApplicationResultViewModel Application { get; set; }    
        public string ApplicationName { get; set; }

        public GetModuleResultViewModel Module { get; set; }
        public string ModuleName { get; set; }
    }
}
