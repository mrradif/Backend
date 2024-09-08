using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;


namespace BLL.Repository.Generic_Master_Detail.Implementation.Delete
{
    public class DeleteGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDtoResult, TDetailDtoResult>
        : IDeleteGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDtoResult, TDetailDtoResult>
        where TMaster : class
        where TDetail : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TMaster> _masterSet;
        private readonly DbSet<TDetail> _detailSet;
        private readonly IMapper _mapper;

        public DeleteGenericMasterDetailRepo(TContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _masterSet = _context.Set<TMaster>();
            _detailSet = _context.Set<TDetail>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>> DeleteMasterDetailAsync(
            object masterId, string masterIdPropertyName, string detailForeignKeyPropertyName)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Find master entity by ID
                    var masterEntity = await _masterSet.FindAsync(masterId);
                    if (masterEntity == null)
                    {
                        return new Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>
                        {
                            Success = false,
                            Message = $"Master entity with ID '{masterId}' not found."
                        };
                    }

                    // Find detail entities by master ID
                    var detailEntities = await _detailSet
                        .Where(detail => EF.Property<object>(detail, detailForeignKeyPropertyName).Equals(masterId))
                        .ToListAsync();

                    // Remove detail entities
                    _detailSet.RemoveRange(detailEntities);

                    // Remove master entity
                    _masterSet.Remove(masterEntity);

                    // Save changes
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
                        Message = "Master and detail entities deleted successfully."
                    };
                }
                catch (Exception ex)
                {
                    // Rollback transaction on exception and return failure response
                    await transaction.RollbackAsync();
                    return new Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>
                    {
                        Success = false,
                        Message = $"Error deleting master and detail entities: {ex.Message}"
                    };
                }
            }
        }
    }
}
