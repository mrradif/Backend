using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.Linq.Expressions;

namespace BLL.Repository.Generic.Interface.Get
{
    public interface IGetGenericRepo<T, TContext, TDtoResult, TId>
        where T : class
        where TContext : DbContext
    {

        // .........................Get All Start
        Task<Response<IEnumerable<TDtoResult>>> GetAllAsync();

        // With Parameter
        Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate);

        // With Parameter and Order By
        Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        // ........................Get All End




        // .........................Get Single Start

        Task<Response<TDtoResult>> GetSingleAsync(TId id);
        Task<Response<TDtoResult>> GetSingleAsync(Expression<Func<T, bool>> predicate);

        // .........................Get Single End





        // ............................Get All With Includes Start
        Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);

        // ............................Get All With Includes End




        // ............................... Get Single With Include Start

        Task<Response<TDtoResult>> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        // ............................... Get Single With Include End
    }
}
