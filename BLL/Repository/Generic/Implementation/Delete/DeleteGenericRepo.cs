using AutoMapper;
using BLL.Repository.Generic.Interface.Delete;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace BLL.Repository.Generic.Implementation.Delete
{
    public class DeleteGenericRepo<T, TContext, TDtoResult, TId> : IDeleteGenericRepo<T, TContext, TDtoResult, TId>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

        public DeleteGenericRepo(TContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _mapper = mapper;
        }

        public async Task<Response<TDtoResult>> DeleteAsync(TId id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);

                if (entity == null)
                {
                    return new Response<TDtoResult>(
                        success: false,
                        statusCode: 404,
                        message: "Entity not found"
                    );
                }

                var isDeletedProperty = typeof(T).GetProperty("isDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (isDeletedProperty == null)
                {
                    return new Response<TDtoResult>(
                        success: false,
                        statusCode: 400,
                        message: "Entity does not have an 'isDeleted' property"
                    );
                }

                var isDeletedValue = (bool?)isDeletedProperty.GetValue(entity);
                if (isDeletedValue == true)
                {
                    return new Response<TDtoResult>(
                        success: false,
                        statusCode: 400,
                        message: "Entity already deleted"
                    );
                }

                isDeletedProperty.SetValue(entity, true);

                AuditableEntityHandler.SetProperties(entity, "Delete");

                _dbSet.Update(entity);
                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<TDtoResult>(entity);
                return new Response<TDtoResult>(
                    success: true,
                    statusCode: 200,
                    data: resultDto,
                    message: "Delete successful"
                );
            }
            catch (Exception ex)
            {
                return new Response<TDtoResult>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Delete failed: {ex.Message}"
                );
            }
        }

        public async Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<TId> ids)
        {
            try
            {
                var primaryKeyProperties = GetPrimaryKeyProperties();
                if (!primaryKeyProperties.Any())
                {
                    throw new InvalidOperationException("Primary key properties not found.");
                }

                var entities = await FindEntitiesByIdsAsync(ids);

                var updatedEntities = new List<T>();
                var notFoundEntityIds = new List<TId>();

                var isDeletedProperty = typeof(T).GetProperty("isDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (isDeletedProperty == null)
                {
                    throw new InvalidOperationException("Entity does not have an 'isDeleted' property.");
                }

                foreach (var id in ids)
                {
                    var entity = entities.FirstOrDefault(e =>
                        primaryKeyProperties.All(p => EqualityComparer<TId>.Default.Equals((TId)p.GetValue(e), id)));

                    if (entity != null)
                    {
                        var isDeletedValue = (bool?)isDeletedProperty.GetValue(entity);
                        if (isDeletedValue == true)
                        {
                            notFoundEntityIds.Add(id);
                        }
                        else
                        {
                            isDeletedProperty.SetValue(entity, true);
                            AuditableEntityHandler.SetProperties(entity, "Delete");
                            updatedEntities.Add(entity);
                        }
                    }
                    else
                    {
                        notFoundEntityIds.Add(id);
                    }
                }

                if (updatedEntities.Count > 0)
                {
                    _dbSet.UpdateRange(updatedEntities);
                    await _context.SaveChangesAsync();
                }

                var updatedEntitiesDtos = updatedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

                var result = new DeleteRangeResult<TDtoResult, TId>(
                    deletedEntities: updatedEntitiesDtos,
                    deletedEntitiesCount: updatedEntitiesDtos.Count
                );

                if (notFoundEntityIds.Any())
                {
                    return new Response<DeleteRangeResult<TDtoResult, TId>>(
                        success: false,
                        statusCode: 404,
                        data: new DeleteRangeResult<TDtoResult, TId>(
                            notFoundEntityIds,
                            notFoundEntityIds.Count
                        ),
                        message: "Some entities not found"
                    );
                }

                return new Response<DeleteRangeResult<TDtoResult, TId>>(
                    success: true,
                    statusCode: 200,
                    data: result,
                    message: "Delete range successful"
                );
            }
            catch (Exception ex)
            {
                return new Response<DeleteRangeResult<TDtoResult, TId>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Delete range failed: {ex.Message}"
                );
            }
        }

        public async Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entities = await _dbSet.Where(predicate).ToListAsync();

                if (entities == null || !entities.Any())
                {
                    return new Response<DeleteRangeResult<TDtoResult, TId>>(
                        success: false,
                        statusCode: 404,
                        data: new DeleteRangeResult<TDtoResult, TId>(
                            new List<TId>(),
                            0
                        ),
                        message: "No entities found for the given predicate"
                    );
                }

                var updatedEntities = new List<T>();
                var notFoundEntityIds = new List<TId>();

                foreach (var entity in entities)
                {
                    var isDeletedProperty = typeof(T).GetProperty("isDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (isDeletedProperty != null)
                    {
                        var isDeletedValue = (bool?)isDeletedProperty.GetValue(entity);
                        if (isDeletedValue != true)
                        {
                            isDeletedProperty.SetValue(entity, true);

                            AuditableEntityHandler.SetProperties(entity, "Delete");

                            updatedEntities.Add(entity);
                        }
                        else
                        {
                            var primaryKeyValue = GetPrimaryKeyValue(entity);
                            notFoundEntityIds.Add((TId)primaryKeyValue);
                        }
                    }
                }

                if (updatedEntities.Count > 0)
                {
                    _dbSet.UpdateRange(updatedEntities);
                    await _context.SaveChangesAsync();
                }

                var updatedEntitiesDtos = updatedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

                var result = new DeleteRangeResult<TDtoResult, TId>(
                    deletedEntities: updatedEntitiesDtos,
                    deletedEntitiesCount: updatedEntitiesDtos.Count
                );

                return new Response<DeleteRangeResult<TDtoResult, TId>>(
                    success: true,
                    statusCode: 200,
                    data: result,
                    message: "Delete successful"
                );
            }
            catch (Exception ex)
            {
                return new Response<DeleteRangeResult<TDtoResult, TId>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Delete failed: {ex.Message}"
                );
            }
        }

        public async Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        {
            try
            {
                var entities = new List<T>();

                foreach (var predicate in predicates)
                {
                    var matchingEntities = await _dbSet.Where(predicate).ToListAsync();
                    entities.AddRange(matchingEntities);
                }

                if (entities == null || !entities.Any())
                {
                    return new Response<DeleteRangeResult<TDtoResult, TId>>(
                        success: false,
                        statusCode: 404,
                        data: new DeleteRangeResult<TDtoResult, TId>(
                            new List<TId>(),
                            0
                        ),
                        message: "No entities found for the given predicates"
                    );
                }

                var updatedEntities = new List<T>();
                var notFoundEntityIds = new List<TId>();

                foreach (var entity in entities)
                {
                    var isDeletedProperty = typeof(T).GetProperty("isDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (isDeletedProperty != null)
                    {
                        var isDeletedValue = (bool?)isDeletedProperty.GetValue(entity);
                        if (isDeletedValue != true)
                        {
                            isDeletedProperty.SetValue(entity, true);

                            AuditableEntityHandler.SetProperties(entity, "Delete");

                            updatedEntities.Add(entity);
                        }
                        else
                        {
                            var primaryKeyValue = GetPrimaryKeyValue(entity);
                            notFoundEntityIds.Add((TId)primaryKeyValue);
                        }
                    }
                }

                if (updatedEntities.Count > 0)
                {
                    _dbSet.UpdateRange(updatedEntities);
                    await _context.SaveChangesAsync();
                }

                var updatedEntitiesDtos = updatedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

                var result = new DeleteRangeResult<TDtoResult, TId>(
                    deletedEntities: updatedEntitiesDtos,
                    deletedEntitiesCount: updatedEntitiesDtos.Count
                );

                return new Response<DeleteRangeResult<TDtoResult, TId>>(
                    success: true,
                    statusCode: 200,
                    data: result,
                    message: "Delete range successful"
                );
            }
            catch (Exception ex)
            {
                return new Response<DeleteRangeResult<TDtoResult, TId>>(
                    success: false,
                    statusCode: ExceptionHelper.MapExceptionToStatusCode(ex),
                    message: $"Delete range failed: {ex.Message}"
                );
            }
        }

        private PropertyInfo[] GetPrimaryKeyProperties()
        {
            return typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Any()).ToArray();
        }

        private object GetPrimaryKeyValue(T entity)
        {
            var keyProperty = GetPrimaryKeyProperties().FirstOrDefault();
            return keyProperty?.GetValue(entity);
        }

        private async Task<List<T>> FindEntitiesByIdsAsync(IEnumerable<TId> ids)
        {
            var primaryKeyProperties = GetPrimaryKeyProperties();
            if (!primaryKeyProperties.Any())
            {
                throw new InvalidOperationException("Primary key properties not found.");
            }

            var entities = new List<T>();
            foreach (var id in ids)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    entities.Add(entity);
                }
            }
            return entities;
        }
    }
}













