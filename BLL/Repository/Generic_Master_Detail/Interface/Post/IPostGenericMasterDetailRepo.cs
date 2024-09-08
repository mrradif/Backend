using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;

public interface IPostGenericMasterDetailRepo<TMaster, TDetail, TContext, TMasterDto, TMasterDtoResult, TDetailDto, TDetailDtoResult>
    where TMaster : class
    where TDetail : class
    where TContext : DbContext
{

    // Without Include
    Task<Response<MasterDetailResult<TMasterDtoResult, TDetailDtoResult>>> AddMasterDetailAsync(TMasterDto masterDto, IEnumerable<TDetailDto> detailDtos, string masterForeignKey, string detailForeignKey);


    // With Include
    Task<Response<PostMasterDetailResultDto<TMasterDtoResult, TDetailDtoResult>>> AddMasterDetailIncludeAsync(
       TMasterDto masterDto, ICollection<TDetailDto> detailDtos, string masterForeignKey, string detailForeignKey);
}
