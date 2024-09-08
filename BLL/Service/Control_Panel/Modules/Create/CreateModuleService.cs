using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Modules;
using Shared.DTO.Common;
using System.Linq.Expressions;
using Shared.View.Control_Panel.Modules;

namespace BLL.Service.Control_Panel.Modules.Create
{
    public class CreateModuleService
    {
        private readonly IPostGenericRepo<Module, ControlPanelDbContext, CreateModuleRequestDto, CreateModuleResultViewModel> _postRepo;

        public CreateModuleService(IPostGenericRepo<Module, ControlPanelDbContext, CreateModuleRequestDto, CreateModuleResultViewModel> postRepo)
        {
            _postRepo = postRepo;
        }

        // Add a single module and check if it already exists
        public async Task<Response<CreateModuleResultViewModel>> AddModuleAsync(CreateModuleRequestDto createModuleDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(c => c.ModuleName == createModuleDto.ModuleName);

            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<CreateModuleResultViewModel>
                {
                    Success = false,
                    Message = "Module already exists",
                    Data = existsResponse.Data
                };
            }

            return await _postRepo.AddAsync(createModuleDto);
        }

        // Add a single module with existence checks using predicates
        public async Task<Response<CreateModuleResultViewModel>> AddModuleWithExistsAsync(CreateModuleRequestDto createModuleDto)
        {
            var predicates = CreatePredicate(createModuleDto);
            return await _postRepo.AddAsync(createModuleDto, predicates);
        }

        // Add multiple modules
        public async Task<Response<AddRangeResult<CreateModuleResultViewModel>>> AddModulesAsync(IEnumerable<CreateModuleRequestDto> createModuleDtos)
        {
            var predicates = createModuleDtos.Select(dto => CreatePredicate(dto)).ToList();
            return await _postRepo.AddRangeAsync(createModuleDtos, predicates);
        }

        // Create a predicate based on the module DTO
        private Expression<Func<Module, bool>> CreatePredicate(CreateModuleRequestDto dto)
        {
            return e =>
                e.ModuleName == dto.ModuleName;
        }
    }
}
