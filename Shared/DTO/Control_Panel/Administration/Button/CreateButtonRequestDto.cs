using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Shared.DTO.Control_Panel.Administration.Button
{
    public class CreateButtonRequestDto
    {
        [Required(ErrorMessage = "ButtonName is required.")]
        [StringLength(100, ErrorMessage = "ButtonName cannot be longer than 100 characters.")]
        public string ButtonName { get; set; }

        [StringLength(200, ErrorMessage = "Tooltip cannot be longer than 200 characters.")]
        public string Tooltip { get; set; }


        [Required(ErrorMessage = "Icon is required.")]
        [StringLength(150, ErrorMessage = "Icon class cannot be longer than 150 characters.")]
        public string Icon { get; set; }

        [StringLength(150, ErrorMessage = "Css class cannot be longer than 150 characters.")]
        public string CssClass { get; set; }

        public bool IsDisabled { get; set; } = false;

        public Guid? UserId { get; set; }
        public long? ApplicationId { get; set; }
        public long? ModuleId { get; set; }
        public long? ComponentId { get; set; }

        public virtual string StateStatus { get; set; } = "Pending";
    }
}
