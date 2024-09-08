using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Control_Panel.Identity.User
{
    namespace Shared.DTO.Control_Panel.Identity.User
    {
        public class UserWithRolesAndRolesClaimsDto
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public List<UserClaimDto> Claims { get; set; }
            public List<UserRoleWithClaimsDto> RolesWithClaims { get; set; }
        }
    }

 

   
}
