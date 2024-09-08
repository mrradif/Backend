
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.Common.Class;

namespace Shared.Domain.Control_Panel.Administration.App_Config
{
    public class MainMenu : AuditableEntity
    {
        [Key]
        public long MainMenuId { get; set; }


        [StringLength(100)]
        public string MainMenuName { get; set; }


        [StringLength(50)]
        public string MainMenuShortName { get; set; }


        [StringLength(100)]
        public string IconClass { get; set; }


        [StringLength(100)]
        public string IconColor { get; set; }

        public int? SequenceNo { get; set; }
        


        [ForeignKey("ModuleId")]
        public long? ModuleId { get; set; }
        public Module Module { get; set; }



        [ForeignKey("ApplicationId")]
        public long? ApplicationId { get; set; }
        public Application Application { get; set; }






        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }


        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}
