using Shared.View.Main_Menu;

namespace Shared.View.Control_Panel.Modules
{
    public class GetModuleWithMainMenuResultViewModel : GetModuleResultViewModel
    {
        public ICollection<GetMainMenuResultViewModel> MainMenus { get; set; }
    }
}
