using BLL.Repository.Generic.Interface.Put;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Application;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;


namespace BLL.Service.Control_Panel.Applications.Update
{
    public class UpdateApplicationService
    {
        private readonly IPutGenericRepo<Application, ControlPanelDbContext, UpdateApplicationRequestDto, UpdateApplicationResultViewModel> _putRepo;

        public UpdateApplicationService(IPutGenericRepo<Application, ControlPanelDbContext, UpdateApplicationRequestDto, UpdateApplicationResultViewModel> putRepo)
        {
            _putRepo = putRepo;
        }

        public async Task<UpdateResponse<UpdateApplicationResultViewModel>> UpdateApplicationAsync(UpdateApplicationRequestDto updateApplicationDto)
        {
            return await _putRepo.UpdateAsync(updateApplicationDto);
        }

        public async Task<UpdateResponse<UpdateRangeResult<UpdateApplicationResultViewModel>>> UpdateApplicationsAsync(IEnumerable<UpdateApplicationRequestDto> updateApplicationDtos)
        {
            var predicates = updateApplicationDtos.Select(CreatePredicate).ToList();
            return await _putRepo.UpdateRangeAsync(updateApplicationDtos, predicates);
        }

        private Expression<Func<Application, bool>> CreatePredicate(UpdateApplicationRequestDto dto)
        {
            return e =>
                e.ApplicationId == dto.ApplicationId;
        }
    }
}
