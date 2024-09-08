using AutoMapper;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.DTO.Control_Panel.Administration.Delete;
using Shared.DTO.Control_Panel.Administration.Update;
using Shared.DTO.Control_Panel.Administration.View;

namespace Shared.Auto_Mapper.Control_Panel.Administration.Company_Map
{
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            // Create

            CreateMap<CreateCompanyResultDto, Company>();

            // Create Result

            CreateMap<Company, CreateCompanyResultDto>();



            // View
            CreateMap<Company, CreateCompanyResultDto>();


            CreateMap<CreateCompanyResultDto, CompanyWithOrganizationDtoResult>()
                .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.OrganizationId));

            CreateMap<CreateCompanyResultDto, CompanyWithOrganizationDtoResult>();

            // Optionally, you can create a reverse mapping if needed
            CreateMap<CompanyWithOrganizationDtoResult, CreateCompanyResultDto>();

            // Update
            CreateMap<UpdateCompanyDto, Company>();

            // Update Range
            CreateMap<UpdateCompanyDto, CreateCompanyResultDto>();

            // Delete
            CreateMap<DeleteCompanyDto, Company>();

            // DeleteRange
            CreateMap<DeleteCompanyDto, CreateCompanyResultDto>();
        }
    }
}
