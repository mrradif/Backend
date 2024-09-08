using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;

namespace BLL.Service.Control_Panel.Organization_Policy_Config_Service
{
    public class CreateOrganizationConfigIncludeCompanyConfigService
    {
        private readonly IPostGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, CreateOrganizationPolicyConfigRequestDto, GetOrganizationPolicyConfigResultDto, CreateCompanyPolicyConfigRequestDto, GetCompanyPolicyConfigResultDto> _postMasterDetailRepo;


        public CreateOrganizationConfigIncludeCompanyConfigService(

            IPostGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, CreateOrganizationPolicyConfigRequestDto, GetOrganizationPolicyConfigResultDto, CreateCompanyPolicyConfigRequestDto, GetCompanyPolicyConfigResultDto> postMasterDetailRepo)
        {
            _postMasterDetailRepo = postMasterDetailRepo;
        }




        public async Task<Response<PostMasterDetailResultDto<GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto>>> CreateOrganizationConfigIncludeCompanyConfigAsync(CreateOrganizationPolicyConfigRequestDto organizationDto, ICollection<CreateCompanyPolicyConfigRequestDto> companyDtos)
        {
            return await _postMasterDetailRepo.AddMasterDetailIncludeAsync(
                organizationDto, companyDtos, "OrganizationPolicyConfigId", "OrganizationPolicyConfigId");
        }

    }
}
