using AutoMapper;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.DTO.Control_Panel.Administration.Organization;

namespace BLL.Service.Control_Panel.Organization_Service
{
    public class DeleteOrganizationWithCompanyService
    {
        private readonly IDeleteGenericMasterDetailRepo<Organization, Company, ControlPanelDbContext, GetOrganizationResultDto, GetCompanyResultDto> _deleteMasterDetailRepo;
        private readonly IMapper _mapper;

        public DeleteOrganizationWithCompanyService(
            IDeleteGenericMasterDetailRepo<Organization, Company, ControlPanelDbContext, GetOrganizationResultDto, GetCompanyResultDto> deleteMasterDetailRepo,
            IMapper mapper)
        {
            _deleteMasterDetailRepo = deleteMasterDetailRepo;
            _mapper = mapper;
        }

        public async Task<Response<MasterDetailResult<GetOrganizationResultDto, GetCompanyResultDto>>> DeleteOrganizationWithCompaniesAsync(object organizationId)
        {
            return await _deleteMasterDetailRepo.DeleteMasterDetailAsync(
                organizationId, "OrganizationId", "OrganizationId");
        }
    }
}
