using Shared.Domain.Control_Panel.Administration.App_Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shared.Domain.Control_Panel.Administration.Org_Config
{
    public class Component:Cancel
    {
        [Key]
        public long ComponentId { get; set; }

        [Required(ErrorMessage = "ComponentName is required.")]
        [StringLength(100, ErrorMessage = "ComponentName cannot be longer than 100 characters.")]
        public string ComponentName { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string Description { get; set; }


        [ForeignKey("ApplicationId")]
        public long? ApplicationId { get; set; }
        public Application Application { get; set; }


        [ForeignKey("ModuleId")]
        public long? ModuleId { get; set; }
        public Module Module { get; set; }


        public ICollection<Button> Buttons { get; set; } = new List<Button>();


    }
}
