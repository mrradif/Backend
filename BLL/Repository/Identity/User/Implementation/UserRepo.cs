
using AutoMapper;
using BLL.Repository.Email.Interface;
using BLL.Repository.Identity.User.Interface;
using BLL.Service.Control_Panel.User_Creation_Config_Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Data.Password;
using Shared.Domain.Control_Panel.Identity;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Identity;
using Shared.DTO.Control_Panel.Identity.User;
using Shared.DTO.Control_Panel.Identity.User.Shared.DTO.Control_Panel.Identity.User;
using Shared.DTO.Control_Panel.Identity.User.Update;
using Shared.DTO.Control_Panel.Identity.User.View;
using Shared.DTO.Login.Create;
using Shared.DTO.Login.Email_Confirmation;
using System.Security.Claims;

namespace BLL.Repository.Identity.User.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IEmailSender _emailSender;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        private readonly IdentityOptions _identityOptions;
        private readonly IOptionsSnapshot<DataProtectionTokenProviderOptions> _tokenOptions;

        private readonly IMapper _mapper;

        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        private readonly GetUserCreationConfigService _userCreateConfigSingleService;


        public UserRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IEmailSender emailSender, IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory, IOptionsSnapshot<DataProtectionTokenProviderOptions> tokenOptions, IOptions<IdentityOptions> identityOptions, IMapper mapper, IPasswordHasher<ApplicationUser> passwordHasher, GetUserCreationConfigService userCreateConfigSingleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _tokenOptions = tokenOptions;
            _identityOptions = identityOptions.Value;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userCreateConfigSingleService = userCreateConfigSingleService;
        }



        public async Task<Response<List<ApplicationUserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();

                var usersDto = _mapper.Map<List<ApplicationUserDto>>(users);
                var userCount = usersDto.Count;

                return new Response<List<ApplicationUserDto>>
                {
                    Success = true,
                    Message = $"Users retrieved successfully. Total users: {userCount}",
                    Data = usersDto
                };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's error handling strategy
                return new Response<List<ApplicationUserDto>>
                {
                    Success = false,
                    Message = $"Error retrieving users: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<Response<List<ApplicationUserDto>>> GetAllUsersAsync(UserSearchCriteriaDto searchCriteria)
        {
            try
            {
                // Start with all users
                var query = _userManager.Users.AsQueryable();

                // Apply filters based on search criteria
                if (!string.IsNullOrEmpty(searchCriteria.Username))
                {
                    query = query.Where(u => u.UserName.Contains(searchCriteria.Username));
                }

                if (!string.IsNullOrEmpty(searchCriteria.Email))
                {
                    query = query.Where(u => u.Email.Contains(searchCriteria.Email));
                }

                if (searchCriteria.IsActive)
                {
                    query = query.Where(u => u.IsActive == searchCriteria.IsActive);
                }

                if (!string.IsNullOrEmpty(searchCriteria.EmployeeId))
                {
                    query = query.Where(u => u.EmployeeId == searchCriteria.EmployeeId);
                }

                if (!string.IsNullOrEmpty(searchCriteria.EmployeeCode))
                {
                    query = query.Where(u => u.EmployeeCode == searchCriteria.EmployeeCode);
                }

                if (searchCriteria.OrganizationId.HasValue)
                {
                    query = query.Where(u => u.OrganizationId == searchCriteria.OrganizationId);
                }

                if (searchCriteria.CompanyId.HasValue)
                {
                    query = query.Where(u => u.CompanyId == searchCriteria.CompanyId);
                }

                if (searchCriteria.DivisionId.HasValue)
                {
                    query = query.Where(u => u.DivisionId == searchCriteria.DivisionId);
                }

                if (searchCriteria.BranchId.HasValue)
                {
                    query = query.Where(u => u.BranchId == searchCriteria.BranchId);
                }

                // Execute the query and retrieve users
                var users = await query.ToListAsync();
                var usersDto = _mapper.Map<List<ApplicationUserDto>>(users);

                if (usersDto.Count > 0)
                {
                    return new Response<List<ApplicationUserDto>>
                    {
                        Success = true,
                        Message = "Users retrieved successfully based on search criteria.",
                        Data = usersDto
                    };
                }
                else
                {
                    return new Response<List<ApplicationUserDto>>
                    {
                        Success = false,
                        Message = "No users found based on the provided search criteria.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<ApplicationUserDto>>
                {
                    Success = false,
                    Message = $"Error retrieving users based on search criteria: {ex.Message}",
                    Data = null
                };
            }
        }




        public async Task<Response<ApplicationUserResultDto>> AddUserAsync(CreateApplicationUserDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            user.IsBackupPassword = BackupPassword.IsBackupPassword;



            // Get User Creattion Config
            var userCreationConfigData = await _userCreateConfigSingleService.GetActiveUserConfigAsync();

            if (userCreationConfigData.Success && userCreationConfigData.Data != null)
            {
                if (userCreationConfigData.Data.RequiredTwoFactor)
                {
                    user.TwoFactorEnabled = true;
                }

                if (userCreationConfigData.Data.LockoutPolicyEnabled)
                {
                    user.LockoutEnabled = true;
                }

                user.OrganizationId = userCreationConfigData.Data.OrganizationId;
                user.CompanyId = userCreationConfigData.Data.CompanyId;
                user.DivisionId = userCreationConfigData.Data.DivisionId;
                user.BranchId = userCreationConfigData.Data.BranchId;
            }


            var result = await _userManager.CreateAsync(user, userDto.Password);


            // Backup Password
            if (result.Succeeded && user.IsBackupPassword)
            {
                var backupPasswordHash = _userManager.PasswordHasher.HashPassword(user, BackupPassword.Password);
                user.BackupPasswordHash = backupPasswordHash;

                await _userManager.UpdateAsync(user);
            }


            if (result.Succeeded)
            {
                // Email Confirmation
                var requireConfirmedEmail = _identityOptions.SignIn.RequireConfirmedEmail;

                if (requireConfirmedEmail)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Generate the confirmation link
                    var confirmationLink = $"{_actionContextAccessor.ActionContext.HttpContext.Request.Scheme}://{_actionContextAccessor.ActionContext.HttpContext.Request.Host}/Account/ConfirmEmail?token={token}&email={Uri.EscapeDataString(user.Email)}";

                    // Create an object with the ConfirmationLink property
                    var templateData = new { ConfirmationLink = confirmationLink };

                    // Send email with the confirmation link using the template
                    var (sentSuccessfully, emailAddress) = await _emailSender.SendEmailFromTemplateAsync(
                        user.Email,
                        "Email Confirmation",
                        "EmailConfirmation",
                        templateData
                    );

                    var userResultDto = _mapper.Map<ApplicationUserResultDto>(user);

                    // Prepare response with token included in Message
                    var response = new Response<ApplicationUserResultDto>
                    {
                        Success = true,
                        Message = $"User creation successful. Please check your email ({user.Email}) to confirm your email address. Token: {token}",
                        Data = userResultDto
                    };

                    return response;
                }
                else
                {
                    // Proceed without sending email confirmation
                    var userResultDto = _mapper.Map<ApplicationUserResultDto>(user);

                    var response = new Response<ApplicationUserResultDto>
                    {
                        Success = true,
                        Message = "User creation successful.",
                        Data = userResultDto
                    };

                    return response;
                }
            }
            else
            {
                // Return failure response with error messages
                var response = new Response<ApplicationUserResultDto>
                {
                    Success = false,
                    Message = string.Join("; ", result.Errors.Select(e => e.Description))
                };

                return response;
            }
        }




        public async Task<Response<EmailConfirmationData>> ResendEmailConfirmationAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<EmailConfirmationData>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                return new Response<EmailConfirmationData>
                {
                    Success = false,
                    Message = "Email is already confirmed."
                };
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{_actionContextAccessor.ActionContext.HttpContext.Request.Scheme}://{_actionContextAccessor.ActionContext.HttpContext.Request.Host}/Account/ConfirmEmail?token={token}&email={Uri.EscapeDataString(user.Email)}";

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your email by clicking this link: <a href=\"{confirmationLink}\">link</a>");

            var responseData = new EmailConfirmationData
            {
                Email = user.Email,
                Token = token
            };

            return new Response<EmailConfirmationData>
            {
                Success = true,
                Message = "Confirmation email resent successfully.",
                Data = responseData
            };
        }



        public async Task<Response<bool>> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new Response<bool>
                {
                    Success = true,
                    Message = "Email confirmed successfully.",
                    Data = true
                };
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code == "InvalidToken" || error.Code == "InvalidTokenWithStatus")
                    {
                        return new Response<bool>
                        {
                            Success = false,
                            Message = "Email confirmation failed. The confirmation link may have expired."
                        };
                    }
                }

                return new Response<bool>
                {
                    Success = false,
                    Message = "Email confirmation failed. Please try again later."
                };
            }
        }


        public async Task<Response<ApplicationUserResultDto>> UpdateUserAsync(UpdateApplicationUserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null)
            {
                return new Response<ApplicationUserResultDto>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            // Update properties using the UpdatePropertyHandler
            UpdatePropertyHandler.UpdateProperties(user, userDto);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded && user.IsBackupPassword)
            {
                user.BackupPasswordHash = _userManager.PasswordHasher.HashPassword(user, BackupPassword.Password);
                await _userManager.UpdateAsync(user);
            }

            return new Response<ApplicationUserResultDto>
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "User update successful." : string.Join("; ", result.Errors.Select(e => e.Description)),
                Data = result.Succeeded ? _mapper.Map<ApplicationUserResultDto>(user) : null
            };
        }




        public async Task<Response<LoginDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
            {
                return new Response<LoginDto>
                {
                    Success = false,
                    Message = "Invalid username",
                    Data = loginDto
                };
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                var lockoutEnd = user.LockoutEnd.HasValue ? user.LockoutEnd.Value.UtcDateTime : DateTime.UtcNow;
                var lockoutTimeRemaining = lockoutEnd - DateTime.UtcNow;
                var lockoutTimeRemainingMessage = $"Account locked due to too many failed login attempts. Please try again after {lockoutTimeRemaining.Minutes} minutes and {lockoutTimeRemaining.Seconds} seconds.";

                return new Response<LoginDto>
                {
                    Success = false,
                    Message = lockoutTimeRemainingMessage,
                    Data = loginDto
                };
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded && !result.RequiresTwoFactor)
            {
                if (user.IsBackupPassword)
                {
                    // Check backup password
                    var isBackupPasswordValid = _userManager.PasswordHasher.VerifyHashedPassword(user, user.BackupPasswordHash, loginDto.Password) == PasswordVerificationResult.Success;
                    if (isBackupPasswordValid)
                    {
                        // Sign in the user
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return new Response<LoginDto>
                        {
                            Success = true,
                            Message = "Login successful with backup password",
                            Data = loginDto
                        };
                    }
                    else
                    {
                        return new Response<LoginDto>
                        {
                            Success = false,
                            Message = "Invalid primary and backup password",
                            Data = loginDto
                        };
                    }
                }

                return new Response<LoginDto>
                {
                    Success = false,
                    Message = "Invalid password",
                    Data = loginDto
                };
            }



            // Retrieve TokenLifespan from options
            var tokenLifespan = _tokenOptions.Value.TokenLifespan;

            if (result.RequiresTwoFactor)
            {
                if (await _userManager.GetTwoFactorEnabledAsync(user))
                {
                    if (string.IsNullOrEmpty(user.Email))
                    {
                        return new Response<LoginDto>
                        {
                            Success = false,
                            Message = "User's email address is not available to send two-factor authentication token",
                            Data = loginDto
                        };
                    }

                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    //await _emailSender.SendEmailAsync(user.Email, "Two-Factor Authentication Token", $"Your two-factor authentication token is: {token}");

                    // Send email using a predefined template
                    var (sentSuccessfully, emailAddress) = await _emailSender.SendEmailFromTemplateAsync(user.Email, "Two-Factor Authentication Token", "TwoFactorToken", token);

                    // Calculate token validity
                    var tokenValidUntil = DateTime.UtcNow.Add(tokenLifespan);

                    return new Response<LoginDto>
                    {
                        Success = true,
                        Message = $"A Two-Factor Authentication code has been sent to {user.Email}. The code is valid until {tokenValidUntil.ToLocalTime()}.",
                        Data = loginDto
                    };

                }
                else
                {
                    return new Response<LoginDto>
                    {
                        Success = false,
                        Message = "Two-factor authentication is required but not enabled for the user",
                        Data = loginDto
                    };
                }
            }
            else if (result.Succeeded)
            {
                return new Response<LoginDto>
                {
                    Success = true,
                    Message = "Login successful",
                    Data = loginDto
                };
            }
            else
            {
                if (result.IsLockedOut)
                {
                    var lockoutEnd = user.LockoutEnd.HasValue ? user.LockoutEnd.Value.UtcDateTime : DateTime.UtcNow;
                    var lockoutTimeRemaining = lockoutEnd - DateTime.UtcNow;
                    var lockoutTimeRemainingMessage = $"Account locked due to too many failed login attempts. Please try again after {lockoutTimeRemaining.Minutes} minutes and {lockoutTimeRemaining.Seconds} seconds.";

                    return new Response<LoginDto>
                    {
                        Success = false,
                        Message = lockoutTimeRemainingMessage,
                        Data = loginDto
                    };
                }


                return new Response<LoginDto>
                {
                    Success = false,
                    Message = "Invalid username or password",
                    Data = loginDto
                };
            }
        }




        public async Task<Response<string>> VerifyTwoFactorTokenAsync(TwoFactorVerificationDto verificationDto)
        {
            var user = await _userManager.FindByNameAsync(verificationDto.UserName);

            if (user == null)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var result = await _signInManager.TwoFactorSignInAsync("Email", verificationDto.Token, false, false);

            if (result.Succeeded)
            {
                return new Response<string>
                {
                    Success = true,
                    Message = "Login successful with Two-factor authentication",
                    Data = verificationDto.UserName
                };
            }
            else if (result.IsLockedOut)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = "Account locked due to too many failed attempts"
                };
            }
            else if (result.IsNotAllowed)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = "Two-factor authentication is not allowed for this user"
                };
            }
            else
            {
                return new Response<string>
                {
                    Success = false,
                    Message = "Two-factor authentication failed. The authentication token is invalid or expired."
                };
            }
        }




        public async Task<Response<string>> AddClaimsAsync(AddClaimDto model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"User with ID {model.UserId} not found.",
                        Data = null
                    };
                }

                foreach (var claimDto in model.Claims)
                {
                    var result = await _userManager.AddClaimAsync(user, new Claim(claimDto.ClaimType, claimDto.ClaimValue));

                    if (!result.Succeeded)
                    {
                        return new Response<string>
                        {
                            Success = false,
                            Message = $"Failed to add claim '{claimDto.ClaimType}': {string.Join(",", result.Errors.Select(e => e.Description))}",
                            Data = null
                        };
                    }
                }

                return new Response<string>
                {
                    Success = true,
                    Message = $"Claims added to user {user.UserName} successfully.",
                    Data = $"Claims added to user {user.UserName} successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                };
            }
        }



        public async Task<Response<string>> UserRoleAssignAsync(UserRoleAssignDto model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"User with ID {model.UserId} not found.",
                        Data = null
                    };
                }

                // Remove existing roles
                var existingRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);

                if (!removeResult.Succeeded)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Failed to remove existing roles for user {user.UserName}.",
                        Data = null
                    };
                }

                // Assign new roles
                var addResult = await _userManager.AddToRolesAsync(user, model.Roles);

                if (!addResult.Succeeded)
                {
                    // Log the failed roles for debugging
                    var failedRoles = model.Roles.Where(role => !addResult.Succeeded);
                    foreach (var role in failedRoles)
                    {
                        // Log or handle failed roles as needed
                        Console.WriteLine($"Failed to assign role {role} to user {user.UserName}");
                    }

                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Failed to assign roles to user {user.UserName}.",
                        Data = null
                    };
                }

                return new Response<string>
                {
                    Success = true,
                    Message = $"Roles assigned to user {user.UserName} successfully.",
                    Data = $"Roles assigned to user {user.UserName} successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<Response<List<string>>> GetUserRolesAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return new Response<List<string>>
                    {
                        Success = false,
                        Message = $"User with ID {userId} not found.",
                        Data = null
                    };
                }

                var roles = await _userManager.GetRolesAsync(user);

                return new Response<List<string>>
                {
                    Success = true,
                    Message = $"User roles retrieved successfully.",
                    Data = roles.ToList()
                };
            }
            catch (Exception ex)
            {
                return new Response<List<string>>
                {
                    Success = false,
                    Message = $"Error retrieving user roles: {ex.Message}",
                    Data = null
                };
            }
        }







        public async Task<Response<List<UserClaimDto>>> GetUserClaimsAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return new Response<List<UserClaimDto>>
                    {
                        Success = false,
                        Message = $"User with ID {userId} not found.",
                        Data = null
                    };
                }

                var claims = await _userManager.GetClaimsAsync(user);

                var userClaimsDto = claims.Select(c => new UserClaimDto
                {
                    ClaimType = c.Type,
                    ClaimValue = c.Value
                }).ToList();

                return new Response<List<UserClaimDto>>
                {
                    Success = true,
                    Message = $"User claims retrieved successfully.",
                    Data = userClaimsDto
                };
            }
            catch (Exception ex)
            {
                return new Response<List<UserClaimDto>>
                {
                    Success = false,
                    Message = $"Error retrieving user claims: {ex.Message}",
                    Data = null
                };
            }
        }




        public async Task<Response<UserWithRolesAndClaimsDto>> GetUserWithRolesAndClaimsAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return new Response<UserWithRolesAndClaimsDto>
                    {
                        Success = false,
                        Message = $"User with ID {userId} not found.",
                        Data = null
                    };
                }

                var claims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var userDto = new UserWithRolesAndClaimsDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Claims = claims.Select(c => new UserClaimDto { ClaimType = c.Type, ClaimValue = c.Value }).ToList(),
                    Roles = roles.ToList()
                };

                return new Response<UserWithRolesAndClaimsDto>
                {
                    Success = true,
                    Message = $"User with ID {userId} retrieved successfully with roles and claims.",
                    Data = userDto
                };
            }
            catch (Exception ex)
            {
                return new Response<UserWithRolesAndClaimsDto>
                {
                    Success = false,
                    Message = $"Error retrieving user with roles and claims: {ex.Message}",
                    Data = null
                };
            }
        }



        public async Task<Response<UserWithRolesAndRolesClaimsDto>> GetUserWithRolesAndRolesClaimsAsync(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    return new Response<UserWithRolesAndRolesClaimsDto>
                    {
                        Success = false,
                        Message = $"User with ID {userName} not found.",
                        Data = null
                    };
                }

                // Get user claims
                var userClaims = await _userManager.GetClaimsAsync(user);
                var userClaimsDto = userClaims.Select(c => new UserClaimDto { ClaimType = c.Type, ClaimValue = c.Value }).ToList();

                // Get user roles with their claims
                var roles = await _userManager.GetRolesAsync(user);
                var rolesWithClaimsDto = new List<UserRoleWithClaimsDto>();

                foreach (var roleName in roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);

                    if (role != null)
                    {
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        var roleClaimsDto = roleClaims.Select(c => new UserRoleClaimDto { ClaimType = c.Type, ClaimValue = c.Value }).ToList();

                        rolesWithClaimsDto.Add(new UserRoleWithClaimsDto
                        {
                            RoleId = role.Id,
                            RoleName = role.Name,
                            Claims = roleClaimsDto
                        });
                    }
                }

                var userWithRolesAndRolesClaimsDto = new UserWithRolesAndRolesClaimsDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Claims = userClaimsDto,
                    RolesWithClaims = rolesWithClaimsDto
                };

                return new Response<UserWithRolesAndRolesClaimsDto>
                {
                    Success = true,
                    Message = "User information with roles and role claims retrieved successfully.",
                    Data = userWithRolesAndRolesClaimsDto
                };
            }
            catch (Exception ex)
            {
                return new Response<UserWithRolesAndRolesClaimsDto>
                {
                    Success = false,
                    Message = $"Error retrieving user information with roles and role claims: {ex.Message}",
                    Data = null
                };
            }
        }






    }

}

