using AutoMapper;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Create;
using Shared.DTO.Control_Panel.Administration.Create.User_Creation_Config;
using Shared.DTO.Control_Panel.Administration.User_Creation_Config;
using Shared.DTO.Control_Panel.Administration.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Auto_Mapper.Control_Panel.Administration
{
    public class UserCreationConfigMappingProfile: Profile
    {
        public UserCreationConfigMappingProfile()
        {
            CreateMap<CreateUserCreationConfigDto, UserCreationConfig>();

            CreateMap<UserCreationConfig, GetUserCreationConfigResultDto>();
        }
    }
}
