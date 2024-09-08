using BLL.Repository.Generic.Interface.Delete;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Application;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;



namespace BLL.Service.Control_Panel.Applications.Delete
{
    public class DeleteApplicationService
    {
        private readonly IDeleteGenericRepo<Application, ControlPanelDbContext, GetApplicationResultViewModel, long> _deleteRepo;

        public DeleteApplicationService(IDeleteGenericRepo<Application, ControlPanelDbContext, GetApplicationResultViewModel, long> deleteRepo)
        {
            _deleteRepo = deleteRepo;
        }

        // Method using IDeleteGenericRepo.DeleteAsync(TId id)
        public async Task<Response<GetApplicationResultViewModel>> DeleteApplicationAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(id);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<TId> ids)
        public async Task<Response<DeleteRangeResult<GetApplicationResultViewModel, long>>> DeleteApplicationsAsync(IEnumerable<long> ids)
        {
            return await _deleteRepo.DeleteRangeAsync(ids);
        }

        // Method using IDeleteGenericRepo.DeleteAsync(Expression<Func<T, bool>> predicate)
        public async Task<Response<DeleteRangeResult<GetApplicationResultViewModel, long>>> DeleteApplicationWithPredicateAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(e => e.ApplicationId == id);
        }

        // Method using IDeleteGenericRepo.DeleteAsync(Expression<Func<T, bool>> predicate)
        public async Task<Response<DeleteRangeResult<GetApplicationResultViewModel, long>>> DeleteApplicationWithPredicateAsync(Expression<Func<Application, bool>> predicate)
        {
            return await _deleteRepo.DeleteAsync(predicate);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        public async Task<Response<DeleteRangeResult<GetApplicationResultViewModel, long>>> DeleteApplicationsWithPredicateAsync(IEnumerable<long> ids)
        {
            var predicates = ids.Select(id => (Expression<Func<Application, bool>>)(e => e.ApplicationId == id)).ToList();
            return await _deleteRepo.DeleteRangeAsync(predicates);
        }

        // Method using a list of predicates directly (as DTOs)
        public async Task<Response<DeleteRangeResult<GetApplicationResultViewModel, long>>> DeleteRangeApplicationsAsync(IEnumerable<DeleteApplicationRequestDto> predicates)
        {
            var expressionPredicates = predicates.Select(dto => CreatePredicateForDeletion(dto)).ToList();
            return await _deleteRepo.DeleteRangeAsync(expressionPredicates);
        }

        // Helper method to create predicate for deletion
        private Expression<Func<Application, bool>> CreatePredicateForDeletion(DeleteApplicationRequestDto dto)
        {
            return e => e.ApplicationId == dto.ApplicationId;
        }
    }
}
