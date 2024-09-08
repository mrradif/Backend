using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.DTO.Control_Panel.Administration.Organization;

namespace BLL.Service.Control_Panel.Organization_Service
{
    public class GetOrganzationWithCompanyService
    {
        private readonly IGetGenericMasterDetailRepo<Organization, Company, ControlPanelDbContext, GetOrganizationResultDto, GetCompanyResultDto> _masterDetailRepo;

        public GetOrganzationWithCompanyService(IGetGenericMasterDetailRepo<Organization, Company, ControlPanelDbContext, GetOrganizationResultDto, GetCompanyResultDto> masterDetailRepo)
        {
            _masterDetailRepo = masterDetailRepo ?? throw new ArgumentNullException(nameof(masterDetailRepo));
        }

        public async Task<Response<MasterDetailResult<GetOrganizationResultDto, GetCompanyResultDto>>> GetOrganizationWithCompaniesAsync(object organizationId)
        {
            return await _masterDetailRepo.GetMasterDetailAsync(
                organizationId, "OrganizationId", "OrganizationId");
        }
    }
}
