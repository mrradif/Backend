

namespace Shared.DTO.Common
{
    public class AddRangeResult<TDtoResult>
    {
        public IEnumerable<TDtoResult> InsertedEntities { get; set; }
        public IEnumerable<TDtoResult> ExistingEntities { get; set; }

        public int InsertedEntitiesCount { get; set; }
        public int ExistingEntitiesCount { get; set; }
    }

}
