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
                if (entity != null)
                {
                    var result = _mapper.Map<TDtoResult>(entity);
                    return new Response<TDtoResult>
                    {
                        Success = true,
                        Message = "Entity exists",
                        Data = result
                    };
                }
                return new Response<TDtoResult>
                {
                    Success = false,
                    Message = "Entity does not exist",
                    Data = default
                };
            }
            catch (Exception ex)
            {
                return new Response<TDtoResult>
                {
                    Success = false,
                    Message = $"Check exists failed: {ex.Message}",
                    Data = default
                };
            }
        }


        public async Task<Response<TDtoResult>> CheckExistsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = await _dbSet.FirstOrDefaultAsync(predicate);
                if (entity != null)
                {
                    var result = _mapper.Map<TDtoResult>(entity);
                    return new Response<TDtoResult>
                    {
                        Success = true,
                        Message = "Entity exists",
                        Data = result
                    };
                }
                return new Response<TDtoResult>
                {
                    Success = false,
                    Message = "Entity does not exist",
                    Data = default
                };
            }
            catch (Exception ex)
            {
                return new Response<TDtoResult>
                {
                    Success = false,
                    Message = $"Check exists failed: {ex.Message}",
                    Data = default
                };
            }
        }
    }
}
