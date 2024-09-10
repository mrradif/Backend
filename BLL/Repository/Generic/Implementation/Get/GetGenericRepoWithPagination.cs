using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Repository.Generic.Interface.Get;
using DAL.Service.Logger;


namespace BLL.Repository.Generic.Implementation.Get
{
    public class GetGenericRepoWithPagination<T, TContext, TDtoResult, TId> : IGetGenericRepoWithPagination<T, TContext, TDtoResult, TId>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;
        private readonly ErrorLogger _errorLogger;

        public GetGenericRepoWithPagination(TContext context, IMapper mapper, ErrorLogger errorLogger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _mapper = mapper;
            _errorLogger = errorLogger;
        }

        #region Public Methods

        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var source = _dbSet.AsQueryable();
                var paginatedList = await CreatePaginatedListAsync(source, pageNumber, pageSize);

                var (message, statusCode) = paginatedList.Items.Any()
                    ? ($"Successfully fetched {paginatedList.TotalItems} records with pagination.", 200)
                    : ("No records found.", 404);

                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, message, statusCode);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                var errorMessage = $"Error retrieving records: {ex.Message}";
                var statusCode = ExceptionHelper.MapExceptionToStatusCode(ex);
                return new PaginationResponse<PaginatedList<TDtoResult>>(errorMessage, statusCode);
            }
        }

        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            try
            {
                var source = _dbSet.Where(predicate).AsQueryable();
                var paginatedList = await CreatePaginatedListAsync(source, pageNumber, pageSize);

                var (message, statusCode) = paginatedList.Items.Any()
                    ? ($"Successfully fetched {paginatedList.TotalItems} records with pagination.", 200)
                    : ("No records found.", 404);

                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, message, statusCode);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                var errorMessage = $"Error retrieving records: {ex.Message}";
                var statusCode = ExceptionHelper.MapExceptionToStatusCode(ex);
                return new PaginationResponse<PaginatedList<TDtoResult>>(errorMessage, statusCode);
            }
        }

        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize)
        {
            try
            {
                var source = orderBy(_dbSet.Where(predicate)).AsQueryable();
                var paginatedList = await CreatePaginatedListAsync(source, pageNumber, pageSize);

                var (message, statusCode) = paginatedList.Items.Any()
                    ? ($"Successfully fetched {paginatedList.TotalItems} records with pagination and ordering.", 200)
                    : ("No records found.", 404);

                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, message, statusCode);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                var errorMessage = $"Error retrieving records: {ex.Message}";
                var statusCode = ExceptionHelper.MapExceptionToStatusCode(ex);
                return new PaginationResponse<PaginatedList<TDtoResult>>(errorMessage, statusCode);
            }
        }

        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                var paginatedList = await CreatePaginatedListAsync(query, pageNumber, pageSize);

                var (message, statusCode) = paginatedList.Items.Any()
                    ? ($"Successfully fetched {paginatedList.TotalItems} records with pagination and includes.", 200)
                    : ("No records found.", 404);

                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, message, statusCode);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                var errorMessage = $"Error retrieving records: {ex.Message}";
                var statusCode = ExceptionHelper.MapExceptionToStatusCode(ex);
                return new PaginationResponse<PaginatedList<TDtoResult>>(errorMessage, statusCode);
            }
        }

        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                var orderedQuery = orderBy(query);
                var paginatedList = await CreatePaginatedListAsync(orderedQuery, pageNumber, pageSize);

                var (message, statusCode) = paginatedList.Items.Any()
                    ? ($"Successfully fetched {paginatedList.TotalItems} records with pagination, ordering, and includes.", 200)
                    : ("No records found.", 404);

                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, message, statusCode);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                var errorMessage = $"Error retrieving records: {ex.Message}";
                var statusCode = ExceptionHelper.MapExceptionToStatusCode(ex);
                return new PaginationResponse<PaginatedList<TDtoResult>>(errorMessage, statusCode);
            }
        }

        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet.Where(predicate);
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                var paginatedList = await CreatePaginatedListAsync(query, pageNumber, pageSize);

                var (message, statusCode) = paginatedList.Items.Any()
                    ? ($"Successfully fetched {paginatedList.TotalItems} records with pagination and includes.", 200)
                    : ("No records found.", 404);

                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, message, statusCode);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                var errorMessage = $"Error retrieving records: {ex.Message}";
                var statusCode = ExceptionHelper.MapExceptionToStatusCode(ex);
                return new PaginationResponse<PaginatedList<TDtoResult>>(errorMessage, statusCode);
            }
        }

        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet.Where(predicate);
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                var orderedQuery = orderBy(query);
                var paginatedList = await CreatePaginatedListAsync(orderedQuery, pageNumber, pageSize);

                var (message, statusCode) = paginatedList.Items.Any()
                    ? ($"Successfully fetched {paginatedList.TotalItems} records with pagination, ordering, and includes.", 200)
                    : ("No records found.", 404);

                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, message, statusCode);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                var errorMessage = $"Error retrieving records: {ex.Message}";
                var statusCode = ExceptionHelper.MapExceptionToStatusCode(ex);
                return new PaginationResponse<PaginatedList<TDtoResult>>(errorMessage, statusCode);
            }
        }

        #endregion

        #region Private Methods

        private async Task<PaginatedList<TDtoResult>> CreatePaginatedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ProjectTo<TDtoResult>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return new PaginatedList<TDtoResult>(items, pageNumber, pageSize, count);
        }

        #endregion
    }
}











