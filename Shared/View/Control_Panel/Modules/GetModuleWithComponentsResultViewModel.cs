

using Shared.View.Control_Panel.Components;

namespace Shared.View.Control_Panel.Modules
{
    public class GetModuleWithComponentsResultViewModel: GetModuleResultViewModel
    {
        public ICollection<GetComponentResultViewModel> Components { get; set; }

    }
}
