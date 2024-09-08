using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using System.Linq.Expressions;


namespace BLL.Repository.Generic.Interface.Get
{
    public interface IGetGenericRepoWithPagination<T, TContext, TDtoResult, TId>
        where T : class
        where TContext : DbContext
    {
        Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize);

        Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);

        Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize);





        Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes);

        Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includes);



        Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes);

        Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes);
    }
}

