using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.Linq.Expressions;
using System.Reflection;
using BLL.Repository.Generic.Interface.Remove;

namespace BLL.Repository.Generic.Implementation.Remove
{
    public class RemoveGenericRepo<T, TContext, TDtoResult, TId> : IRemoveGenericRepo<T, TContext, TDtoResult, TId>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;

        public RemoveGenericRepo(TContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<TDtoResult>> DeleteAsync(TId id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);

                if (entity == null)
                {
                    return new Response<TDtoResult>
                    {
                        Success = false,
                        Message = "Entity not found"
                    };
                }

                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<TDtoResult>(entity);
                return new Response<TDtoResult>
                {
                    Success = true,
                    Data = resultDto,
                    Message = "Delete successful"
                };
            }
            catch (Exception ex)
            {
                return new Response<TDtoResult>
                {
                    Success = false,
                    Message = $"Delete failed: {ex.Message}"
                };
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

                var deletedEntities = new List<T>();
                var notFoundEntityIds = new List<TId>();

                foreach (var id in ids)
                {
                    var entity = entities.FirstOrDefault(e =>
                        primaryKeyProperties.All(p => EqualityComparer<TId>.Default.Equals((TId)p.GetValue(e), id)));

                    if (entity != null)
                    {
                        deletedEntities.Add(entity);
                    }
                    else
                    {
                        notFoundEntityIds.Add(id);
                    }
                }

                if (deletedEntities.Count > 0)
                {
                    _context.Set<T>().RemoveRange(deletedEntities);
                    await _context.SaveChangesAsync();
                }

                var deletedEntitiesDtos = deletedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

                var result = new DeleteRangeResult<TDtoResult, TId>
                {
                    DeletedEntities = deletedEntitiesDtos,
                    NotFoundEntityIds = notFoundEntityIds,
                    DeletedEntitiesCount = deletedEntitiesDtos.Count,
                    NotFoundEntitiesCount = notFoundEntityIds.Count
                };

                return new Response<DeleteRangeResult<TDtoResult, TId>>
                {
                    Success = true,
                    Data = result,
                    Message = "Delete range successful"
                };
            }
            catch (Exception ex)
            {
                return new Response<DeleteRangeResult<TDtoResult, TId>>
                {
                    Success = false,
                    Message = $"Delete range failed: {ex.Message}"
                };
            }
        }

        public async Task<Response<TDtoResult>> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entities = await _context.Set<T>().Where(predicate).ToListAsync();

                if (entities == null || !entities.Any())
                {
                    return new Response<TDtoResult>
                    {
                        Success = false,
                        Message = "No entities found for the given predicate"
                    };
                }

                _context.Set<T>().RemoveRange(entities);
                await _context.SaveChangesAsync();

                // If you want to return a single DTO, take the first one; otherwise, consider returning a list
                var resultDto = _mapper.Map<TDtoResult>(entities.FirstOrDefault()); // Assuming TDtoResult maps from T
                return new Response<TDtoResult>
                {
                    Success = true,
                    Data = resultDto,
                    Message = "Delete successful"
                };
            }
            catch (Exception ex)
            {
                return new Response<TDtoResult>
                {
                    Success = false,
                    Message = $"Delete failed: {ex.Message}"
                };
            }
        }

        public async Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
        {
            try
            {
                var entities = new List<T>();

                foreach (var predicate in predicates)
                {
                    var matchingEntities = await _context.Set<T>().Where(predicate).ToListAsync();
                    entities.AddRange(matchingEntities);
                }

                if (entities == null || !entities.Any())
                {
                    return new Response<DeleteRangeResult<TDtoResult, TId>>
                    {
                        Success = false,
                        Message = "No entities found for the given predicates"
                    };
                }

                _context.Set<T>().RemoveRange(entities);
                await _context.SaveChangesAsync();

                var deletedEntitiesDtos = entities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

                var result = new DeleteRangeResult<TDtoResult, TId>
                {
                    DeletedEntities = deletedEntitiesDtos,
                    NotFoundEntityIds = new List<TId>(),
                    DeletedEntitiesCount = deletedEntitiesDtos.Count,
                    NotFoundEntitiesCount = 0
                };

                return new Response<DeleteRangeResult<TDtoResult, TId>>
                {
                    Success = true,
                    Data = result,
                    Message = "Delete range successful"
                };
            }
            catch (Exception ex)
            {
                return new Response<DeleteRangeResult<TDtoResult, TId>>
                {
                    Success = false,
                    Message = $"Delete range failed: {ex.Message}"
                };
            }
        }

        private async Task<List<T>> FindEntitiesByIdsAsync(IEnumerable<TId> ids)
        {
            var entities = new List<T>();

            foreach (var id in ids)
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    entities.Add(entity);
                }
            }

            return entities;
        }

        private IEnumerable<PropertyInfo> GetPrimaryKeyProperties()
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            var primaryKey = entityType.FindPrimaryKey();
            return primaryKey.Properties.Select(p => typeof(T).GetProperty(p.Name));
        }
    }
}















