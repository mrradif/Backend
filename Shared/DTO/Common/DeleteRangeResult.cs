namespace Shared.DTO.Common
{
    public class DeleteRangeResult<TDtoResult, TId>
    {
        public List<TDtoResult> DeletedEntities { get; set; }
        public List<TId> NotFoundEntityIds { get; set; }
        public int DeletedEntitiesCount { get; set; }
        public int NotFoundEntitiesCount { get; set; }

        // Constructor for success
        public DeleteRangeResult(
            List<TDtoResult> deletedEntities,
            int deletedEntitiesCount)
        {
            DeletedEntities = deletedEntities;
            DeletedEntitiesCount = deletedEntitiesCount;
            NotFoundEntityIds = new List<TId>();
            NotFoundEntitiesCount = 0;
        }

        // Constructor for failure
        public DeleteRangeResult(
            List<TId> notFoundEntityIds,
            int notFoundEntitiesCount)
        {
            DeletedEntities = new List<TDtoResult>();
            DeletedEntitiesCount = 0;
            NotFoundEntityIds = notFoundEntityIds;
            NotFoundEntitiesCount = notFoundEntitiesCount;
        }

        // Default constructor
        public DeleteRangeResult()
        {
            DeletedEntities = new List<TDtoResult>();
            NotFoundEntityIds = new List<TId>();
        }
    }
}
