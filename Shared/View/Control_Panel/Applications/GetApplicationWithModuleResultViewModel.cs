using Shared.View.Control_Panel.Modules;

namespace Shared.View.Control_Panel.Applications
{
    public class GetApplicationWithModuleResultViewModel : GetApplicationResultViewModel
    {
        public ICollection<GetModuleResultViewModel> Modules { get; set; }
    }
}
