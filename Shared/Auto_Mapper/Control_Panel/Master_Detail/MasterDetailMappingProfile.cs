using AutoMapper;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;


namespace Shared.Auto_Mapper.Control_Panel.Master_Detail
{
    public class MasterDetailMappingProfile : Profile
    {
        public MasterDetailMappingProfile()
        {

            CreateMap<CreateOrganizationPolicyConfigRequestDto, OrganizationPolicyConfig>();
            CreateMap<OrganizationPolicyConfig, GetOrganizationPolicyConfigResultDto>();


            CreateMap<CreateCompanyPolicyConfigRequestDto, CompanyPolicyConfig>();
            CreateMap<CompanyPolicyConfig, GetCompanyPolicyConfigResultDto>();
        }
    }
}
