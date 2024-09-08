using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using Shared.DTO.Common;
using System.Linq.Expressions;
using Shared.View.Control_Panel.Components;
using Shared.DTO.Control_Panel.Administration.Component;
using Shared.Domain.Control_Panel.Administration.Org_Config;

namespace BLL.Service.Control_Panel.Component_Service
{
    public class CreateComponentService
    {
        private readonly IPostGenericRepo<Component, ControlPanelDbContext, CreateComponentRequestDto, CreateComponentResultViewModel> _postRepo;

        public CreateComponentService(
            IPostGenericRepo<Component, ControlPanelDbContext, CreateComponentRequestDto, CreateComponentResultViewModel> postRepo
        )
        {
            _postRepo = postRepo ?? throw new ArgumentNullException(nameof(postRepo));
        }

        // Add a single component and check if it already exists
        public async Task<Response<CreateComponentResultViewModel>> AddComponentAsync(CreateComponentRequestDto createComponentDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(c => c.ComponentName == createComponentDto.ComponentName);

            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<CreateComponentResultViewModel>
                {
                    Success = false,
                    Message = "Component already exists",
                    Data = existsResponse.Data
                };
            }

            return await _postRepo.AddAsync(createComponentDto);
        }

        // Add a single component with existence checks using predicates
        public async Task<Response<CreateComponentResultViewModel>> AddComponentWithExistsAsync(CreateComponentRequestDto createComponentDto)
        {
            var predicates = CreatePredicate(createComponentDto);
            return await _postRepo.AddAsync(createComponentDto, predicates);
        }

        // Add multiple components
        public async Task<Response<AddRangeResult<CreateComponentResultViewModel>>> AddComponentsAsync(IEnumerable<CreateComponentRequestDto> createComponentDtos)
        {
            var predicates = createComponentDtos.Select(dto => CreatePredicate(dto)).ToList();
            return await _postRepo.AddRangeAsync(createComponentDtos, predicates);
        }

        // Create a predicate based on the component DTO
        private Expression<Func<Component, bool>> CreatePredicate(CreateComponentRequestDto dto)
        {
            return e =>
                e.ComponentName == dto.ComponentName;
        }
    }
}
