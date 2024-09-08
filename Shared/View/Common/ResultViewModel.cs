
namespace Shared.View.Common
{
    public class ResultViewModel
    {
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRestored { get; set; }
        public bool IsCancelled { get; set; }

        public bool IsActivated { get; set; }
        public bool IsDeactived { get; set; }
        public bool IsActive { get; set; }
        public string StateStatus { get; set; }
    }
}
