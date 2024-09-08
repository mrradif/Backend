using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.Linq.Expressions;

namespace BLL.Repository.Generic.Interface.Put
{
    public interface IPutGenericRepo<T, TContext, TDto, TDtoResult> where T : class where TContext : DbContext
    {
        Task<UpdateResponse<TDtoResult>> UpdateAsync(TDto dto);
        Task<UpdateResponse<UpdateRangeResult<TDtoResult>>> UpdateRangeAsync(IEnumerable<TDto> dtos, IEnumerable<Expression<Func<T, bool>>> predicates);

        Task<Response<T>> CheckExistsAsync(Expression<Func<T, bool>> predicate, TContext context);
        Task<Response<T>> CheckExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
