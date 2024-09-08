
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.Common.Class;

namespace Shared.Domain.Control_Panel.Administration.App_Config
{
    public class PageTab : AuditableEntity, IStateStatus
    {
        [Key]
        public long TabId { get; set; }
        [StringLength(100)]
        public string TabName { get; set; }
        [StringLength(30)]
        public string IconClass { get; set; }
        [StringLength(20)]
        public string IconColor { get; set; }


        [ForeignKey("SubmenuId")]
        public long? SubmenuId { get; set; }
        public SubMenu SubMenu { get; set; }


        public long MMId { get; set; }
        public long ComId { get; set; }
        public long BranchId { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(50, ErrorMessage = "StateStatus can't be longer than 50 characters")]
        public string StateStatus { get; set; }

    }
}
