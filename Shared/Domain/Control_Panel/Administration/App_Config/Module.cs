
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.Control_Panel.Administration.Org_Config;


namespace Shared.Domain.Control_Panel.Administration.App_Config { 
    public class Module : Cancel
    {
        [Key]
        public long ModuleId { get; set; }
        [StringLength(100)]
        public string ModuleName { get; set; }



        [ForeignKey("ApplicationId")]
        public long? ApplicationId { get; set; }
        public Application Application { get; set; }



        public virtual ICollection<MainMenu> MainMenus { get; set; }
        public virtual ICollection<Component> Components { get; set; }
        public virtual ICollection<Button> Buttons { get; set; }
    }
}
