using AutoMapper;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.DTO.Control_Panel.Administration.Delete;
using Shared.DTO.Control_Panel.Administration.Organization;
using Shared.DTO.Control_Panel.Administration.Organization.OrganizationWithCompany;
using Shared.DTO.Control_Panel.Administration.Update;


namespace Shared.Auto_Mapper.Control_Panel.Administration.Organization_Map
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {


            // ..........................................................................
            // .............................................. Single Entity Get
            // ................................ Start
            
            // Also For Delete
            CreateMap<Organization, GetOrganizationResultDto>();


            // ................................ End
            // .............................................. Single Entity Get
            // ..........................................................................





            // ..........................................................................
            // .............................................. Get With Company
            // ................................ Start


            // With Includes
            CreateMap<Organization, GetOrganizationWithCompanyResultDto>()
                .ForMember(dest => dest.Companies, opt => opt.MapFrom(src => src.Companies));



            // Without Includes
            // Also For Delete
            CreateMap<Company, GetCompanyResultDto>();



            // ................................ End
            // .............................................. Get With Company
            // ..........................................................................






            // ...........................................................................
            // .............................................. Single Entity Create
            // ................................. Start

            CreateMap<CreateOrganizationRequestDto, Organization>();

            CreateMap<Organization, CreateOrganizationResultDto>();

            // For Add Range
            CreateMap<Organization, CreateOrganizationResultDto>().ReverseMap();



            // ................................. End
            // .............................................. Single Entity Create
            // ...........................................................................






            // ...........................................................................
            // .............................................. Create Master Detail
            // ............................ Start


            CreateMap<CreateOrganizationIncludesCompanyRequestDto, Organization>();
      


            CreateMap<CreateCompanyRequestDto, Company>();
            CreateMap<Company, CreateCompanyResultDto>();


            // ............................. End
            // .............................................. Create Master Detail
            // ...........................................................................






            // Update
            CreateMap<UpdateOrganizationDto, Organization>();

            // Update Range
            CreateMap<UpdateOrganizationDto, CreateOrganizationResultDto>();


            // Delete
            CreateMap<DeleteOrganizationDto, Organization>();

            // DeleteRange
            CreateMap<DeleteOrganizationDto, CreateOrganizationResultDto>();

        }
    }
}
