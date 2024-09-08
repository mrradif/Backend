using BLL.Repository.Generic.Interface.Put;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Modules;
using Shared.DTO.Common;
using System.Linq.Expressions;
using Shared.View.Control_Panel.Modules;

namespace BLL.Service.Control_Panel.Modules.Update
{
    public class UpdateModuleService
    {
        private readonly IPutGenericRepo<Module, ControlPanelDbContext, UpdateModuleRequestDto, UpdateModuleResultViewModel> _putRepo;

        public UpdateModuleService(IPutGenericRepo<Module, ControlPanelDbContext, UpdateModuleRequestDto, UpdateModuleResultViewModel> putRepo)
        {
            _putRepo = putRepo;
        }

        public async Task<UpdateResponse<UpdateModuleResultViewModel>> UpdateModuleAsync(UpdateModuleRequestDto updateModuleDto)
        {
            return await _putRepo.UpdateAsync(updateModuleDto);
        }

        public async Task<UpdateResponse<UpdateRangeResult<UpdateModuleResultViewModel>>> UpdateModulesAsync(IEnumerable<UpdateModuleRequestDto> updateModuleDtos)
        {
            var predicates = updateModuleDtos.Select(CreatePredicate).ToList();
            return await _putRepo.UpdateRangeAsync(updateModuleDtos, predicates);
        }

        private Expression<Func<Module, bool>> CreatePredicate(UpdateModuleRequestDto dto)
        {
            return e =>
                e.ModuleId == dto.ModuleId;
        }
    }
}
