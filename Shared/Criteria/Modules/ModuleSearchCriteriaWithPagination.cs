

using Shared.Criteria.Pagination;
using System.ComponentModel.DataAnnotations;

namespace Shared.Criteria.Modules
{
    public class ModuleSearchCriteriaWithPagination: PaginationCriteria
    {
        [StringLength(100)]
        public string ModuleName { get; set; }

        public bool IsActive { get; set; }
    }
}
