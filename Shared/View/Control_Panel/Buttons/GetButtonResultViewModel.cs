
using Shared.Domain.Control_Panel.Administration.Org_Config;

namespace Shared.View.Control_Panel.Buttons
{
    public class GetButtonResultViewModel
    {
        public long ButtonId { get; set; }
        public string ButtonName { get; set; }
        public string Tooltip { get; set; }
        public string OnClickActionName { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }


        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRestored { get; set; }
        public bool IsCancelled { get; set; }

        public bool IsActivated { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeactived { get; set; }

        public string StateStatus { get; set; }

        public bool IsDisabled { get; set; } = false;

        public Guid? UserId { get; set; }
        public long? ApplicationId { get; set; }
        public long? ModuleId { get; set; }
        public long? ComponentId { get; set; }


        public string ComponentName { get; set; }
    }
}
