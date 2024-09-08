using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;


public interface IPutGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDto, TMasterDtoResult, TDetailDto, TDetailDtoResult>
   where TMaster : class
   where TDetail : class
   where TContext : DbContext
{
    Task<Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>> UpdateMasterDetailAsync(TMasterDto masterDto, IEnumerable<TDetailDto> detailDtos, string masterIdPropertyName, string detailForeignKeyPropertyName);
}