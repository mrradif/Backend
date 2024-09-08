
using System.ComponentModel.DataAnnotations;


namespace Shared.Domain.Control_Panel.Administration.Logger
{
    public class UserLoginInformation
    {
        [Key]
        public long UserLoginInformationId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public string UserAgent { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        [Required]
        public DateTime ExpirationTime { get; set; }

        public DateTime? LogoutTime { get; set; }
    }
}