//using Microsoft.EntityFrameworkCore;
//using Shared.DTO.Common;
//using Shared.Helper.Pagination;
//using System.Linq.Expressions;
//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using BLL.Repository.Generic.Interface.Get;
//using DAL.Service.Logger;

//namespace BLL.Repository.Generic.Implementation.Get
//{
//    public class GetGenericRepoWithPagination<T, TContext, TDtoResult, TId> : IGetGenericRepoWithPagination<T, TContext, TDtoResult, TId>
//        where T : class
//        where TContext : DbContext
//    {
//        private readonly TContext _context;
//        private readonly DbSet<T> _dbSet;
//        private readonly IMapper _mapper;
//        private readonly ErrorLogger _errorLogger; 

//        public GetGenericRepoWithPagination(TContext context, IMapper mapper, ErrorLogger errorLogger)
//        {
//            _context = context;
//            _dbSet = _context.Set<T>();
//            _mapper = mapper;
//            _errorLogger = errorLogger;
//        }

//        //public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize)
//        //{
//        //    try
//        //    {
//        //        var source = _dbSet.AsQueryable();
//        //        var paginatedList = await CreatePaginatedListAsync(source, pageNumber, pageSize);
//        //        var successMessage = $"Successfully fetched {paginatedList.TotalItems} records with pagination.";
//        //        return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, successMessage);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        await _errorLogger.LogErrorAsync(ex); 

//        //        return new PaginationResponse<PaginatedList<TDtoResult>>(ex.Message);
//        //    }
//        //}

//        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize)
//        {
//            try
//            {
//                var source = _dbSet.AsQueryable();
//                var paginatedList = await CreatePaginatedListAsync(source, pageNumber, pageSize);

//                var (message, statusCode) = paginatedList.Items.Any()
//                    ? ($"Successfully fetched {paginatedList.TotalItems} records with pagination.", 200) 
//                    : ("No records found.", 404);

//                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, message, statusCode);
//            }
//            catch (Exception ex)
//            {
//                await _errorLogger.LogErrorAsync(ex);
//                var errorMessage = $"Error retrieving records: {ex.Message}";
//                var statusCode = ExceptionHelper.MapExceptionToStatusCode(ex); 
//                return new PaginationResponse<PaginatedList<TDtoResult>>(errorMessage, statusCode);
//            }
//        }







//        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
//        {
//            try
//            {
//                var source = _dbSet.Where(predicate).AsQueryable();
//                var paginatedList = await CreatePaginatedListAsync(source, pageNumber, pageSize);
//                var successMessage = $"Successfully fetched {paginatedList.TotalItems} records with pagination.";
//                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, successMessage);
//            }
//            catch (Exception ex)
//            {
//                await _errorLogger.LogErrorAsync(ex); // Log the error
//                return new PaginationResponse<PaginatedList<TDtoResult>>(ex.Message);
//            }
//        }



