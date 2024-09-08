

namespace Shared.DTO.Common
{
    public class MasterDetailResult<TMasterDto, TDetailDto>
    {
        public TMasterDto Master { get; set; }
        public List<TDetailDto> Details { get; set; }
        public int MasterDataCount { get; set; }
        public int DetailDataCount { get; set; }
        public string MasterEntityName { get; set; }
        public string DetailEntityName { get; set; }
    }
}
