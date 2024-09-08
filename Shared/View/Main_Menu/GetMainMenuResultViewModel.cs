
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.View.Main_Menu
{
    public class GetMainMenuResultViewModel
    {
        public long MainMenuId { get; set; }

        public string MainMenuName { get; set; }

        public string MainMenuShortName { get; set; }

        public string IconClass { get; set; }

        public string IconColor { get; set; }

        public int? SequenceNo { get; set; }

        public long? ModuleId { get; set; }
        public long? ApplicationId { get; set; }

        public bool IsActive { get; set; }

        public string StateStatus { get; set; }
    }
}
