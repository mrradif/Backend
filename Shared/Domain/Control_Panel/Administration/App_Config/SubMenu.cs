
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.Common.Class;


namespace Shared.Domain.Control_Panel.Administration.App_Config
{
    public class SubMenu : AuditableEntity, IStateStatus
    {
        [Key]
        public long SubmenuId { get; set; }
        [StringLength(100)]
        public string SubmenuName { get; set; }
        [StringLength(100)]
        public string ControllerName { get; set; }
        [StringLength(100)]
        public string ActionName { get; set; }
        [StringLength(100)]
        public string Path { get; set; }
        [StringLength(100)]
        public string Component { get; set; }
        [StringLength(100)]
        public string IconClass { get; set; }
        [StringLength(100)]
        public string IconColor { get; set; }
        public bool IsViewable { get; set; }
        public bool IsActAsParent { get; set; }
        public bool HasTab { get; set; }
        public int? MenuSequence { get; set; }
        public long? ParentSubMenuId { get; set; }




        [ForeignKey("ApplicationId")]
        public long? ApplicationId { get; set; }
        public Application Application { get; set; }


        [ForeignKey("ModuleId")]
        public long? ModuleId { get; set; }
        public Module Module { get; set; }


        [ForeignKey("MainMenuId")]
        public long MainMenuId { get; set; }
        public MainMenu MainMenu { get; set; }



        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }



        public ICollection<PageTab> PageTabs { get; set; }
    }
}