//using AutoMapper;
//using BLL.Repository.Generic.Interface.Delete;
//using Microsoft.EntityFrameworkCore;
//using Shared.DTO.Common;
//using System.Linq.Expressions;
//using System.Reflection;


//namespace BLL.Repository.Generic.Implementation.Delete
//{
//    public class DeleteGenericRepo<T, TContext, TDtoResult, TId> : IDeleteGenericRepo<T, TContext, TDtoResult, TId>
//        where T : class
//        where TContext : DbContext
//    {
//        private readonly TContext _context;
//        private readonly DbSet<T> _dbSet;
//        private readonly IMapper _mapper;

//        public DeleteGenericRepo(TContext context, IMapper mapper)
//        {
//            _context = context;
//            _dbSet = _context.Set<T>();
//            _mapper = mapper;
//        }

//        public async Task<Response<TDtoResult>> DeleteAsync(TId id)
//        {
//            try
//            {
//                var entity = await _dbSet.FindAsync(id);

//                if (entity == null)
//                {
//                    return new Response<TDtoResult>
//                    {
//                        Success = false,
//                        Message = "Entity not found"
//                    };
//                }

//                var isDeletedProperty = typeof(T).GetProperty("isDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

//                if (isDeletedProperty == null)
//                {
//                    return new Response<TDtoResult>
//                    {
//                        Success = false,
//                        Message = "Entity does not have an 'isDeleted' property"
//                    };
//                }