//        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize)
//        {
//            try
//            {
//                var source = orderBy(_dbSet.Where(predicate)).AsQueryable();
//                var paginatedList = await CreatePaginatedListAsync(source, pageNumber, pageSize);
//                var successMessage = $"Successfully fetched {paginatedList.TotalItems} records with pagination and ordering.";
//                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, successMessage);
//            }
//            catch (Exception ex)
//            {
//                await _errorLogger.LogErrorAsync(ex); // Log the error
//                return new PaginationResponse<PaginatedList<TDtoResult>>(ex.Message);
//            }
//        }

//        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
//        {
//            try
//            {
//                IQueryable<T> query = _dbSet;
//                foreach (var include in includes)
//                {
//                    query = query.Include(include);
//                }

//                var paginatedList = await CreatePaginatedListAsync(query, pageNumber, pageSize);
//                var successMessage = $"Successfully fetched {paginatedList.TotalItems} records with pagination and includes.";
//                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, successMessage);
//            }
//            catch (Exception ex)
//            {
//                await _errorLogger.LogErrorAsync(ex); // Log the error
//                return new PaginationResponse<PaginatedList<TDtoResult>>(ex.Message);
//            }
//        }

//        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params Expression<Func<T, object>>[] includes)
//        {
//            try
//            {
//                IQueryable<T> query = _dbSet;
//                foreach (var include in includes)
//                {
//                    query = query.Include(include);
//                }
//                var orderedQuery = orderBy(query);
//                var paginatedList = await CreatePaginatedListAsync(orderedQuery, pageNumber, pageSize);
//                var successMessage = $"Successfully fetched {paginatedList.TotalItems} records with pagination, ordering, and includes.";
//                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, successMessage);
//            }
//            catch (Exception ex)
//            {
//                await _errorLogger.LogErrorAsync(ex); // Log the error
//                return new PaginationResponse<PaginatedList<TDtoResult>>(ex.Message);
//            }
//        }

//        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
//        {
//            try
//            {
//                IQueryable<T> query = _dbSet.Where(predicate);
//                foreach (var include in includes)
//                {
//                    query = query.Include(include);
//                }
//                var paginatedList = await CreatePaginatedListAsync(query, pageNumber, pageSize);
//                var successMessage = $"Successfully fetched {paginatedList.TotalItems} records with pagination and includes.";
//                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, successMessage);
//            }
//            catch (Exception ex)
//            {
//                await _errorLogger.LogErrorAsync(ex); // Log the error
//                return new PaginationResponse<PaginatedList<TDtoResult>>(ex.Message);
//            }
//        }

//        public async Task<PaginationResponse<PaginatedList<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
//        {
//            try
//            {
//                IQueryable<T> query = _dbSet.Where(predicate);
//                foreach (var include in includes)
//                {
//                    query = query.Include(include);
//                }
//                var orderedQuery = orderBy(query);
//                var paginatedList = await CreatePaginatedListAsync(orderedQuery, pageNumber, pageSize);
//                var successMessage = $"Successfully fetched {paginatedList.TotalItems} records with pagination, ordering, and includes.";
//                return new PaginationResponse<PaginatedList<TDtoResult>>(paginatedList, successMessage);
//            }
//            catch (Exception ex)
//            {
//                await _errorLogger.LogErrorAsync(ex); // Log the error
//                return new PaginationResponse<PaginatedList<TDtoResult>>(ex.Message);
//            }
//        }

//        private async Task<PaginatedList<TDtoResult>> CreatePaginatedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
//        {
//            var count = await source.CountAsync();
//            var items = await source.Skip((pageNumber - 1) * pageSize)
//                                    .Take(pageSize)
//                                    .ProjectTo<TDtoResult>(_mapper.ConfigurationProvider)
//                                    .ToListAsync();
//            return new PaginatedList<TDtoResult>(items, pageNumber, pageSize, count);
//        }
//    }
//}
