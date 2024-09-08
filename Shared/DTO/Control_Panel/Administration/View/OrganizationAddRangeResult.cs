
using Shared.DTO.Control_Panel.Administration.Organization;

namespace Shared.DTO.Control_Panel.Administration.View
{
    public class OrganizationAddRangeResult
    {
        public List<CreateOrganizationResultDto> ExistingOrganizations { get; set; }
        public IEnumerable<CreateOrganizationResultDto> AddedOrganizations { get; set; }
    }
}
