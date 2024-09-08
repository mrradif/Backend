using System.Linq.Expressions;
using BLL.Repository.Generic.Interface.Remove;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Delete;
using Shared.DTO.Control_Panel.Administration.Organization;

namespace BLL.Service.Control_Panel.Organization_Service
{
    public class RemoveOrganizationService
    {
        private readonly IRemoveGenericRepo<Organization, ControlPanelDbContext, CreateOrganizationResultDto, long> _deleteRepo;


        public RemoveOrganizationService(IRemoveGenericRepo<Organization, ControlPanelDbContext, CreateOrganizationResultDto, long> deleteRepo)
        {
            _deleteRepo = deleteRepo;
        }



        // Method using IDeleteGenericRepo.DeleteAsync(TId id)
        // Delete With Primary key
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
        public async Task<Response<CreateOrganizationResultDto>> DeleteOrganizationWithPredicateAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(e => e.OrganizationId == id);
        }


        public async Task<Response<CreateOrganizationResultDto>> DeleteOrganizationWithPredicateAsync(Expression<Func<Organization, bool>> predicate)
        {
            return await _deleteRepo.DeleteAsync(predicate);
        }



        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        public async Task<Response<DeleteRangeResult<CreateOrganizationResultDto, long>>> DeleteOrganizationsWithPredicateAsync(IEnumerable<long> ids)
        {
            var predicates = ids.Select(id => (Expression<Func<Organization, bool>>)(e => e.OrganizationId == id)).ToList();
            return await _deleteRepo.DeleteRangeAsync(predicates);
        }



        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        public async Task<Response<DeleteRangeResult<CreateOrganizationResultDto, long>>> DeleteRangeOrganizationsByDtoAsync(IEnumerable<DeleteOrganizationDto> dtos)
        {
            var predicates = dtos.Select(dto => CreatePredicateForDeletion(dto)).ToList();
            return await _deleteRepo.DeleteRangeAsync(predicates);
        }




        // Helper method to create predicate for deletion
        private Expression<Func<Organization, bool>> CreatePredicateForDeletion(DeleteOrganizationDto dto)
        {
            return e =>
                e.OrganizationId == dto.OrganizationId &&
                e.OrgUniqueId == dto.OrgUniqueId &&
                e.OrgCode == dto.OrgCode &&
                e.OrganizationName == dto.OrganizationName;
        }
    }
}
