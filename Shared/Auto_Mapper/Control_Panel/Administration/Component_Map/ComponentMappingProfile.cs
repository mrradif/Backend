

using AutoMapper;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Component;
using Shared.View.Control_Panel.Components;


namespace Shared.Auto_Mapper.Control_Panel.Administration.Component_Map
{
    public class ComponentMappingProfile: Profile
    {
        public ComponentMappingProfile()
        {
            // ....................................................................
            // .......................................Get & Delete
            // ............................ Start

            // Get Component
            CreateMap<Component, GetComponentResultViewModel>();


            // Get Component With Application and Module
            // ........ Start

            CreateMap<Component, GetComponentWithApplicationAndModuleResultViewModel>();
                
            // ........ End
            // Get Component With Application and Module



            // ....................................................................
            // .......................................Create
            // ............................ Start

            // Create Component
            CreateMap<CreateComponentRequestDto, Component>();

            // Create Component Result
            CreateMap<Component, CreateComponentResultViewModel>();


            // ............................ End
            // .......................................Create
            // ....................................................................


        }
    }
}
