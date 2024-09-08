using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;

namespace BLL.Repository.Generic_Master_Detail.Implementation.Post
{
    public class PostGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDto, TMasterDtoResult, TDetailDto, TDetailDtoResult>
        : IPostGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDto, TMasterDtoResult, TDetailDto, TDetailDtoResult>
        where TMaster : class
        where TDetail : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TMaster> _masterSet;
        private readonly DbSet<TDetail> _detailSet;
        private readonly IMapper _mapper;

        public PostGenericMasterDetailRepo(TContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _masterSet = _context.Set<TMaster>();
            _detailSet = _context.Set<TDetail>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>> AddMasterDetailAsync(
            TMasterDto masterDto, IEnumerable<TDetailDto> detailDtos, string masterIdPropertyName, string detailForeignKeyPropertyName)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Map TMasterDto to TMaster entity
                    var masterEntity = _mapper.Map<TMaster>(masterDto);

                    // Add master entity to context
                    await _masterSet.AddAsync(masterEntity);
                    await _context.SaveChangesAsync();

                    // Retrieve the value of the master entity's ID property dynamically
                    var masterIdProperty = masterEntity.GetType().GetProperty(masterIdPropertyName);
                    if (masterIdProperty == null)
                    {
                        throw new InvalidOperationException($"Master entity does not have a '{masterIdPropertyName}' property.");
                    }
                    var masterId = masterIdProperty.GetValue(masterEntity);

                    // Map each TDetailDto to TDetail entity and set the foreign key dynamically
                    var detailEntities = detailDtos.Select(detailDto =>
                    {
                        var detailEntity = _mapper.Map<TDetail>(detailDto);
                        var foreignKeyProperty = detailEntity.GetType().GetProperty(detailForeignKeyPropertyName);
                        if (foreignKeyProperty == null)
                        {
                            throw new InvalidOperationException($"Detail entity does not have a '{detailForeignKeyPropertyName}' property.");
                        }
                        foreignKeyProperty.SetValue(detailEntity, masterId);

                        return detailEntity;
                    }).ToList();

                    // Add detail entities to context
                    await _detailSet.AddRangeAsync(detailEntities);
                    await _context.SaveChangesAsync();

                    // Commit transaction
                    await transaction.CommitAsync();

                    // Create response with success message and results
                    var result = new MasterDetailResult<TMasterDtoResult, TDetailDtoResult>
                    {
                        Master = _mapper.Map<TMasterDtoResult>(masterEntity),
                        Details = detailEntities.Select(_mapper.Map<TDetailDtoResult>).ToList(),
                        MasterDataCount = 1,
                        DetailDataCount = detailEntities.Count,
                        MasterEntityName = typeof(TMaster).Name,
                        DetailEntityName = typeof(TDetail).Name

                    };

                    return new Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>
                    {
                        Success = true,
                        Data = result,
                        Message = "Master and detail entities added successfully."
                    };
                }
                catch (Exception ex)
                {
                    // Rollback transaction on exception and return failure response
                    await transaction.RollbackAsync();
                    return new Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>
                    {
                        Success = false,
                        Message = $"Error adding master and detail entities: {ex.Message}"
                    };
                }
            }
        }





        public async Task<Response<PostMasterDetailResultDto<TMasterDtoResult, TDetailDtoResult>>> AddMasterDetailIncludeAsync(
            TMasterDto masterDto, ICollection<TDetailDto> detailDtos, string masterIdPropertyName, string detailForeignKeyPropertyName)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Map TMasterDto to TMaster entity
                    var masterEntity = _mapper.Map<TMaster>(masterDto);

                    // Add master entity to context
                    await _masterSet.AddAsync(masterEntity);
                    await _context.SaveChangesAsync();

                    // Retrieve the value of the master entity's ID property dynamically
                    var masterIdProperty = masterEntity.GetType().GetProperty(masterIdPropertyName);
                    if (masterIdProperty == null)
                    {
                        throw new InvalidOperationException($"Master entity does not have a '{masterIdPropertyName}' property.");
                    }
                    var masterId = masterIdProperty.GetValue(masterEntity);

                    // Map each TDetailDto to TDetail entity and set the foreign key dynamically
                    var detailEntities = detailDtos.Select(detailDto =>
                    {
                        var detailEntity = _mapper.Map<TDetail>(detailDto);
                        var foreignKeyProperty = detailEntity.GetType().GetProperty(detailForeignKeyPropertyName);
                        if (foreignKeyProperty == null)
                        {
                            throw new InvalidOperationException($"Detail entity does not have a '{detailForeignKeyPropertyName}' property.");
                        }
                        foreignKeyProperty.SetValue(detailEntity, masterId);

                        return detailEntity;
                    }).ToList();

                    // Add detail entities to context
                    await _detailSet.AddRangeAsync(detailEntities);
                    await _context.SaveChangesAsync();

                    // Load includes if any (using Property for non-navigation properties)
                    await _context.Entry(masterEntity).ReloadAsync();

                    // Commit transaction
                    await transaction.CommitAsync();

                    // Create response with success message and results
                    var result = new PostMasterDetailResultDto<TMasterDtoResult, TDetailDtoResult>
                    {
                        Master = _mapper.Map<TMasterDtoResult>(masterEntity),
                        Details = detailEntities.Select(_mapper.Map<TDetailDtoResult>).ToList(),
                        MasterDataCount = 1,
                        DetailDataCount = detailEntities.Count,
                        MasterEntityName = typeof(TMaster).Name,
                        DetailEntityName = typeof(TDetail).Name
                    };

                    return new Response<PostMasterDetailResultDto<TMasterDtoResult, TDetailDtoResult>>
                    {
                        Success = true,
                        Data = result,
                        Message = "Master and detail entities added successfully."
                    };
                }
                catch (Exception ex)
                {
                    // Log error using ErrorLogger
                    //  await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);

                    // Rollback transaction on exception and return failure response
                    await transaction.RollbackAsync();
                    return new Response<PostMasterDetailResultDto<TMasterDtoResult, TDetailDtoResult>>
                    {
                        Success = false,
                        Message = $"Error adding master and detail entities: {ex.Message}"
                    };
                }
            }
        }





    }
}
