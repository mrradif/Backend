

namespace Shared.DTO.Control_Panel.Administration.Delete
{
    public class DeleteOrganizationDto
    {
        public long OrganizationId { get; set; }
        public string OrgUniqueId { get; set; }
        public string OrgCode { get; set; }
        public string OrganizationName { get; set; }
        public bool isActive { get; set; }

    }
}
