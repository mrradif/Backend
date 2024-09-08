using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.Control_Panel.Identity;
using Shared.DTO.Control_Panel.Identity;
using Shared.DTO.Control_Panel.Identity.Role;
using Shared.DTO.Control_Panel.Identity.Role.Create;
using Shared.DTO.Control_Panel.Identity.Role.Result;
using Shared.DTO.Control_Panel.Identity.User;
using Shared.DTO.Control_Panel.Identity.User.Update;
using Shared.DTO.Control_Panel.Identity.User.View;
using System.Security.Claims;


namespace Shared.Auto_Mapper.Control_Panel.Identity
{
    public class IdentityMappingProfile:Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<ApplicationUser, CreateApplicationUserDto>().ReverseMap();

            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
           



            CreateMap<ApplicationRole, ApplicationRoleDto>();
            CreateMap<CreateRoleRequestDto, ApplicationRole>()
                       .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<ApplicationRole, CreateRoleResultDto>();


            CreateMap<ApplicationUser, ApplicationUserResultDto>();
            CreateMap<UpdateApplicationUserDto, ApplicationUser>();


        }
    }
}
