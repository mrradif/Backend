using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;


public interface IGetGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDtoResult, TDetailDtoResult>
    where TMaster : class
    where TDetail : class
    where TContext : DbContext
{
    Task<Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>> GetMasterDetailAsync(
       object masterId, string masterIdPropertyName, string detailForeignKeyPropertyName);
}