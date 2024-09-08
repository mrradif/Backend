using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;

namespace BLL.Service.Control_Panel.Organization_Policy_Config_Service
{

    public class CreateOrganizationConfigWithCompanyConfigService
    {
        private readonly IPostGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, CreateOrganizationPolicyConfigRequestDto, GetOrganizationPolicyConfigResultDto, CreateCompanyPolicyConfigRequestDto, GetCompanyPolicyConfigResultDto> _postMasterDetailRepo;


        public CreateOrganizationConfigWithCompanyConfigService(
            IPostGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, CreateOrganizationPolicyConfigRequestDto, GetOrganizationPolicyConfigResultDto, CreateCompanyPolicyConfigRequestDto, GetCompanyPolicyConfigResultDto> postMasterDetailRepo)
        {
            _postMasterDetailRepo = postMasterDetailRepo;
        }


        public async Task<Response<MasterDetailResult<GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto>>> CreateOrganizationConfigWithCompanyConfigAsync(CreateOrganizationPolicyConfigRequestDto organizationDto, IEnumerable<CreateCompanyPolicyConfigRequestDto> companyDtos)
        {
            return await _postMasterDetailRepo.AddMasterDetailAsync(
                organizationDto, companyDtos, "OrganizationPolicyConfigId", "OrganizationPolicyConfigId");
        }


    }
}
