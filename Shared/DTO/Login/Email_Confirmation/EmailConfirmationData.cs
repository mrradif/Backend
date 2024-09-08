
using Shared.DTO.Common;

namespace Shared.DTO.Login.Email_Confirmation
{
    public class EmailConfirmationData
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
