

using Shared.View.Company_Policy_Config;
using Shared.View.Main_Menu;

namespace Shared.View.Organization_Policy_Config
{
    public class GetOrganizationPolicyConfigWithCompaniesResultViewModel: GetOrganizationPolicyConfigResultViewModel
    {
        public ICollection<GetCompanyPolicyConfigResultViewModel> CompanyPolicyConfigs { get; set; }
    }
}