//                var isDeletedValue = (bool?)isDeletedProperty.GetValue(entity);
//                if (isDeletedValue == true)
//                {
//                    return new Response<TDtoResult>
//                    {
//                        Success = false,
//                        Message = "Entity already deleted"
//                    };
//                }

//                isDeletedProperty.SetValue(entity, true);

//                AuditableEntityHandler.SetProperties(entity, "Delete");

//                _dbSet.Update(entity);
//                await _context.SaveChangesAsync();

//                var resultDto = _mapper.Map<TDtoResult>(entity);
//                return new Response<TDtoResult>
//                {
//                    Success = true,
//                    Data = resultDto,
//                    Message = "Delete successful"
//                };
//            }
//            catch (Exception ex)
//            {
//                return new Response<TDtoResult>
//                {
//                    Success = false,
//                    Message = $"Delete failed: {ex.Message}"
//                };
//            }
//        }

//        public async Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<TId> ids)
//        {
//            try
//            {
//                var primaryKeyProperties = GetPrimaryKeyProperties();
//                if (!primaryKeyProperties.Any())
//                {
//                    throw new InvalidOperationException("Primary key properties not found.");
//                }

//                var entities = await FindEntitiesByIdsAsync(ids);

//                var updatedEntities = new List<T>();
//                var notFoundEntityIds = new List<TId>();

