using Shared.Domain.Control_Panel.Administration.Org_Config;
using System.ComponentModel.DataAnnotations;


namespace Shared.Domain.Control_Panel.Administration.App_Config
{
    public class Application : Cancel
    {
        [Key]
        public long ApplicationId { get; set; }

        [Required]
        [StringLength(100)]
        public string ApplicationName { get; set; }

        [Required]
        [StringLength(50)]
        public string ApplicationType { get; set; }

        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Component> Components { get; set; }
        public virtual ICollection<MainMenu> MainMenus { get; set; }
        public virtual ICollection<Button> Buttons { get; set; }
    }
}
