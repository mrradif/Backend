using AutoMapper;
using BLL.Repository.Generic.Interface.Post;
using DAL.Context.Control_Panel;
using DAL.Service.Logger;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System.Linq.Expressions;

namespace BLL.Repository.Generic.Implementation.Post
{
    public class PostGenericRepo<T, TContext, TDto, TDtoResult> : IPostGenericRepo<T, TContext, TDto, TDtoResult>
        where T : class
        where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;
        private readonly ErrorLogger _errorLogger;

        public PostGenericRepo(TContext context, IMapper mapper, ErrorLogger errorLogger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _mapper = mapper;
            _errorLogger = errorLogger;
        }

        public async Task<Response<TDtoResult>> AddAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<T>(dto);

                // Handle audit properties
                AuditableEntityHandler.SetProperties(entity, "Add");

                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                var result = _mapper.Map<TDtoResult>(entity);
                return new Response<TDtoResult>
                {
                    Success = true,
                    Message = "Insert successful",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<TDtoResult>
                {
                    Success = false,
                    Message = $"Insert failed: {ex.Message}",
                    Data = default
                };
            }
        }

        public async Task<Response<TDtoResult>> AddAsync(TDto dto, Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entity = _mapper.Map<T>(dto);

                var existsResponse = await CheckExistsAsync(predicate, _context);

                if (existsResponse.Success)
                {
                    return new Response<TDtoResult>
                    {
                        Success = true,
                        Message = "Entity already exists",
                        Data = existsResponse.Data
                    };
                }

                // Handle audit properties
                AuditableEntityHandler.SetProperties(entity, "Add");

                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                var result = _mapper.Map<TDtoResult>(entity);
                return new Response<TDtoResult>
                {
                    Success = true,
                    Message = "Insert successful",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<TDtoResult>
                {
                    Success = false,
                    Message = $"Insert failed: {ex.Message}",
                    Data = default
                };
            }
        }

        public async Task<Response<AddRangeResult<TDtoResult>>> AddRangeAsync(IEnumerable<TDto> dtos, IEnumerable<Expression<Func<T, bool>>> predicates)
        {
            try
            {
                var entities = dtos.Select(dto => _mapper.Map<T>(dto)).ToList();
                var existingEntities = new List<T>();
                var entitiesToAdd = new List<T>();

                var predicateList = predicates.ToList();

                for (int i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var predicate = predicateList[i];
                    var existsResponse = await CheckExistsAsync(predicate, _context);

                    if (existsResponse.Success)
                    {
                        existingEntities.Add(_mapper.Map<T>(existsResponse.Data));
                    }
                    else
                    {
                        // Handle audit properties
                        AuditableEntityHandler.SetProperties(entity, "Add");
                        entitiesToAdd.Add(entity);
                    }
                }

                if (entitiesToAdd.Any())
                {
                    await _dbSet.AddRangeAsync(entitiesToAdd);
                    await _context.SaveChangesAsync();
                }

                var insertedEntities = entitiesToAdd.Select(e => _mapper.Map<TDtoResult>(e));
                var existingEntitiesDtos = existingEntities.Select(e => _mapper.Map<TDtoResult>(e));

                string message;
                if (existingEntitiesDtos.Any() && insertedEntities.Any())
                {
                    message = $"{insertedEntities.Count()} new entities inserted successfully. {existingEntities.Count} entities already existed and were not inserted.";
                }
                else if (existingEntitiesDtos.Any())
                {
                    message = $"All {existingEntities.Count} entities already exist and were not inserted.";
                }
                else if (insertedEntities.Any())
                {
                    message = $"All {insertedEntities.Count()} entities were inserted successfully.";
                }
                else
                {
                    message = $"No entities were processed.";
                }

                return new Response<AddRangeResult<TDtoResult>>
                {
                    Success = true,
                    Message = message,
                    Data = new AddRangeResult<TDtoResult>
                    {
                        InsertedEntities = insertedEntities,
                        ExistingEntities = existingEntitiesDtos,
                        InsertedEntitiesCount = insertedEntities.Count(),
                        ExistingEntitiesCount = existingEntitiesDtos.Count()
                    }
                };
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex);
                return new Response<AddRangeResult<TDtoResult>>
                {
                    Success = false,
                    Message = $"Insert operation failed: {ex.Message}",
                    Data = new AddRangeResult<TDtoResult>
                    {
                        InsertedEntitiesCount = 0,
                        ExistingEntitiesCount = 0,
                        InsertedEntities = Enumerable.Empty<TDtoResult>(),
                        ExistingEntities = Enumerable.Empty<TDtoResult>()
                    }
                };
            }
        }

        public async Task<Response<TDtoResult>> CheckExistsAsync(Expression<Func<T, bool>> predicate, TContext context)
        {
            try
            {
                var entity = await context.Set<T>().FirstOrDefaultAsync(predicate);
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
                await _errorLogger.LogErrorAsync(ex);
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
                await _errorLogger.LogErrorAsync(ex);
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
