using AutoMapper;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Create;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;
using Shared.View.Company_Policy_Config;
using Shared.View.Organization_Policy_Config;



namespace Shared.Auto_Mapper.Control_Panel.Administration.Organization_Policy_Config_Map
{
    public class OrganizationPolicyConfigMappingProfile : Profile
    {
        public OrganizationPolicyConfigMappingProfile()
        {
            // ....................................................................
            // .......................................Get & Delete
            // ............................ Start

            // Get Organization Policy Config
            CreateMap<OrganizationPolicyConfig, GetOrganizationPolicyConfigResultViewModel>();


            // Get Organization Policy Config With Company Policy Config
            // ........ Start

            CreateMap<OrganizationPolicyConfig, GetOrganizationPolicyConfigWithCompaniesResultViewModel>();

            CreateMap<CompanyPolicyConfig, GetCompanyPolicyConfigResultViewModel>();

            // ........ End
            // Get Organization Policy Config With Company Policy Config



            // ............................ End
            // .......................................Get & Delete
            // ....................................................................



            // Create
            CreateMap<CreateOrganizationPolicyConfigDto, GetOrganizationPolicyConfigResultDto>();

            // Create Result
            CreateMap<GetOrganizationPolicyConfigResultDto, OrganizationPolicyConfig>().ReverseMap();
        }
    }
}
