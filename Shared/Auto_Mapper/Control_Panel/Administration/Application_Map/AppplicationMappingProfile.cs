using AutoMapper;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Application;
using Shared.View.Control_Panel.Applications;
using Shared.View.Control_Panel.Buttons;
using Shared.View.Control_Panel.Components;
using Shared.View.Control_Panel.Modules;
using Shared.View.Main_Menu;

namespace Shared.Auto_Mapper.Control_Panel.Administration.Application_Map
{
    public class AppplicationMappingProfile: Profile
    {

        public AppplicationMappingProfile()
        {

            // ....................................................................
            // .......................................Get & Delete
            // ............................ Start

            // Get Application
           CreateMap<Application, GetApplicationResultViewModel>();


            // Get Application With Module
            // ........ Start

            CreateMap<Application, GetApplicationWithModuleResultViewModel>();

            CreateMap<Module, GetModuleResultViewModel>();

            // ........ End
            // Get Application With Module





            // Get Application With Includes
            // ........ Start

            CreateMap<Application, GetApplicationWithIncludesResultViewModel>();

            CreateMap<MainMenu, GetMainMenuResultViewModel>();
            CreateMap<Component, GetComponentResultViewModel>();
            CreateMap<Button, GetButtonResultViewModel>();

            // ........ End
            // Get Application With Includes



            // ............................ End
            // .......................................Get & Delete
            // ....................................................................



            // ....................................................................
            // .......................................Create
            // ............................ Start

            // Create Application
            CreateMap<CreateApplicationRequestDto, Application>();
            CreateMap<Application, CreateApplicationRequestDto>();

            // Create Application Result
            CreateMap<Application, CreateApplicationResultViewModel>().ReverseMap();
   

            // ............................ End
            // .......................................Create
            // ....................................................................




            // ....................................................................
            // .......................................Update
            // ............................ Start

            // Update Application
            CreateMap<UpdateApplicationRequestDto, Application>();

            // Update Application Result
            CreateMap<Application, UpdateApplicationResultViewModel>();

            // ............................ End
            // .......................................Update
            // ....................................................................




            // ....................................................................
            // .......................................Delete
            // ............................ Start

            // Delete Application
            CreateMap<DeleteApplicationRequestDto, Application>();


            // ............................ End
            // .......................................Delete
            // ....................................................................

        }

    }
}
