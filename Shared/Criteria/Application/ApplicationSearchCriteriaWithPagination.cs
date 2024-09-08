

using Shared.Criteria.Pagination;

namespace Shared.Criteria.Application
{
    public class ApplicationSearchCriteriaWithPagination: PaginationCriteria
    {
        public string ApplicationName { get; set; }
        public bool IsActive { get; set; }
    }
}
