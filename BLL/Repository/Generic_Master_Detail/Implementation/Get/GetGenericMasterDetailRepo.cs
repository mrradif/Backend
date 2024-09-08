using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;


namespace BLL.Repository.Generic_Master_Detail.Implementation.Get
{
    public class GetGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDtoResult, TDetailDtoResult>
       : IGetGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDtoResult, TDetailDtoResult>
       where TMaster : class
       where TDetail : class
       where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TMaster> _masterSet;
        private readonly DbSet<TDetail> _detailSet;
        private readonly IMapper _mapper;

        public GetGenericMasterDetailRepo(TContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _masterSet = _context.Set<TMaster>();
            _detailSet = _context.Set<TDetail>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>> GetMasterDetailAsync(
            object masterId, string masterIdPropertyName, string detailForeignKeyPropertyName)
        {
            // Find master entity by ID
            var masterEntity = await _context.Set<TMaster>().FindAsync(masterId);
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

            // Map master and detail entities to DTOs
            var masterDto = _mapper.Map<TMasterDtoResult>(masterEntity);
            var detailDtos = detailEntities.Select(_mapper.Map<TDetailDtoResult>).ToList();

            var result = new MasterDetailResult<TMasterDtoResult, TDetailDtoResult>
            {
                Master = masterDto,
                Details = detailDtos,
                MasterDataCount = 1,
                DetailDataCount = detailDtos.Count,
                MasterEntityName = typeof(TMaster).Name,
                DetailEntityName = typeof(TDetail).Name
            };

            return new Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>
            {
                Success = true,
                Data = result,
                Message = detailDtos.Any() ? "Master and detail entities retrieved successfully." : "Master entity retrieved successfully, no detail entities found."
            };
        }



    }
}
