
using BLL.Repository.Identity.User.Interface;
using DAL.Context.Control_Panel;
using DAL.Service.Logger;
using Microsoft.AspNetCore.Identity;
using Shared.DTO.Common;


namespace BLL.Repository.Identity.User.Implementation
{
    public class UserConfigsRepo: IUserConfigsRepo
    {
        readonly UserManager<ApplicationUser> _userManager;
        public UserConfigsRepo(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<bool>> AddTwoFactorEnableToUserAsync(string userId)
        {
            var response = new Response<bool>();

            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.Message = $"User with ID '{userId}' not found.";
                    return response;
                }

                // Check if two-factor authentication is already enabled
                var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                if (isTwoFactorEnabled)
                {
                    response.Message = $"Two-factor authentication is already enabled for user with ID '{userId}'.";
                    response.Data = true; // Indicate success
                    response.Success = true;
                    return response;
                }

                // Enable two-factor authentication for the user
                var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
                if (!result.Succeeded)
                {
                    response.Message = $"Failed to enable two-factor authentication for user with ID '{userId}'.";
                    return response;
                }

                response.Data = true; // Indicate success
                response.Success = true;
                response.Message = $"Two-factor authentication enabled successfully for user with ID '{userId}'.";
                return response;
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Message = $"Error ensuring two-factor authentication: {ex.Message}";
                return response;
            }
        }


        public async Task<Response<bool>> AddTwoFactorEnableToUsersAsync(IEnumerable<string> userIds)
        {
            var response = new Response<bool>();

            try
            {
                foreach (var userId in userIds)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        response.Message += $"User with ID '{userId}' not found. ";
                        continue; // Move to the next user
                    }

                    // Check if two-factor authentication is already enabled
                    var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                    if (isTwoFactorEnabled)
                    {
                        response.Message += $"Two-factor authentication is already enabled for user with ID '{userId}'. ";
                        continue; // Move to the next user
                    }

                    // Enable two-factor authentication for the user
                    var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
                    if (!result.Succeeded)
                    {
                        response.Message += $"Failed to enable two-factor authentication for user with ID '{userId}'. ";
                        continue; // Move to the next user
                    }

                    response.Message += $"Two-factor authentication enabled successfully for user with ID '{userId}'. ";
                }