//                var isDeletedProperty = typeof(T).GetProperty("isDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
//                if (isDeletedProperty == null)
//                {
//                    throw new InvalidOperationException("Entity does not have an 'isDeleted' property.");
//                }

//                foreach (var id in ids)
//                {
//                    var entity = entities.FirstOrDefault(e =>
//                        primaryKeyProperties.All(p => EqualityComparer<TId>.Default.Equals((TId)p.GetValue(e), id)));

//                    if (entity != null)
//                    {
//                        var isDeletedValue = (bool?)isDeletedProperty.GetValue(entity);
//                        if (isDeletedValue == true)
//                        {
//                            notFoundEntityIds.Add(id);
//                        }
//                        else
//                        {
//                            isDeletedProperty.SetValue(entity, true);
//                            AuditableEntityHandler.SetProperties(entity, "Delete");
//                            updatedEntities.Add(entity);
//                        }
//                    }
//                    else
//                    {
//                        notFoundEntityIds.Add(id);
//                    }
//                }

//                if (updatedEntities.Count > 0)
//                {
//                    _dbSet.UpdateRange(updatedEntities);
//                    await _context.SaveChangesAsync();
//                }

//                var updatedEntitiesDtos = updatedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

//                var result = new DeleteRangeResult<TDtoResult, TId>
//                {
//                    DeletedEntities = updatedEntitiesDtos,
//                    NotFoundEntityIds = notFoundEntityIds,
//                    DeletedEntitiesCount = updatedEntitiesDtos.Count,
//                    NotFoundEntitiesCount = notFoundEntityIds.Count
//                };

//                return new Response<DeleteRangeResult<TDtoResult, TId>>
//                {
//                    Success = true,
//                    Data = result,
//                    Message = "Delete range successful"
//                };
//            }
//            catch (Exception ex)
//            {
//                return new Response<DeleteRangeResult<TDtoResult, TId>>
//                {
//                    Success = false,
//                    Message = $"Delete range failed: {ex.Message}"
//                };
//            }
//        }



//        public async Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteAsync(Expression<Func<T, bool>> predicate)
//        {
//            try
//            {
//                var entities = await _dbSet.Where(predicate).ToListAsync();

//                if (entities == null || !entities.Any())
//                {
//                    return new Response<DeleteRangeResult<TDtoResult, TId>>
//                    {
//                        Success = false,
//                        Message = "No entities found for the given predicate"
//                    };
//                }

//                var updatedEntities = new List<T>();
//                var notFoundEntityIds = new List<TId>();

//                foreach (var entity in entities)
//                {
//                    var isDeletedProperty = typeof(T).GetProperty("isDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
//                    if (isDeletedProperty != null)
//                    {
//                        var isDeletedValue = (bool?)isDeletedProperty.GetValue(entity);
//                        if (isDeletedValue != true)
//                        {
//                            isDeletedProperty.SetValue(entity, true);

//                            AuditableEntityHandler.SetProperties(entity, "Delete");

//                            updatedEntities.Add(entity);
//                        }
//                        else
//                        {
//                            var primaryKeyValue = GetPrimaryKeyValue(entity);
//                            notFoundEntityIds.Add((TId)primaryKeyValue);
//                        }
//                    }
//                }

//                if (updatedEntities.Count > 0)
//                {
//                    _dbSet.UpdateRange(updatedEntities);
//                    await _context.SaveChangesAsync();
//                }

//                var updatedEntitiesDtos = updatedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

//                var result = new DeleteRangeResult<TDtoResult, TId>
//                {
//                    DeletedEntities = updatedEntitiesDtos,
//                    NotFoundEntityIds = notFoundEntityIds,
//                    DeletedEntitiesCount = updatedEntitiesDtos.Count,
//                    NotFoundEntitiesCount = notFoundEntityIds.Count
//                };

