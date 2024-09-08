namespace Shared.DTO.Common
{
    public class UpdateRangeResult<TDtoResult>
    {
        public List<TDtoResult> UpdatedEntities { get; set; }
        public List<TDtoResult> NotUpdatedEntities { get; set; }
        public int UpdatedEntitiesCount { get; set; }
        public int NotUpdatedEntitiesCount { get; set; }
    }
}
