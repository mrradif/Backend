using Shared.DTO.Common;
using System.Linq.Expressions;


namespace BLL.Repository.Generic.Interface.Remove
{
    public interface IRemoveGenericRepo<T, TContext, TDtoResult, TId>
    {
        Task<Response<TDtoResult>> DeleteAsync(TId id);
        Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<TId> ids);


        Task<Response<TDtoResult>> DeleteAsync(Expression<Func<T, bool>> predicate);

        Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates);



    }
}
