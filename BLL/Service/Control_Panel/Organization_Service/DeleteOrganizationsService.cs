using BLL.Repository.Generic.Interface.Delete;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Delete;
using Shared.DTO.Control_Panel.Administration.Organization;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Service.Control_Panel.Organization_Service
{
    public class DeleteOrganizationsService
    {
        private readonly IDeleteGenericRepo<Organization, ControlPanelDbContext, CreateOrganizationResultDto, long> _deleteRepo;

        public DeleteOrganizationsService(IDeleteGenericRepo<Organization, ControlPanelDbContext, CreateOrganizationResultDto, long> deleteRepo)
        {
            _deleteRepo = deleteRepo;
        }

        // Method using IDeleteGenericRepo.DeleteAsync(TId id)
        public async Task<Response<CreateOrganizationResultDto>> DeleteOrganizationAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(id);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<TId> ids)
        public async Task<Response<DeleteRangeResult<CreateOrganizationResultDto, long>>> DeleteOrganizationsAsync(IEnumerable<long> ids)
        {
            return await _deleteRepo.DeleteRangeAsync(ids);
        }

        // Method using IDeleteGenericRepo.DeleteAsync(Expression<Func<T, bool>> predicate)
        public async Task<Response<DeleteRangeResult<CreateOrganizationResultDto, long>>> DeleteOrganizationWithPredicateAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(e => e.OrganizationId == id);
        }

        // Method using IDeleteGenericRepo.DeleteAsync(Expression<Func<T, bool>> predicate)
        public async Task<Response<DeleteRangeResult<CreateOrganizationResultDto, long>>> DeleteOrganizationWithPredicateAsync(Expression<Func<Organization, bool>> predicate)
        {
            return await _deleteRepo.DeleteAsync(predicate);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        public async Task<Response<DeleteRangeResult<CreateOrganizationResultDto, long>>> DeleteOrganizationsWithPredicateAsync(IEnumerable<long> ids)
        {
            var predicates = ids.Select(id => (Expression<Func<Organization, bool>>)(e => e.OrganizationId == id)).ToList();
            return await _deleteRepo.DeleteRangeAsync(predicates);
        }






        // Method using a list of predicates directly (as DTOs)
        public async Task<Response<DeleteRangeResult<CreateOrganizationResultDto, long>>> DeleteRangeOrganizationsAsync(IEnumerable<DeleteOrganizationDto> predicates)
        {
            var expressionPredicates = predicates.Select(dto => CreatePredicateForDeletion(dto)).ToList();
            return await _deleteRepo.DeleteRangeAsync(expressionPredicates);
        }


        // Helper method to create predicate for deletion
        private Expression<Func<Organization, bool>> CreatePredicateForDeletion(DeleteOrganizationDto dto)
        {
            return e => e.OrganizationId == dto.OrganizationId;
        }




    }
}
