using AutoMapper;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Button;
using Shared.View.Control_Panel.Buttons;



namespace Shared.Auto_Mapper.Control_Panel.Administration.Button_Map
{
    public class ButtonMappingProfile: Profile
    {
        public ButtonMappingProfile()
        {
            // ....................................................................
            // .......................................Get & Delete
            // ............................ Start

            // Get Button
            CreateMap<Button, GetButtonResultViewModel>();


            // Get Button With Components
            // ........ Start

            CreateMap<Button, GetButtonWithComponetResultViewModel>();

            // ........ End
            // Get Button With Components



            // ............................ Start
            // .......................................Get & Delete
            // ....................................................................





            // ....................................................................
            // .......................................Create
            // ............................ Start

            // Create Component
            CreateMap<CreateButtonRequestDto, Button>();

            // Create Component Result
            CreateMap<Button, CreateButtonResultViewModel>();


            // ............................ End
            // .......................................Create
            // ....................................................................
        }
    }
}
