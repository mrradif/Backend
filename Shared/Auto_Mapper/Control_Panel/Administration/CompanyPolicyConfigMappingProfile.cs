using AutoMapper;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;

namespace Shared.Auto_Mapper.Control_Panel.Administration
{
    public class CompanyPolicyConfigMappingProfile: Profile
    {
        public CompanyPolicyConfigMappingProfile()
        {
            CreateMap<CreateCompanyPolicyConfigRequestDto, CompanyPolicyConfig>();

            CreateMap<GetCompanyPolicyConfigResultDto, CompanyPolicyConfig>().ReverseMap();
        }
    }
}
