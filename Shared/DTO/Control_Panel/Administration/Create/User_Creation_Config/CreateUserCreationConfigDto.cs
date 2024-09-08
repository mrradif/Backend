namespace Shared.DTO.Control_Panel.Administration.Create.User_Creation_Config
{
    public class CreateUserCreationConfigDto
    {
        public long OrganizationId { get; set; }
        public long CompanyId { get; set; }
        public long DivisionId { get; set; }
        public long BranchId { get; set; }
        public bool RequiredTwoFactor { get; set; }
        public bool LockoutPolicyEnabled { get; set; }
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
