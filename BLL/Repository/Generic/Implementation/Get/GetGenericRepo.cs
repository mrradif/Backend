using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.Data;
using System.Linq.Expressions;
using DAL.Service.Logger;


namespace BLL.Repository.Generic.Implementation.Get
{
    public class GetGenericRepo<T, TContext, TDtoResult, TId> : IGetGenericRepo<T, TContext, TDtoResult, TId>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;
        private readonly ErrorLogger _errorLogger;

        public GetGenericRepo(
            TContext context, 
            IMapper mapper,
            ErrorLogger errorLogger)
        {
            _context = context;
            _mapper = mapper;
            _errorLogger = errorLogger;
        }


        #region GetAllAsync Methods

        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Set<T>().ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();
                return new Response<IEnumerable<TDtoResult>>(
                    success: dtos.Any(),
                    statusCode: dtos.Any() ? 200 : 404,
                    data: dtos.Any() ? dtos : new List<TDtoResult>(),
                    message: dtos.Any() ? $"{dtos.Count} entities retrieved successfully." : "No entities found."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<IEnumerable<TDtoResult>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entities: {ex.Message}"
                );
            }
        }

        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entities = await _context.Set<T>().Where(predicate).ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();
                return new Response<IEnumerable<TDtoResult>>(
                    success: dtos.Any(),
                    statusCode: dtos.Any() ? 200 : 404,
                    data: dtos.Any() ? dtos : new List<TDtoResult>(),
                    message: dtos.Any() ? $"{dtos.Count} entities retrieved successfully." : "No entities found matching the criteria."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<IEnumerable<TDtoResult>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entities: {ex.Message}"
                );
            }
        }

        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>().Where(predicate);

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                var entities = await query.ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();
                return new Response<IEnumerable<TDtoResult>>(
                    success: dtos.Any(),
                    statusCode: dtos.Any() ? 200 : 404,
                    data: dtos.Any() ? dtos : new List<TDtoResult>(),
                    message: dtos.Any() ? $"{dtos.Count} entities retrieved successfully." : "No entities found matching the criteria."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<IEnumerable<TDtoResult>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entities: {ex.Message}"
                );
            }
        }

        #endregion





        #region GetSingleAsync Methods

        public async Task<Response<TDtoResult>> GetSingleAsync(TId id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                return new Response<TDtoResult>(
                    success: entity != null,
                    statusCode: entity != null ? 200 : 404,
                    data: entity != null ? _mapper.Map<TDtoResult>(entity) : default,
                    message: entity != null ? "Entity retrieved successfully." : "Entity not found."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<TDtoResult>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entity: {ex.Message}"
                );
            }
        }

        public async Task<Response<TDtoResult>> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
                return new Response<TDtoResult>(
                    success: entity != null,
                    statusCode: entity != null ? 200 : 404,
                    data: entity != null ? _mapper.Map<TDtoResult>(entity) : default,
                    message: entity != null ? "Entity retrieved successfully." : "Entity not found."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<TDtoResult>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entity: {ex.Message}"
                );
            }
        }

        #endregion





        #region GetAllAsync Methods With Includes

        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                var entities = await query.ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();
                return new Response<IEnumerable<TDtoResult>>(
                    success: dtos.Any(),
                    statusCode: dtos.Any() ? 200 : 404,
                    data: dtos.Any() ? dtos : new List<TDtoResult>(),
                    message: dtos.Any() ? $"{dtos.Count} entities retrieved successfully." : "No entities found."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<IEnumerable<TDtoResult>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entities: {ex.Message}"
                );
            }
        }

        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                query = predicate != null ? query.Where(predicate) : query;

                var entities = await query.ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();
                return new Response<IEnumerable<TDtoResult>>(
                    success: dtos.Any(),
                    statusCode: dtos.Any() ? 200 : 404,
                    data: dtos.Any() ? dtos : new List<TDtoResult>(),
                    message: dtos.Any() ? $"{dtos.Count} entities retrieved successfully." : "No entities found."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<IEnumerable<TDtoResult>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entities: {ex.Message}"
                );
            }
        }

        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                query = predicate != null ? query.Where(predicate) : query;

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                var entities = await query.ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();
                return new Response<IEnumerable<TDtoResult>>(
                    success: dtos.Any(),
                    statusCode: dtos.Any() ? 200 : 404,
                    data: dtos.Any() ? dtos : new List<TDtoResult>(),
                    message: dtos.Any() ? $"{dtos.Count} entities retrieved successfully." : "No entities found."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<IEnumerable<TDtoResult>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entities: {ex.Message}"
                );
            }
        }

        #endregion




        #region GetSingleAsync With Include Methods

        /// <summary>
        /// Retrieves a single entity that matches the specified predicate, optionally including related data, and maps it to a DTO.
        /// </summary>
        public async Task<Response<TDtoResult>> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                query = predicate != null ? query.Where(predicate) : query;

                var entity = await query.FirstOrDefaultAsync();

                // Use the constructor for success cases
                return new Response<TDtoResult>(
                    success: entity != null,
                    statusCode: entity != null ? 200 : 404,
                    data: entity != null ? _mapper.Map<TDtoResult>(entity) : default,
                    message: entity != null ? "Entity retrieved successfully." : "Entity not found."
                );
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);

                // Use the constructor for failure cases
                return new Response<TDtoResult>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Error retrieving entity: {ex.Message}"
                );
            }
        }

        #endregion





    }
}
