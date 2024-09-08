using BLL.Repository.Generic.Interface.Put;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Organization;
using Shared.DTO.Control_Panel.Administration.Update;
using System.Linq.Expressions;

public class UpdateOrganizationService
{
    private readonly IPutGenericRepo<Organization, ControlPanelDbContext, UpdateOrganizationDto, CreateOrganizationResultDto> _putRepo;

    public UpdateOrganizationService(IPutGenericRepo<Organization, ControlPanelDbContext, UpdateOrganizationDto, CreateOrganizationResultDto> putRepo)
    {
        _putRepo = putRepo;
    }

    public async Task<UpdateResponse<CreateOrganizationResultDto>> UpdateOrganizationAsync(UpdateOrganizationDto updateOrganizationDto)
    {
        return await _putRepo.UpdateAsync(updateOrganizationDto);
    }

    public async Task<UpdateResponse<UpdateRangeResult<CreateOrganizationResultDto>>> UpdateOrganizationsAsync(IEnumerable<UpdateOrganizationDto> updateOrganizationDtos)
    {
        var user = UserHelper.AppUser();
        foreach (var dto in updateOrganizationDtos)
        {
            dto.CreatedBy = user.EmployeeId;
            dto.CreatedDate = DateTime.UtcNow;
        }

        var predicates = updateOrganizationDtos.Select(CreatePredicate).ToList();
        return await _putRepo.UpdateRangeAsync(updateOrganizationDtos, predicates);
    }

    private Expression<Func<Organization, bool>> CreatePredicate(UpdateOrganizationDto dto)
    {
        return e =>
            e.OrganizationId == dto.OrganizationId &&
            e.OrgUniqueId == dto.OrgUniqueId;
    }
}
