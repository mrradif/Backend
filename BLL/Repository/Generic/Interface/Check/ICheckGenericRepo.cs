
using Shared.DTO.Common;
using System.Linq.Expressions;

namespace BLL.Repository.Generic.Interface.Check
{
    public interface ICheckGenericRepo<T, TContext, TDto, TDtoResult>
    {

        Task<Response<TDtoResult>> CheckExistsAsync(Expression<Func<T, bool>> predicate, TContext context);
        Task<Response<TDtoResult>> CheckExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
