using AutoMapper;
using BLL.Repository.Generic.Interface.Put;
using DAL.Service.Logger;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.Linq.Expressions;

namespace BLL.Repository.Generic.Implementation.Put
{
    public class PutGenericRepo<T, TContext, TDto, TDtoResult> : IPutGenericRepo<T, TContext, TDto, TDtoResult>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<T> _dbSet;
        private readonly ErrorLogger _errorLogger;

        public PutGenericRepo(TContext context, IMapper mapper, ErrorLogger errorLogger)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<T>();
            _errorLogger = errorLogger;
        }

        public async Task<UpdateResponse<TDtoResult>> UpdateAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<T>(dto);
                var predicate = BuildPredicate(entity);
                var checkExistsResponse = await CheckExistsAsync(predicate);

                if (checkExistsResponse.Success && checkExistsResponse.Data != null)
                {
                    var originalEntity = checkExistsResponse.Data;

                    // Update properties based on DTO using UpdatePropertyHandler
                    UpdatePropertyHandler.UpdateProperties(originalEntity, entity);

                    // Handle audit properties
                    AuditableEntityHandler.SetProperties(originalEntity, "Update");

                    // Save changes
                    await _context.SaveChangesAsync();

                    var updatedEntityDto = _mapper.Map<TDtoResult>(originalEntity);
                    return new UpdateResponse<TDtoResult>
                    {
                        Success = true,
                        Message = "Entity updated successfully",
                        Data = updatedEntityDto
                    };
                }

                return new UpdateResponse<TDtoResult>
                {
                    Success = false,
                    Message = "Entity not found",
                    Data = default
                };
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new UpdateResponse<TDtoResult>
                {
                    Success = false,
                    Message = $"Update operation failed: {ex.Message}",
                    Data = default
                };
            }
        }

        public async Task<UpdateResponse<UpdateRangeResult<TDtoResult>>> UpdateRangeAsync(IEnumerable<TDto> dtos, IEnumerable<Expression<Func<T, bool>>> predicates)
        {
            try
            {
                var entities = dtos.Select(dto => _mapper.Map<T>(dto)).ToList();
                var updatedEntities = new List<T>();
                var notFoundEntities = new List<T>();

                if (dtos.Count() != predicates.Count())
                {
                    throw new ArgumentException("Number of predicates must match the number of DTOs.");
                }

                for (int i = 0; i < dtos.Count(); i++)
                {
                    var entity = entities[i];
                    var predicate = predicates.ElementAt(i);
                    var checkExistsResponse = await CheckExistsAsync(predicate);

                    if (checkExistsResponse.Success && checkExistsResponse.Data != null)
                    {
                        var originalEntity = checkExistsResponse.Data;

                        // Update properties based on DTO using UpdatePropertyHandler
                        UpdatePropertyHandler.UpdateProperties(originalEntity, entity);

                        // Handle audit properties
                        AuditableEntityHandler.SetProperties(originalEntity, "Update");

                        // Save changes
                        await _context.SaveChangesAsync();

                        updatedEntities.Add(originalEntity);
                    }
                    else
                    {
                        notFoundEntities.Add(entity);
                    }
                }

                var updatedEntitiesDtos = updatedEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();
                var notFoundEntitiesDtos = notFoundEntities.Select(e => _mapper.Map<TDtoResult>(e)).ToList();

                var result = new UpdateRangeResult<TDtoResult>
                {
                    UpdatedEntities = updatedEntitiesDtos,
                    NotUpdatedEntities = notFoundEntitiesDtos,
                    UpdatedEntitiesCount = updatedEntitiesDtos.Count,
                    NotUpdatedEntitiesCount = notFoundEntitiesDtos.Count
                };

                return new UpdateResponse<UpdateRangeResult<TDtoResult>>
                {
                    Success = true,
                    Message = $"Update operation completed. Updated entities: {updatedEntitiesDtos.Count}, Not found entities: {notFoundEntitiesDtos.Count}",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new UpdateResponse<UpdateRangeResult<TDtoResult>>
                {
                    Success = false,
                    Message = $"Update operation failed: {ex.Message}",
                    Data = default
                };
            }
        }


        private Expression<Func<T, bool>> BuildPredicate(T entity)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            if (entityType == null)
                throw new Exception("Entity type not found in the context model.");

            var primaryKey = entityType.FindPrimaryKey();
            if (primaryKey == null)
                throw new Exception("Primary key not found.");

            var primaryKeyProperties = primaryKey.Properties;
            if (primaryKeyProperties == null || !primaryKeyProperties.Any())
                throw new Exception("Primary key properties not found.");

            var parameter = Expression.Parameter(typeof(T), "e");
            Expression predicate = null;

            foreach (var primaryKeyProperty in primaryKeyProperties)
            {
                var property = Expression.Property(parameter, primaryKeyProperty.Name);
                var value = Expression.Constant(entity.GetType().GetProperty(primaryKeyProperty.Name)?.GetValue(entity));
                var equal = Expression.Equal(property, value);

                predicate = predicate == null ? equal : Expression.AndAlso(predicate, equal);
            }

            return Expression.Lambda<Func<T, bool>>(predicate, parameter);
        }


        public async Task<Response<T>> CheckExistsAsync(Expression<Func<T, bool>> predicate, TContext context)
        {
            try
            {
                var entity = await context.Set<T>().FirstOrDefaultAsync(predicate);
                if (entity != null)
                {
                    return new Response<T>
                    {
                        Success = true,
                        Message = "Entity exists",
                        Data = entity
                    };
                }
                return new Response<T>
                {
                    Success = false,
                    Message = "Entity does not exist",
                    Data = default
                };
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<T>
                {
                    Success = false,
                    Message = $"Check exists failed: {ex.Message}",
                    Data = default
                };
            }
        }

        public async Task<Response<T>> CheckExistsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = await _dbSet.FirstOrDefaultAsync(predicate);
                if (entity != null)
                {
                    return new Response<T>
                    {
                        Success = true,
                        Message = "Entity exists",
                        Data = entity
                    };
                }
                return new Response<T>
                {
                    Success = false,
                    Message = "Entity does not exist",
                    Data = default
                };
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<T>
                {
                    Success = false,
                    Message = $"Check exists failed: {ex.Message}",
                    Data = default
                };
            }
        }


    }
}
