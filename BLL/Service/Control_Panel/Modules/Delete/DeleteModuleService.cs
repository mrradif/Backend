using BLL.Repository.Generic.Interface.Delete;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Modules;
using Shared.DTO.Common;
using System.Linq.Expressions;
using Shared.View.Control_Panel.Modules;

namespace BLL.Service.Control_Panel.Modules.Delete
{
    public class DeleteModuleService
    {
        private readonly IDeleteGenericRepo<Module, ControlPanelDbContext, GetModuleResultViewModel, long> _deleteRepo;

        public DeleteModuleService(IDeleteGenericRepo<Module, ControlPanelDbContext, GetModuleResultViewModel, long> deleteRepo)
        {
            _deleteRepo = deleteRepo;
        }

        // Method using IDeleteGenericRepo.DeleteAsync(TId id)
        public async Task<Response<GetModuleResultViewModel>> DeleteModuleAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(id);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<TId> ids)
        public async Task<Response<DeleteRangeResult<GetModuleResultViewModel, long>>> DeleteModulesAsync(IEnumerable<long> ids)
        {
            return await _deleteRepo.DeleteRangeAsync(ids);
        }

        // Method using IDeleteGenericRepo.DeleteAsync(Expression<Func<T, bool>> predicate)
        public async Task<Response<DeleteRangeResult<GetModuleResultViewModel, long>>> DeleteModuleWithPredicateAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(e => e.ModuleId == id);
        }

        // Method using IDeleteGenericRepo.DeleteAsync(Expression<Func<T, bool>> predicate)
        public async Task<Response<DeleteRangeResult<GetModuleResultViewModel, long>>> DeleteModuleWithPredicateAsync(Expression<Func<Module, bool>> predicate)
        {
            return await _deleteRepo.DeleteAsync(predicate);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        public async Task<Response<DeleteRangeResult<GetModuleResultViewModel, long>>> DeleteModulesWithPredicateAsync(IEnumerable<long> ids)
        {
            var predicates = ids.Select(id => (Expression<Func<Module, bool>>)(e => e.ModuleId == id)).ToList();
            return await _deleteRepo.DeleteRangeAsync(predicates);
        }

        // Method using a list of predicates directly (as DTOs)
        public async Task<Response<DeleteRangeResult<GetModuleResultViewModel, long>>> DeleteRangeModulesAsync(IEnumerable<DeleteModuleRequestDto> predicates)
        {
            var expressionPredicates = predicates.Select(dto => CreatePredicateForDeletion(dto)).ToList();
            return await _deleteRepo.DeleteRangeAsync(expressionPredicates);
        }

        // Helper method to create predicate for deletion
        private Expression<Func<Module, bool>> CreatePredicateForDeletion(DeleteModuleRequestDto dto)
        {
            return e => e.ModuleId == dto.ModuleId;
        }
    }
}
