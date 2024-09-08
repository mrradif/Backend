using AutoMapper;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Modules;
using Shared.View.Control_Panel.Buttons;
using Shared.View.Control_Panel.Components;
using Shared.View.Control_Panel.Modules;
using Shared.View.Main_Menu;

namespace Shared.Auto_Mapper.Control_Panel.Administration.Module_Map
{
    public class ModuleMappingProfile : Profile
    {
        public ModuleMappingProfile()
        {
            // ....................................................................
            // .......................................Get & Delete
            // ............................ Start

            // Get Module
            CreateMap<Module, GetModuleResultViewModel>();

            // Get Module with Component
            // ........... Start

            CreateMap<Module, GetModuleWithComponentsResultViewModel>();

            CreateMap<Component, GetComponentResultViewModel>();

            // ........... End
            // Get Module With Component


            // Get Module with Application
            // ........... Start

            CreateMap<Module, GetModuleWithApplicationResultViewModel>();

            // ........... End
            // Get Module With Application


            // Get Module with Includes
            // ........... Start

            CreateMap<Module, GetModuleWithIncludesResultViewModel>();

            CreateMap<MainMenu, GetMainMenuResultViewModel>();

            CreateMap<Button, GetButtonResultViewModel>();

            // ........... End
            // Get Module With Includes


            // ............................ End
            // .......................................Get & Delete
            // ....................................................................

            // ....................................................................
            // .......................................Create
            // ............................ Start

            // Create Module
            CreateMap<CreateModuleRequestDto, Module>();

            // Create Module Result
            CreateMap<Module, CreateModuleResultViewModel>();

            // ............................ End
            // .......................................Create
            // ....................................................................

            // ....................................................................
            // .......................................Update
            // ............................ Start

            // Update Module
            CreateMap<UpdateModuleRequestDto, Module>();

            // Update Module Result
            CreateMap<Module, UpdateModuleResultViewModel>();

            // ............................ End
            // .......................................Update
            // ....................................................................

            // ....................................................................
            // .......................................Delete
            // ............................ Start

            // Delete Module
            CreateMap<DeleteModuleRequestDto, Module>();

            // ............................ End
            // .......................................Delete
            // ....................................................................
        }
    }
}
