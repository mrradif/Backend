using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Control_Panel.Administration.Logger
{
    public class ActivityLog
    {
        [Key]
        public long ActivityLogId { get; set; }

        public long? UserLoginInformationId { get; set; }
        // Navigation property
        [ForeignKey("UserLoginInformationId")]
        public virtual UserLoginInformation UserLoginInformation { get; set; }

        public Guid? UserId { get; set; }
        public string UserName { get; set; }


        public string ActionType { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string AddedValues { get; set; }
        public string PropertyChanges { get; set; }
        public string PreviousValues { get; set; }
        public string PresentValues { get; set; }
        public DateTime Timestamp { get; set; }
        public string PreviousPropertyValues { get; set; }
        public string ChangesPropertyValues { get; set; }


        public string RequestIpAddress { get; set; }
        public string RequestUrl { get; set; }
        public string DbContextName { get; set; }

        public long? OrganizationId { get; set; }
        public long? CompanyId { get; set; }



        public string MachineName { get; set; }
        public string OsVersion { get; set; }
        public string UserDomainName { get; set; }
        public string UserNameOnPC { get; set; }

        public string MacAddress { get; set; }
        public string IpAddress { get; set; }


    }
}
