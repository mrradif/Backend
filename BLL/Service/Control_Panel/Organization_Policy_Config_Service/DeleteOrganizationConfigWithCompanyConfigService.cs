using AutoMapper;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;


namespace BLL.Service.Control_Panel.Organization_Policy_Config_Service
{
    public class DeleteOrganizationConfigWithCompanyConfigService
    {
        private readonly IDeleteGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto> _deleteMasterDetailRepo;
        private readonly IMapper _mapper;

        public DeleteOrganizationConfigWithCompanyConfigService(
            IDeleteGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto> deleteMasterDetailRepo,
            IMapper mapper)
        {
            _deleteMasterDetailRepo = deleteMasterDetailRepo;
            _mapper = mapper;
        }

        public async Task<Response<MasterDetailResult<GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto>>> DeleteOrganizationWithCompaniesAsync(object organizationId)
        {
            return await _deleteMasterDetailRepo.DeleteMasterDetailAsync(
                organizationId, "OrganizationPolicyConfigId", "OrganizationPolicyConfigId");
        }
    }
}
