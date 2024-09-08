using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;


namespace BLL.Repository.Generic_Master_Detail.Implementation.Put
{
    public class PutGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDto, TMasterDtoResult, TDetailDto, TDetailDtoResult>
    : IPutGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDto, TMasterDtoResult, TDetailDto, TDetailDtoResult>
    where TMaster : class
    where TDetail : class
    where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TMaster> _masterSet;
        private readonly DbSet<TDetail> _detailSet;
        private readonly IMapper _mapper;

        public PutGenericMasterDetailRepo(TContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _masterSet = _context.Set<TMaster>();
            _detailSet = _context.Set<TDetail>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>> UpdateMasterDetailAsync(
            TMasterDto masterDto, IEnumerable<TDetailDto> detailDtos, string masterIdPropertyName, string detailForeignKeyPropertyName)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Map TMasterDto to TMaster entity
                    var masterEntity = _mapper.Map<TMaster>(masterDto);

                    // Update master entity in context
                    _context.Entry(masterEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    // Retrieve the value of the master entity's ID property dynamically
                    var masterIdProperty = masterEntity.GetType().GetProperty(masterIdPropertyName);
                    if (masterIdProperty == null)
                    {
                        throw new InvalidOperationException($"Master entity does not have a '{masterIdPropertyName}' property.");
                    }
                    var masterId = masterIdProperty.GetValue(masterEntity);

                    // Delete existing detail entities related to the master entity
                    var existingDetails = _detailSet.Where(d => EF.Property<object>(d, detailForeignKeyPropertyName).Equals(masterId));
                    _detailSet.RemoveRange(existingDetails);
                    await _context.SaveChangesAsync();

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

                    // Add new detail entities to context
                    await _detailSet.AddRangeAsync(detailEntities);
                    await _context.SaveChangesAsync();

                    // Commit transaction
                    await transaction.CommitAsync();

                    // Create response with success message and results
                    var result = new MasterDetailResult<TMasterDtoResult, TDetailDtoResult>
                    {
                        Master = _mapper.Map<TMasterDtoResult>(masterEntity),
                        Details = detailEntities.Select(_mapper.Map<TDetailDtoResult>).ToList(),

                    };

                    return new Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>
                    {
                        Success = true,
                        Data = result,
                        Message = "Master and detail entities updated successfully."
                    };
                }
                catch (Exception ex)
                {
                    // Rollback transaction on exception and return failure response
                    await transaction.RollbackAsync();
                    return new Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>
                    {
                        Success = false,
                        Message = $"Error updating master and detail entities: {ex.Message}"
                    };
                }
            }
        }
    }
}