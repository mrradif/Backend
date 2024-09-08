using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using Shared.DTO.Common;
using System.Linq.Expressions;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Control_Panel.Administration.Button;
using Shared.View.Control_Panel.Buttons;

namespace BLL.Service.Control_Panel.Button_Service
{
    public class CreateButtonService
    {
        private readonly IPostGenericRepo<Button, ControlPanelDbContext, CreateButtonRequestDto, CreateButtonResultViewModel> _postRepo;

        public CreateButtonService(
            IPostGenericRepo<Button, ControlPanelDbContext, CreateButtonRequestDto, CreateButtonResultViewModel> postRepo
        )
        {
            _postRepo = postRepo ?? throw new ArgumentNullException(nameof(postRepo));
        }

        // Add a single button and check if it already exists
        public async Task<Response<CreateButtonResultViewModel>> AddButtonAsync(CreateButtonRequestDto createButtonDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(b => b.ButtonName == createButtonDto.ButtonName);

            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<CreateButtonResultViewModel>
                {
                    Success = false,
                    Message = "Button already exists",
                    Data = existsResponse.Data
                };
            }

            return await _postRepo.AddAsync(createButtonDto);
        }

        // Add a single button with existence checks using predicates
        public async Task<Response<CreateButtonResultViewModel>> AddButtonWithExistsAsync(CreateButtonRequestDto createButtonDto)
        {
            var predicates = CreatePredicate(createButtonDto);
            return await _postRepo.AddAsync(createButtonDto, predicates);
        }

        // Add multiple buttons
        public async Task<Response<AddRangeResult<CreateButtonResultViewModel>>> AddButtonsAsync(IEnumerable<CreateButtonRequestDto> createButtonDtos)
        {
            var predicates = createButtonDtos.Select(dto => CreatePredicate(dto)).ToList();
            return await _postRepo.AddRangeAsync(createButtonDtos, predicates);
        }

        // Create a predicate based on the button DTO
        private Expression<Func<Button, bool>> CreatePredicate(CreateButtonRequestDto dto)
        {
            return b =>
                b.ButtonName == dto.ButtonName;
        }
    }
}
