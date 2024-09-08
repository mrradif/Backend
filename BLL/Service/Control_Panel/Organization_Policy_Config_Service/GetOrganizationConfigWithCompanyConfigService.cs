using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;


namespace BLL.Service.Control_Panel.Organization_Policy_Config_Service
{
    public class GetOrganizationConfigWithCompanyConfigService
    {
        private readonly IGetGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto> _masterDetailRepo;

        public GetOrganizationConfigWithCompanyConfigService(IGetGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto> masterDetailRepo)
        {
            _masterDetailRepo = masterDetailRepo ?? throw new ArgumentNullException(nameof(masterDetailRepo));
        }

        public async Task<Response<MasterDetailResult<GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto>>> GetOrganizationConfigWithCompanyConfigAsync(object organizationId)
        {
            return await _masterDetailRepo.GetMasterDetailAsync(
                organizationId, "OrganizationPolicyConfigId", "OrganizationPolicyConfigId");
        }



    }
}
