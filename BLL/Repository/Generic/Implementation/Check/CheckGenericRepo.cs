using AutoMapper;
using BLL.Repository.Generic.Interface.Check;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.Linq.Expressions;

namespace BLL.Repository.Generic.Implementation.Check
{
    public class CheckGenericRepo<T, TContext, TDto, TDtoResult> : ICheckGenericRepo<T, TContext, TDto, TDtoResult>
        where T : class
        where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

        public CheckGenericRepo(TContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _mapper = mapper;
        }

        public async Task<Response<TDtoResult>> CheckExistsAsync(Expression<Func<T, bool>> predicate, TContext context)
        {
            try
            {
                DbSet<T> dbSet = context.Set<T>();
                var entity = await dbSet.FirstOrDefaultAsync(predicate);
                return new Response<TDtoResult>(
                    success: entity != null,
                    statusCode: entity != null ? 200 : 404,
                    data: entity != null ? _mapper.Map<TDtoResult>(entity) : default,
                    message: entity != null ? "Entity exists" : "Entity does not exist"
                );
            }
            catch (Exception ex)
            {
                return new Response<TDtoResult>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Check exists failed: {ex.Message}"
                );
            }
        }

        public async Task<Response<TDtoResult>> CheckExistsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = await _dbSet.FirstOrDefaultAsync(predicate);
                return new Response<TDtoResult>(
                    success: entity != null,
                    statusCode: entity != null ? 200 : 404,
                    data: entity != null ? _mapper.Map<TDtoResult>(entity) : default,
                    message: entity != null ? "Entity exists" : "Entity does not exist"
                );
            }
            catch (Exception ex)
            {
                return new Response<TDtoResult>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Check exists failed: {ex.Message}"
                );
            }
        }
    }
}
