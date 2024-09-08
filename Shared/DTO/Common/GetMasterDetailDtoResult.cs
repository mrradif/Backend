
namespace Shared.DTO.Common
{
    public class GetMasterDetailDtoResult<TMasterDto, TDetailDto>
    {
        public TMasterDto Master { get; set; }
        public IEnumerable<TDetailDto> Details { get; set; }
        public int MasterDataCount { get; set; }
        public int DetailDataCount { get; set; }
        public string MasterEntityName { get; set; }
        public string DetailEntityName { get; set; }
    }
}
