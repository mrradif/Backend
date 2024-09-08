using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Organization_Policy_Config;
using System.Linq.Expressions;


namespace BLL.Service.Control_Panel.Organization_Policy_Config_Service
{
    public class CreateOrganizationPolicyConfigService
    {
        private readonly IPostGenericRepo<OrganizationPolicyConfig, ControlPanelDbContext, CreateOrganizationPolicyConfigRequestDto, GetOrganizationPolicyConfigResultDto> _postRepo;

        public CreateOrganizationPolicyConfigService(IPostGenericRepo<OrganizationPolicyConfig, ControlPanelDbContext, CreateOrganizationPolicyConfigRequestDto, GetOrganizationPolicyConfigResultDto> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Response<GetOrganizationPolicyConfigResultDto>> AddOrganizationPolicyConfigAsync(CreateOrganizationPolicyConfigRequestDto createDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(o => o.OrganizationPolicyConfigName == createDto.OrganizationPolicyConfigName);
            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<GetOrganizationPolicyConfigResultDto>
                {
                    Success = false,
                    Message = "Organization policy configuration already exists",
                    Data = existsResponse.Data
                };
            }

            var user = UserHelper.AppUser();
            if (user != null)
            {
                createDto.CreatedBy = user.EmployeeId;
                createDto.CreatedDate = DateTime.UtcNow;
            }

            return await _postRepo.AddAsync(createDto);
        }

        public async Task<Response<AddRangeResult<GetOrganizationPolicyConfigResultDto>>> CreateOrganizationPolicyConfigsAsync(IEnumerable<CreateOrganizationPolicyConfigRequestDto> createDtos)
        {
            var user = UserHelper.AppUser();
            if (user != null)
            {
                foreach (var dto in createDtos)
                {
                    dto.CreatedBy = user.EmployeeId;
                    dto.CreatedDate = DateTime.UtcNow;
                }
            }

            var predicates = createDtos.Select(dto => CreatePredicate(dto)).ToList();

            return await _postRepo.AddRangeAsync(createDtos, predicates);
        }

        private Expression<Func<OrganizationPolicyConfig, bool>> CreatePredicate(CreateOrganizationPolicyConfigRequestDto dto)
        {
            return e =>
                e.OrganizationPolicyConfigName == dto.OrganizationPolicyConfigName;
        }
    }
}
