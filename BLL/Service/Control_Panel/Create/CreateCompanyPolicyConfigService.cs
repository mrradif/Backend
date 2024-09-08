using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company_Policy_Config;
using System.Linq.Expressions;


namespace BLL.Service.Control_Panel.Create
{
    public class CreateCompanyPolicyConfigService
    {
        private readonly IPostGenericRepo<CompanyPolicyConfig, ControlPanelDbContext, CreateCompanyPolicyConfigRequestDto, GetCompanyPolicyConfigResultDto> _postRepo;

        public CreateCompanyPolicyConfigService(IPostGenericRepo<CompanyPolicyConfig, ControlPanelDbContext, CreateCompanyPolicyConfigRequestDto, GetCompanyPolicyConfigResultDto> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Response<GetCompanyPolicyConfigResultDto>> AddCompanyPolicyConfigAsync(CreateCompanyPolicyConfigRequestDto createDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(o => o.CompanyPolicyConfigName == createDto.CompanyPolicyConfigName);
            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<GetCompanyPolicyConfigResultDto>
                {
                    Success = false,
                    Message = "Company policy configuration already exists",
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

        public async Task<Response<AddRangeResult<GetCompanyPolicyConfigResultDto>>> CreateCompanyPolicyConfigsAsync(IEnumerable<CreateCompanyPolicyConfigRequestDto> createDtos)
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

        private Expression<Func<CompanyPolicyConfig, bool>> CreatePredicate(CreateCompanyPolicyConfigRequestDto dto)
        {
            return e =>
                e.CompanyPolicyConfigName == dto.CompanyPolicyConfigName;
        }
    }
}
