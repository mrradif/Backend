using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.Repository.Generic.Interface.Remove;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.DTO.Control_Panel.Administration.Delete;

namespace BLL.Service.Control_Panel.Delete
{
    public class DeleteCompanyService
    {
        private readonly IRemoveGenericRepo<Company, ControlPanelDbContext, CreateCompanyResultDto, long> _deleteRepo;

        public DeleteCompanyService(IRemoveGenericRepo<Company, ControlPanelDbContext, CreateCompanyResultDto, long> deleteRepo)
        {
            _deleteRepo = deleteRepo;
        }

        // Method using IDeleteGenericRepo.DeleteAsync(TId id)
        public async Task<Response<CreateCompanyResultDto>> DeleteCompanyAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(id);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<TId> ids)
        public async Task<Response<DeleteRangeResult<CreateCompanyResultDto, long>>> DeleteCompaniesAsync(IEnumerable<long> ids)
        {
            return await _deleteRepo.DeleteRangeAsync(ids);
        }

        // Method using IDeleteGenericRepo.DeleteAsync(Expression<Func<T, bool>> predicate)
        public async Task<Response<CreateCompanyResultDto>> DeleteCompanyWithPredicateAsync(long id)
        {
            return await _deleteRepo.DeleteAsync(e => e.CompanyId == id);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        public async Task<Response<DeleteRangeResult<CreateCompanyResultDto, long>>> DeleteCompaniesWithPredicateAsync(IEnumerable<long> ids)
        {
            var predicates = ids.Select(id => (Expression<Func<Company, bool>>)(e => e.CompanyId == id)).ToList();
            return await _deleteRepo.DeleteRangeAsync(predicates);
        }

        // Method using IDeleteGenericRepo.DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        public async Task<Response<DeleteRangeResult<CreateCompanyResultDto, long>>> DeleteCompaniesByDtoAsync(IEnumerable<DeleteCompanyDto> dtos)
        {
            var predicates = dtos.Select(dto => CreatePredicateForDeletion(dto)).ToList();
            return await _deleteRepo.DeleteRangeAsync(predicates);
        }



        // Helper method to create predicate for deletion
        private Expression<Func<Company, bool>> CreatePredicateForDeletion(DeleteCompanyDto dto)
        {
            return e =>
                e.CompanyId == dto.CompanyId &&
                e.ComUniqueId == dto.ComUniqueId &&
                e.CompanyCode == dto.CompanyCode &&
                e.CompanyName == dto.CompanyName;
        }
    }
}
