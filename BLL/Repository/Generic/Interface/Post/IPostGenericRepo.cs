using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.Linq.Expressions;


namespace BLL.Repository.Generic.Interface.Post
{
    public interface IPostGenericRepo<T, TContext, TDto, TDtoResult>
        where T : class
        where TContext : DbContext
    {
        Task<Response<TDtoResult>> AddAsync(TDto dto);
        Task<Response<TDtoResult>> AddAsync(TDto dto, Expression<Func<T, bool>> predicate);


        Task<Response<AddRangeResult<TDtoResult>>> AddRangeAsync(IEnumerable<TDto> dtos, IEnumerable<Expression<Func<T, bool>>> predicates);



        Task<Response<TDtoResult>> CheckExistsAsync(Expression<Func<T, bool>> predicate, TContext context);
        Task<Response<TDtoResult>> CheckExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
