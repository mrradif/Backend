namespace Shared.DTO.Common
{
    public class DeleteRangeResult<TDtoResult, TId>
    {
        public List<TDtoResult> DeletedEntities { get; set; }
        public List<TId> NotFoundEntityIds { get; set; }
        public int DeletedEntitiesCount { get; set; }
        public int NotFoundEntitiesCount { get; set; }
    }
}
