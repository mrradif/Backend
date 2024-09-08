

using Shared.View.Common;

namespace Shared.View.Control_Panel.Components
{
    public class GetComponentResultViewModel: ResultViewModel
    {
        public long ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string Description { get; set; }
        public long? ApplicationId { get; set; }
        public long? ModuleId { get; set; }

    }
}
