

using Shared.Criteria.Pagination;

namespace Shared.Criteria.Application
{
    public class ApplicationSearchCriteria
    {
        public string ApplicationName { get; set; }
        public string ApplicationType { get; set; }
        public bool IsActive { get; set; }
    }


}
