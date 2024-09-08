using Microsoft.AspNetCore.Identity;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Identity;
using Shared.DTO.Control_Panel.Identity.User;
using Shared.DTO.Control_Panel.Identity.User.Shared.DTO.Control_Panel.Identity.User;
using Shared.DTO.Control_Panel.Identity.User.Update;
using Shared.DTO.Control_Panel.Identity.User.View;
using Shared.DTO.Login.Create;
using Shared.DTO.Login.Email_Confirmation;


namespace BLL.Repository.Identity.User.Interface
{
    public interface IUserRepo
    {
        Task<Response<ApplicationUserResultDto>> AddUserAsync(CreateApplicationUserDto userDto);
        Task<Response<ApplicationUserResultDto>> UpdateUserAsync(UpdateApplicationUserDto userDto);




        Task<Response<bool>> ConfirmEmailAsync(string email, string token);
        Task<Response<EmailConfirmationData>> ResendEmailConfirmationAsync(string email);


        Task<Response<LoginDto>> LoginAsync(LoginDto loginDto);
        Task<Response<string>> VerifyTwoFactorTokenAsync(TwoFactorVerificationDto twoFactorVerificationDto);



        Task<Response<List<ApplicationUserDto>>> GetAllUsersAsync();
        Task<Response<List<ApplicationUserDto>>> GetAllUsersAsync(UserSearchCriteriaDto searchCriteria);


        Task<Response<string>> AddClaimsAsync(AddClaimDto model);
        Task<Response<List<UserClaimDto>>> GetUserClaimsAsync(string userId);
        Task<Response<UserWithRolesAndClaimsDto>> GetUserWithRolesAndClaimsAsync(string userId);
        Task<Response<UserWithRolesAndRolesClaimsDto>> GetUserWithRolesAndRolesClaimsAsync(string userName);


        Task<Response<string>> UserRoleAssignAsync(UserRoleAssignDto model);
        Task<Response<List<string>>> GetUserRolesAsync(string userId);







    }
}