                response.Data = true; // Indicate success for all users processed
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Message = $"Error ensuring two-factor authentication for multiple users: {ex.Message}";
                return response;
            }
        }


        public async Task<Response<bool>> RemoveTwoFactorEnableToUserAsync(string userId)
        {
            var response = new Response<bool>();

            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.Message = $"User with ID '{userId}' not found.";
                    return response;
                }

                // Check if two-factor authentication is already enabled
                var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                if (!isTwoFactorEnabled)
                {
                    response.Message = $"Two-factor authentication is not enabled for user with ID '{userId}'.";
                    response.Data = true; // Indicate success
                    response.Success = true;
                    return response;
                }

                // Enable two-factor authentication for the user
                var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
                if (!result.Succeeded)
                {
                    response.Message = $"Failed to remove two-factor authentication for user with ID '{userId}'.";
                    return response;
                }

                response.Data = true; // Indicate success
                response.Success = true;
                response.Message = $"Two-factor authentication remove successfully for user with ID '{userId}'.";
                return response;
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Message = $"Error ensuring two-factor authentication: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<bool>> RemoveTwoFactorEnableToUsersAsync(IEnumerable<string> userIds)
        {
            var response = new Response<bool>();

            try
            {
                foreach (var userId in userIds)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        response.Message += $"User with ID '{userId}' not found. ";
                        continue; // Move to the next user
                    }

                    // Check if two-factor authentication is already enabled
                    var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                    if (isTwoFactorEnabled)
                    {
                        response.Message += $"Two-factor authentication is not enabled for user with ID '{userId}'. ";
                        continue; // Move to the next user
                    }

                    // Enable two-factor authentication for the user
                    var result = await _userManager.SetTwoFactorEnabledAsync(user, false);
                    if (!result.Succeeded)
                    {
                        response.Message += $"Failed to remove two-factor authentication for user with ID '{userId}'. ";
                        continue; // Move to the next user
                    }

                    response.Message += $"Two-factor authentication remove successfully for user with ID '{userId}'. ";
                }

                response.Data = true; // Indicate success for all users processed
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Message = $"Error ensuring two-factor authentication for multiple users: {ex.Message}";
                return response;
            }
        }





        public async Task<Response<bool>> AddLockoutEnableToUserAsync(string userId)
        {
            var response = new Response<bool>();

            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.Message = $"User with ID '{userId}' not found.";
                    return response;
                }

                var isLockoutEnabled = await _userManager.GetLockoutEnabledAsync(user);
                if (isLockoutEnabled)
                {
                    response.Message = $"Lockout is already enabled for user with ID '{userId}'.";
                    response.Data = true; // Indicate success
                    response.Success = true;
                    return response;
                }

                var result = await _userManager.SetLockoutEnabledAsync(user, true);
                if (!result.Succeeded)
                {
                    response.Message = $"Failed to enable lockout for user with ID '{userId}'.";
                    return response;
                }

                response.Data = true; 
                response.Success = true;
                response.Message = $"Lockout enabled successfully for user with ID '{userId}'.";
                return response;
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Message = $"Error ensuring Lockout: {ex.Message}";
                return response;
            }
        }


        public async Task<Response<bool>> AddLockoutEnableToUsersAsync(IEnumerable<string> userIds)
        {
            var response = new Response<bool>();

            try
            {
                foreach (var userId in userIds)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        response.Message += $"User with ID '{userId}' not found. ";
                        continue; // Move to the next user
                    }

                    var isLockoutEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
                    if (isLockoutEnabled)
                    {
                        response.Message += $"Lockout is already enabled for user with ID '{userId}'. ";
                        continue; 
                    }

                    var result = await _userManager.SetLockoutEnabledAsync(user, true);
                    if (!result.Succeeded)
                    {
                        response.Message += $"Failed to enable Lockout for user with ID '{userId}'. ";
                        continue; // Move to the next user
                    }

                    response.Message += $"Lockout enabled successfully for user with ID '{userId}'. ";
                }

                response.Data = true; 
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Message = $"Error ensuring Lockout for multiple users: {ex.Message}";
                return response;
            }
        }


        public async Task<Response<bool>> RemoveLockoutEnableToUserAsync(string userId)
        {
            var response = new Response<bool>();

            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    response.Message = $"User with ID '{userId}' not found.";
                    return response;
                }

                var isLockoutEnabled = await _userManager.GetLockoutEnabledAsync(user);
                if (!isLockoutEnabled)
                {
                    response.Message = $"Lockout is not enabled for user with ID '{userId}'.";
                    response.Data = true; 
                    response.Success = true;
                    return response;
                }

                var result = await _userManager.SetLockoutEnabledAsync(user, false);
                if (!result.Succeeded)
                {
                    response.Message = $"Failed to remove Lockout for user with ID '{userId}'.";
                    return response;
                }

                response.Data = true; 
                response.Success = true;
                response.Message = $"Lockout remove successfully for user with ID '{userId}'.";
                return response;
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Message = $"Error ensuring Lockout: {ex.Message}";
                return response;
            }
        }

        public async Task<Response<bool>> RemoveLockoutEnableToUsersAsync(IEnumerable<string> userIds)
        {
            var response = new Response<bool>();

            try
            {
                foreach (var userId in userIds)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        response.Message += $"User with ID '{userId}' not found. ";
                        continue;
                    }

                    var isTwoFactorEnabled = await _userManager.GetLockoutEnabledAsync(user);
                    if (isTwoFactorEnabled)
                    {
                        response.Message += $"Lockout is not enabled for user with ID '{userId}'. ";
                        continue; 
                    }

                    var result = await _userManager.SetLockoutEnabledAsync(user, false);
                    if (!result.Succeeded)
                    {
                        response.Message += $"Failed to remove Lockout for user with ID '{userId}'. ";
                        continue; // Move to the next user
                    }

                    response.Message += $"Lockout remove successfully for user with ID '{userId}'. ";
                }

                response.Data = true; 
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Message = $"Error ensuring Lockout for multiple users: {ex.Message}";
                return response;
            }
        }



    }
}
