namespace Shared.Domain.Common.Interface
{
    public interface IOrganizationAndCompanyEntity
    {
        long? OrganizationId { get; set; }
        long? CompanyId { get; set; }
    }

}
