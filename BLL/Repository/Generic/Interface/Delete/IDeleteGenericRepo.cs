using Shared.DTO.Common;
using System.Linq.Expressions;


namespace BLL.Repository.Generic.Interface.Delete
{
    public interface IDeleteGenericRepo<T, TContext, TDtoResult, TId>
    {
        Task<Response<TDtoResult>> DeleteAsync(TId id);
        Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<TId> ids);



        Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates);


    }
}
