using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Create.User_Creation_Config;
using Shared.DTO.Control_Panel.Administration.User_Creation_Config;

namespace BLL.Service.Control_Panel.User_Creation_Config_Service
{
    public class GetCreateUserCreationConfigService
    {
        private readonly IPostGenericRepo<UserCreationConfig, ControlPanelDbContext, CreateUserCreationConfigDto, GetUserCreationConfigResultDto> _postRepo;

        public GetCreateUserCreationConfigService(IPostGenericRepo<UserCreationConfig, ControlPanelDbContext, CreateUserCreationConfigDto, GetUserCreationConfigResultDto> postRepo)
        {
            _postRepo = postRepo;
        }


        public async Task<Response<GetUserCreationConfigResultDto>> AddUserCreationConfigAsync(CreateUserCreationConfigDto createUserCreationConfigDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(
                c => c.OrganizationId == createUserCreationConfigDto.OrganizationId &&
                c.CompanyId == createUserCreationConfigDto.CompanyId &&
                c.IsActive == true
                );


            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<GetUserCreationConfigResultDto>
                {
                    Success = false,
                    Message = $"The config already exists",
                    Data = existsResponse.Data
                };
            }

            var user = UserHelper.AppUser();
            if (user != null)
            {
                createUserCreationConfigDto.CreatedBy = user.EmployeeId;
                createUserCreationConfigDto.CreatedDate = DateTime.UtcNow;
            }

            createUserCreationConfigDto.IsActive = true;

            return await _postRepo.AddAsync(createUserCreationConfigDto);
        }
    }
}
