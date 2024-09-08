using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.DTO.Control_Panel.Administration.Organization;

namespace BLL.Service.Control_Panel.Organization_Service
{
    public class CreateOrganizationWithIncludeCompanyService
    {
        private readonly IPostGenericMasterDetailRepo<Organization, Company, ControlPanelDbContext, CreateOrganizationRequestDto, CreateOrganizationResultDto, CreateCompanyRequestDto, CreateCompanyResultDto> _postMasterDetailRepo;


        public CreateOrganizationWithIncludeCompanyService(

            IPostGenericMasterDetailRepo<Organization, Company, ControlPanelDbContext, CreateOrganizationRequestDto, CreateOrganizationResultDto, CreateCompanyRequestDto, CreateCompanyResultDto> postMasterDetailRepo)
        {
            _postMasterDetailRepo = postMasterDetailRepo;
        }




        public async Task<Response<PostMasterDetailResultDto<CreateOrganizationResultDto, CreateCompanyResultDto>>> CreateOrganizationIncludeCompaniesAsync(CreateOrganizationRequestDto organizationDto, ICollection<CreateCompanyRequestDto> companyDtos)
        {
            return await _postMasterDetailRepo.AddMasterDetailIncludeAsync(
                organizationDto, companyDtos, "OrganizationId", "OrganizationId");
        }



        public async Task<Response<MasterDetailResult<CreateOrganizationResultDto, CreateCompanyResultDto>>> CreateOrganizationWithCompaniesAsync(CreateOrganizationRequestDto organizationDto, IEnumerable<CreateCompanyRequestDto> companyDtos)
        {
            return await _postMasterDetailRepo.AddMasterDetailAsync(
                organizationDto, companyDtos, "OrganizationId", "OrganizationId");
        }


    }
}
