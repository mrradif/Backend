using Shared.DTO.Common;
using DAL.Context.Control_Panel;
using System.Linq.Expressions;
using BLL.Repository.Generic.Interface.Post;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.Domain.Control_Panel.Administration.Org_Config;

namespace BLL.Service.Control_Panel.Company_Service
{
    public class CreateCompanyService
    {
        private readonly IPostGenericRepo<Company, ControlPanelDbContext, CreateCompanyRequestDto, CreateCompanyResultDto> _postRepo;

        public CreateCompanyService(IPostGenericRepo<Company, ControlPanelDbContext, CreateCompanyRequestDto, CreateCompanyResultDto> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Response<CreateCompanyResultDto>> AddCompanyAsync(CreateCompanyRequestDto createCompanyDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(c => c.ComUniqueId == createCompanyDto.ComUniqueId);

            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<CreateCompanyResultDto>
                {
                    Success = false,
                    Message = "Company already exists",
                    Data = existsResponse.Data
                };
            }

            var user = UserHelper.AppUser();
            if (user != null)
            {
                createCompanyDto.CreatedBy = user.EmployeeId ?? user.Id.ToString();
                createCompanyDto.CreatedDate = DateTime.UtcNow;
            }

            return await _postRepo.AddAsync(createCompanyDto);
        }


        public async Task<Response<CreateCompanyResultDto>> AddCompanyWithExistsAsync(CreateCompanyRequestDto createCompanyDto)
        {

            var user = UserHelper.AppUser();
            if (user != null)
            {
                createCompanyDto.CreatedBy = user.EmployeeId ?? user.Id.ToString();
                createCompanyDto.CreatedDate = DateTime.UtcNow;
            }

            var predicates = CreatePredicate(createCompanyDto);

            return await _postRepo.AddAsync(createCompanyDto, predicates);
        }



        public async Task<Response<AddRangeResult<CreateCompanyResultDto>>> CreateCompaniesAsync(IEnumerable<CreateCompanyRequestDto> createCompanyDtos)
        {
            var user = UserHelper.AppUser();
            if (user != null)
            {
                foreach (var dto in createCompanyDtos)
                {
                    dto.CreatedBy = user.EmployeeId ?? user.Id.ToString();
                    dto.CreatedDate = DateTime.UtcNow;
                }
            }

            var predicates = createCompanyDtos.Select(dto => CreatePredicate(dto)).ToList();

            return await _postRepo.AddRangeAsync(createCompanyDtos, predicates);
        }

        private Expression<Func<Company, bool>> CreatePredicate(CreateCompanyRequestDto dto)
        {
            return e =>

                e.CompanyCode == dto.CompanyCode ||
                e.CompanyName == dto.CompanyName;
        }
    }
}
