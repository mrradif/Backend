using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Organization;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Organization_Service
{
    public class CreateOrganizationService
    {
        private readonly IPostGenericRepo<Organization, ControlPanelDbContext, CreateOrganizationRequestDto, CreateOrganizationResultDto> _postRepo;

        public CreateOrganizationService(IPostGenericRepo<Organization, ControlPanelDbContext, CreateOrganizationRequestDto, CreateOrganizationResultDto> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Response<CreateOrganizationResultDto>> AddOrganizationAsync(CreateOrganizationRequestDto createOrganizationDto)
        {
            var existsResponse = await _postRepo.CheckExistsAsync(c => c.OrgUniqueId == createOrganizationDto.OrgUniqueId);

            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<CreateOrganizationResultDto>
                {
                    Success = false,
                    Message = "Organization already exists",
                    Data = existsResponse.Data
                };
            }


            return await _postRepo.AddAsync(createOrganizationDto);
        }


        public async Task<Response<CreateOrganizationResultDto>> AddOrganizationWithExistsAsync(CreateOrganizationRequestDto createCompanyDto)
        {

            var predicates = CreatePredicate(createCompanyDto);

            return await _postRepo.AddAsync(createCompanyDto, predicates);
        }



        public async Task<Response<AddRangeResult<CreateOrganizationResultDto>>> AddOrganizatiosAsync(IEnumerable<CreateOrganizationRequestDto> createOrganizationDtos)
        {

            var predicates = createOrganizationDtos.Select(dto => CreatePredicate(dto)).ToList();

            return await _postRepo.AddRangeAsync(createOrganizationDtos, predicates);
        }



        private Expression<Func<Organization, bool>> CreatePredicate(CreateOrganizationRequestDto dto)
        {
            return e =>

                e.OrgCode == dto.OrgCode ||
                e.OrganizationName == dto.OrganizationName;
        }
    }
}