//using AutoMapper;
//using Shared.DTO.Common;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;
//using BLL.Repository.Generic.Interface.Remove;


//namespace BLL.Repository.Generic.Implementation.Remove
//{
//    public class RemoveGenericRepo<T, TContext, TDtoResult, TId> : IRemoveGenericRepo<T, TContext, TDtoResult, TId>
//        where T : class
//        where TContext : DbContext
//    {
//        private readonly TContext _context;
//        private readonly IMapper _mapper;

//        public RemoveGenericRepo(TContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<Response<TDtoResult>> DeleteAsync(TId id)
//        {
//            try
//            {
//                var entity = await _context.Set<T>().FindAsync(id);

//                if (entity == null)
//                {
//                    return new Response<TDtoResult>
//                    {
//                        Success = false,
//                        Message = "Entity not found"
//                    };
//                }

//                _context.Set<T>().Remove(entity);
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

//                var deletedEntities = new List<T>();
//                var notFoundEntityIds = new List<TId>();

//                foreach (var id in ids)
//                {
//                    var entity = entities.FirstOrDefault(e =>
//                        primaryKeyProperties.All(p => EqualityComparer<TId>.Default.Equals((TId)p.GetValue(e), id)));

//                    if (entity != null)
//                    {
//                        deletedEntities.Add(entity);
//                    }
//                    else
//                    {
//                        notFoundEntityIds.Add(id);
//                    }
//                }

//                if (deletedEntities.Count > 0)
//                {
//                    _context.Set<T>().RemoveRange(deletedEntities);
//                    await _context.SaveChangesAsync();
//                }

//                var deletedEntitiesDtos = deletedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

//                var result = new DeleteRangeResult<TDtoResult, TId>
//                {
//                    DeletedEntities = deletedEntitiesDtos,
//                    NotFoundEntityIds = notFoundEntityIds,
//                    DeletedEntitiesCount = deletedEntitiesDtos.Count,
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
//                    Message = $"Delete range failed: {ex.Message}",

//                };
//            }
//        }

//        private async Task<List<T>> FindEntitiesByIdsAsync(IEnumerable<TId> ids)
//        {
//            var entities = new List<T>();

//            foreach (var id in ids)
//            {
//                var entity = await _context.Set<T>().FindAsync(id);
//                if (entity != null)
//                {
//                    entities.Add(entity);
//                }
//            }

//            return entities;
//        }

//        private object[] ExtractPrimaryKeyValues(TId id, IEnumerable<System.Reflection.PropertyInfo> primaryKeyProperties)
//        {
//            var keyValues = new List<object>();
//            foreach (var prop in primaryKeyProperties)
//            {
//                keyValues.Add(prop.GetValue(id));
//            }
//            return keyValues.ToArray();
//        }

//        private IEnumerable<System.Reflection.PropertyInfo> GetPrimaryKeyProperties()
//        {
//            var entityType = _context.Model.FindEntityType(typeof(T));
//            var primaryKey = entityType.FindPrimaryKey();
//            return primaryKey.Properties.Select(p => typeof(T).GetProperty(p.Name));
//        }




//        public async Task<Response<TDtoResult>> DeleteAsync(Expression<Func<T, bool>> predicate)
//        {
//            try
//            {
//                var entities = await _context.Set<T>().Where(predicate).ToListAsync();

//                if (entities == null || !entities.Any())
//                {
//                    return new Response<TDtoResult>
//                    {
//                        Success = false,
//                        Message = "No entities found for the given predicate"
//                    };
//                }

//                _context.Set<T>().RemoveRange(entities);
//                await _context.SaveChangesAsync();

//                var resultDto = _mapper.Map<TDtoResult>(entities.FirstOrDefault()); // Assuming TDtoResult maps from T
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



//        public async Task<Response<DeleteRangeResult<TDtoResult, TId>>> DeleteRangeAsync(IEnumerable<Expression<Func<T, bool>>> predicates)
//        {
//            try
//            {
//                var entities = new List<T>();

//                foreach (var predicate in predicates)
//                {
//                    var matchingEntities = await _context.Set<T>().Where(predicate).ToListAsync();
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

//                _context.Set<T>().RemoveRange(entities);
//                await _context.SaveChangesAsync();

//                var deletedEntitiesDtos = entities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

//                var result = new DeleteRangeResult<TDtoResult, TId>
//                {
//                    DeletedEntities = deletedEntitiesDtos,
//                    NotFoundEntityIds = new List<TId>(),
//                    DeletedEntitiesCount = deletedEntitiesDtos.Count,
//                    NotFoundEntitiesCount = 0
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







//    }
//}
