
using Shared.View.Control_Panel.Buttons;
using Shared.View.Control_Panel.Components;
using Shared.View.Control_Panel.Modules;
using Shared.View.Main_Menu;

namespace Shared.View.Control_Panel.Applications
{
    public class GetApplicationWithIncludesResultViewModel: GetApplicationResultViewModel
    {
        public ICollection<GetModuleResultViewModel> Modules { get; set; }
        public ICollection<GetComponentResultViewModel> Components { get; set; }
        public ICollection<GetMainMenuResultViewModel> MainMenus { get; set; }
        public ICollection<GetButtonResultViewModel> Buttons { get; set; }
    }
}
