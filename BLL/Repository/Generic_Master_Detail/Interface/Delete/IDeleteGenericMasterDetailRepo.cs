using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;


public interface IDeleteGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDtoResult, TDetailDtoResult>
    where TMaster : class
    where TDetail : class
    where TContext : DbContext
{
    Task<Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>> DeleteMasterDetailAsync(object masterId, string masterIdPropertyName, string detailForeignKeyPropertyName);
}