//                return new Response<DeleteRangeResult<TDtoResult, TId>>
//                {
//                    Success = true,
//                    Data = result,
//                    Message = "Delete successful"
//                };
//            }
//            catch (Exception ex)
//            {
//                return new Response<DeleteRangeResult<TDtoResult, TId>>
//                {
//                    Success = false,
//                    Message = $"Delete failed: {ex.Message}"
//                };
//            }
//        }



//        public async Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
//        {
//            try
//            {
//                var entities = new List<T>();

//                foreach (var predicate in predicates)
//                {
//                    var matchingEntities = await _dbSet.Where(predicate).ToListAsync();
//                    entities.AddRange(matchingEntities);
//                }

//                if (entities == null || !entities.Any())
//                {
//                    return new Response<DeleteRangeResult<TDtoResult, TId>>
//                    {
//                        Success = false,
//                        Message = "No entities found for the given predicates"
//                    };
//                }

//                var updatedEntities = new List<T>();
//                var notFoundEntityIds = new List<TId>();

//                foreach (var entity in entities)
//                {
//                    var isDeletedProperty = typeof(T).GetProperty("isDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
//                    if (isDeletedProperty != null)
//                    {
//                        var isDeletedValue = (bool?)isDeletedProperty.GetValue(entity);
//                        if (isDeletedValue != true)
//                        {
//                            isDeletedProperty.SetValue(entity, true);

//                            AuditableEntityHandler.SetProperties(entity, "Delete");

//                            updatedEntities.Add(entity);
//                        }
//                        else
//                        {
//                            var primaryKeyValue = GetPrimaryKeyValue(entity);
//                            notFoundEntityIds.Add((TId)primaryKeyValue);
//                        }
//                    }
//                }

//                if (updatedEntities.Count > 0)
//                {
//                    _dbSet.UpdateRange(updatedEntities);
//                    await _context.SaveChangesAsync();
//                }

//                var updatedEntitiesDtos = updatedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

//                var result = new DeleteRangeResult<TDtoResult, TId>
//                {
//                    DeletedEntities = updatedEntitiesDtos,
//                    NotFoundEntityIds = notFoundEntityIds,
//                    DeletedEntitiesCount = updatedEntitiesDtos.Count,
//                    NotFoundEntitiesCount = notFoundEntityIds.Count
//                };

//                return new Response<DeleteRangeResult<TDtoResult, TId>>
//                {
//                    Success = true,
//                    Data = result,
//                    Message = "Delete range successful"
//                };
//            }
//            catch (Exception ex)
//            {
//                return new Response<DeleteRangeResult<TDtoResult, TId>>
//                {
//                    Success = false,
//                    Message = $"Delete range failed: {ex.Message}"
//                };
//            }
//        }




//        private async Task<List<T>> FindEntitiesByIdsAsync(IEnumerable<TId> ids)
//        {
//            var entities = new List<T>();

//            foreach (var id in ids)
//            {
//                var entity = await _dbSet.FindAsync(id);
//                if (entity != null)
//                {
//                    entities.Add(entity);
//                }
//            }

//            return entities;
//        }

//        private object GetPrimaryKeyValue(T entity)
//        {
//            var primaryKeyProperties = GetPrimaryKeyProperties();
//            var keyValues = new List<object>();
//            foreach (var prop in primaryKeyProperties)
//            {
//                keyValues.Add(prop.GetValue(entity));
//            }
//            return keyValues.Count == 1 ? keyValues.First() : keyValues;
//        }

//        private IEnumerable<PropertyInfo> GetPrimaryKeyProperties()
//        {
//            var entityType = _context.Model.FindEntityType(typeof(T));
//            var primaryKey = entityType.FindPrimaryKey();
//            return primaryKey.Properties.Select(p => typeof(T).GetProperty(p.Name));
//        }
//    }
//}
