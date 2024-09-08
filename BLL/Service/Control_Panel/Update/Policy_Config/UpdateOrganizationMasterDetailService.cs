using AutoMapper;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;

namespace BLL.Service.Control_Panel.Update.Policy_Config
{
    public class UpdateOrganizationMasterDetailService
    {
        private readonly IPutGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, CreateOrganizationPolicyConfigRequestDto, GetOrganizationPolicyConfigResultDto, CreateCompanyPolicyConfigRequestDto, GetCompanyPolicyConfigResultDto> _putMasterDetailRepo;
        private readonly IMapper _mapper;

        public UpdateOrganizationMasterDetailService(
            IPutGenericMasterDetailRepo<OrganizationPolicyConfig, CompanyPolicyConfig, ControlPanelDbContext, CreateOrganizationPolicyConfigRequestDto, GetOrganizationPolicyConfigResultDto, CreateCompanyPolicyConfigRequestDto, GetCompanyPolicyConfigResultDto> putMasterDetailRepo,
            IMapper mapper)
        {
            _putMasterDetailRepo = putMasterDetailRepo;
            _mapper = mapper;
        }

        public async Task<Response<MasterDetailResult<GetOrganizationPolicyConfigResultDto, GetCompanyPolicyConfigResultDto>>> UpdateOrganizationWithCompaniesAsync(CreateOrganizationPolicyConfigRequestDto organizationDto, IEnumerable<CreateCompanyPolicyConfigRequestDto> companyDtos)
        {
            return await _putMasterDetailRepo.UpdateMasterDetailAsync(
                organizationDto, companyDtos, "OrganizationConfigId", "OrganizationConfigId");
        }
    }
}
