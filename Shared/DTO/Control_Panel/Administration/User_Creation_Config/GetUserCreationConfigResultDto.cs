namespace Shared.DTO.Control_Panel.Administration.User_Creation_Config
{
    public class GetUserCreationConfigResultDto
    {
        public int UserCreationConfigId { get; set; }
        public long OrganizationId { get; set; }
        public long CompanyId { get; set; }
        public long DivisionId { get; set; }
        public long BranchId { get; set; }
        public bool RequiredTwoFactor { get; set; }
        public bool LockoutPolicyEnabled { get; set; }
    }
}
