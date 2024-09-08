using Shared.Domain.Control_Panel.Administration.App_Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Control_Panel.Administration.Org_Config
{
    public class Button: Cancel
    {
        [Key]
        public long ButtonId { get; set; }

        [Required(ErrorMessage = "ButtonType is required.")]
        [StringLength(50, ErrorMessage = "ButtonType cannot be longer than 50 characters.")]
        public string ButtonType { get; set; }

        [Required(ErrorMessage = "ActionName is required.")]
        [StringLength(100, ErrorMessage = "ActionName cannot be longer than 100 characters.")]
        public string ButtonName { get; set; }


        [StringLength(200, ErrorMessage = "Tooltip cannot be longer than 200 characters.")]
        public string Tooltip { get; set; }


        [Required(ErrorMessage = "Icon is required.")]
        [StringLength(50, ErrorMessage = "Icon class cannot be longer than 50 characters.")]
        public string Icon { get; set; }


        [StringLength(50, ErrorMessage = "Css class cannot be longer than 50 characters.")]
        public string CssClass { get; set; }

        public bool IsDisabled { get; set; } = true;




        // Navigation Property
        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }


        [ForeignKey("ApplicationId")]
        public long? ApplicationId { get; set; }
        public Application Application { get; set; }


        [ForeignKey("ModuleId")]
        public long? ModuleId { get; set; }
        public Module Module { get; set; }


        public long? ComponentId { get; set; }

        [ForeignKey(nameof(ComponentId))]
        public Component Components { get; set; }



        [Required(ErrorMessage = "ComponentName is required.")]
        [StringLength(100, ErrorMessage = "ComponentName cannot be longer than 100 characters.")]
        public string ComponentName { get; set; }



    }
}
