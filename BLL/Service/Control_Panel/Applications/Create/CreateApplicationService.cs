using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Application;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;


namespace BLL.Service.Control_Panel.Applications.Create
{
    public class CreateApplicationService
    {
        private readonly IPostGenericRepo<Application, ControlPanelDbContext, CreateApplicationRequestDto, CreateApplicationResultViewModel> _postRepo;

        public CreateApplicationService(IPostGenericRepo<Application, ControlPanelDbContext, CreateApplicationRequestDto, CreateApplicationResultViewModel> postRepo)
        {
            _postRepo = postRepo;
        }

        // Add a single application and check if it already exists
        public async Task<Response<CreateApplicationResultViewModel>> AddApplicationAsync(CreateApplicationRequestDto createApplicationDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(c => c.ApplicationName == createApplicationDto.ApplicationName);

            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<CreateApplicationResultViewModel>
                {
                    Success = false,
                    Message = "Application already exists",
                    Data = existsResponse.Data
                };
            }

            return await _postRepo.AddAsync(createApplicationDto);
        }


        // Add a single application with existence checks using predicates
        public async Task<Response<CreateApplicationResultViewModel>> AddApplicationWithExistsAsync(CreateApplicationRequestDto createApplicationDto)
        {
            var predicates = CreatePredicate(createApplicationDto);
            return await _postRepo.AddAsync(createApplicationDto, predicates);
        }



        // Add multiple applications
        public async Task<Response<AddRangeResult<CreateApplicationResultViewModel>>> AddApplicationsAsync(IEnumerable<CreateApplicationRequestDto> createApplicationDtos)
        {
            var predicates = createApplicationDtos.Select(dto => CreatePredicate(dto)).ToList();
            return await _postRepo.AddRangeAsync(createApplicationDtos, predicates);
        }



        // Create a predicate based on the application DTO
        private Expression<Func<Application, bool>> CreatePredicate(CreateApplicationRequestDto dto)
        {
            return e =>
                e.ApplicationName == dto.ApplicationName ||
                e.ApplicationType == dto.ApplicationType;
        }
    }
}
