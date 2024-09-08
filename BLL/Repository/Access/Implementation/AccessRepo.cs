using AutoMapper;
using BLL.Repository.Access.Interface;
using BLL.Repository.Email.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Domain.Control_Panel.Identity;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Identity;
using Shared.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Shared.Service;
using DAL.Context.Control_Panel;
using Microsoft.AspNetCore.Http;
using Shared.DTO.Common.Access;
using System.Security.Cryptography;
using Shared.DTO.Login.Create;
using Shared.DTO.Logout;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.RefreshToken;
using BLL.Repository.Identity.User.Interface;
using Shared.Domain.Control_Panel.Administration.Logger;


namespace BLL.Repository.Access.Implementation
{
    public class AccessRepo: IAccessRepo
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

        private readonly Random _random = new Random();

        private readonly ControlPanelDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUserRepo _userRepo;


        public AccessRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IEmailSender emailSender, IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory, IOptionsSnapshot<DataProtectionTokenProviderOptions> tokenOptions, IOptions<IdentityOptions> identityOptions, IMapper mapper, IPasswordHasher<ApplicationUser> passwordHasher, ControlPanelDbContext context, IHttpContextAccessor httpContextAccessor,IUserRepo userRepo)
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
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;   
        }




        //public async Task<Response<LoginDto>> LoginAsync(LoginDto loginDto)
        //{
        //    var user = await _userManager.FindByNameAsync(loginDto.UserName);

        //    if (user == null)
        //    {
        //        return new Response<LoginDto>
        //        {
        //            Success = false,
        //            Message = "Invalid username",
        //            Data = loginDto
        //        };
        //    }

        //    if (!user.IsActive)
        //    {
        //        return new Response<LoginDto>
        //        {
        //            Success = false,
        //            Message = "Unauthorized user",
        //            Data = loginDto
        //        };
        //    }

        //    if (await _userManager.IsLockedOutAsync(user))
        //    {
        //        return GetLockoutResponse(user, loginDto);
        //    }

        //    var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, isPersistent: false, lockoutOnFailure: true);

        //    if (result.RequiresTwoFactor)
        //    {
        //        return await HandleTwoFactorAuthentication(user, loginDto);
        //    }

        //    if (result.Succeeded)
        //    {
        //        var loginResponse = await HandleSuccessfulLogin(user, loginDto);

        //        if (!loginResponse.Success)
        //        {
        //            return loginResponse;
        //        }

        //        return loginResponse;
        //    }

        //    if (result.IsLockedOut)
        //    {
        //        return GetLockoutResponse(user, loginDto);
        //    }


        //    if (!result.Succeeded && !result.RequiresTwoFactor)
        //    {
        //        if (user.IsBackupPassword)
        //        {
        //            // Check backup password
        //            var isBackupPasswordValid = _userManager.PasswordHasher.VerifyHashedPassword(user, user.BackupPasswordHash, loginDto.Password) == PasswordVerificationResult.Success;
        //            if (isBackupPasswordValid)
        //            {
        //                // Sign in the user
        //                await _signInManager.SignInAsync(user, isPersistent: false);

        //                var loginResponse = await HandleSuccessfulLogin(user, loginDto);

        //                if (!loginResponse.Success)
        //                {
        //                    return loginResponse;
        //                }

        //                return loginResponse;
        //            }
        //            else
        //            {
        //                return new Response<LoginDto>
        //                {
        //                    Success = false,
        //                    Message = "Invalid primary and backup password",
        //                    Data = loginDto
        //                };
        //            }
        //        }

        //        return new Response<LoginDto>
        //        {
        //            Success = false,
        //            Message = "Invalid password",
        //            Data = loginDto
        //        };
        //    }

        //    return new Response<LoginDto>
        //    {
        //        Success = false,
        //        Message = "Invalid username or password",
        //        Data = loginDto
        //    };
        //}


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
                if (user.IsBackupPassword)
                {
                    // Check backup password
                    var isBackupPasswordValid = _userManager.PasswordHasher.VerifyHashedPassword(user, user.BackupPasswordHash, loginDto.Password) == PasswordVerificationResult.Success;
                    if (isBackupPasswordValid)
                    {
                        // Sign in the user
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        var loginResponse = await HandleSuccessfulLogin(user, loginDto);

                        if (!loginResponse.Success)
                        {
                            return loginResponse;
                        }

                        return loginResponse;
                    }
                    else
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
                }
               
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
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

                        // Send email using a predefined template
                        var (sentSuccessfully, emailAddress) = await _emailSender.SendEmailFromTemplateAsync(user.Email, "Two-Factor Authentication Token", "TwoFactorToken", token);

                        // Calculate token validity
                        var tokenValidUntil = DateTime.UtcNow.Add(_tokenOptions.Value.TokenLifespan);

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
                else if (result.IsLockedOut)
                {
                    if (user.IsBackupPassword)
                    {
                        // Check backup password
                        var isBackupPasswordValid = _userManager.PasswordHasher.VerifyHashedPassword(user, user.BackupPasswordHash, loginDto.Password) == PasswordVerificationResult.Success;
                        if (isBackupPasswordValid)
                        {
                            // Sign in the user
                            await _signInManager.SignInAsync(user, isPersistent: false);

                            var loginResponse = await HandleSuccessfulLogin(user, loginDto);

                            if (!loginResponse.Success)
                            {
                                return loginResponse;
                            }

                            return loginResponse;
                        }
                        else
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
                    }

                 
                }
                else if (user.IsBackupPassword)
                {
                    // Check backup password
                    var isBackupPasswordValid = _userManager.PasswordHasher.VerifyHashedPassword(user, user.BackupPasswordHash, loginDto.Password) == PasswordVerificationResult.Success;
                    if (isBackupPasswordValid)
                    {
                        // Sign in the user
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        var loginResponse = await HandleSuccessfulLogin(user, loginDto);

                        if (!loginResponse.Success)
                        {
                            return loginResponse;
                        }

                        return loginResponse;
                    }
                    else
                    {
                        return new Response<LoginDto>
                        {
                            Success = false,
                            Message = "Invalid password",
                            Data = loginDto
                        };
                    }
                }
                else if (result.IsNotAllowed)
                {
                    return new Response<LoginDto>
                    {
                        Success = false,
                        Message = "Login not allowed. Please confirm your email or complete any required steps.",
                        Data = loginDto
                    };
                }
                else
                {
                    return new Response<LoginDto>
                    {
                        Success = false,
                        Message = "Invalid password",
                        Data = loginDto
                    };
                }
            }

            else if(result.Succeeded)
            {
                var loginResponse = await HandleSuccessfulLogin(user, loginDto);

                if (!loginResponse.Success)
                {
                    return loginResponse;
                }

                return loginResponse;
            }

            return new Response<LoginDto>
            {
                Success = false,
                Message = "No Message",
                Data = loginDto
            };

        }




        private async Task<LoginResponse<LoginDto>> HandleSuccessfulLogin(ApplicationUser user, LoginDto loginDto)
        {
            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            // Generate token and refresh token
            var (tokenString, refreshToken, expirationTime) = GenerateTokenAndRefreshToken(user, userAgent);

            // Save login information to database
            await SaveLoginInfoToDatabase(user.Id, user.UserName, tokenString, refreshToken, userAgent, expirationTime);

            return new LoginResponse<LoginDto>
            {
                Success = true,
                Message = "Login successful",
                Data = loginDto,
                AccessToken = tokenString,
                RefreshToken = refreshToken
            };
        }



        private Response<LoginDto> GetLockoutResponse(ApplicationUser user, LoginDto loginDto)
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

        private async Task<Response<LoginDto>> HandleTwoFactorAuthentication(ApplicationUser user, LoginDto loginDto)
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

                var (sentSuccessfully, emailAddress) = await _emailSender.SendEmailFromTemplateAsync(user.Email, "Two-Factor Authentication Token", "TwoFactorToken", token);

                var tokenLifespan = _tokenOptions.Value.TokenLifespan;
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




        private (string, string, DateTime) GenerateTokenAndRefreshToken(ApplicationUser user, string userAgent)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.SymmetricSecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var userData = _userRepo.GetUserWithRolesAndRolesClaimsAsync(user.UserName);


            var userInfoencrypt = Encryptor.EncryptStringAES(UtilityService.JsonData(userData.Result.Data));



            var tokenOptions = new JwtSecurityToken(
                issuer: AppSettings.ApiValidIssuer,
                audience: AppSettings.ApiValidAudience,
                claims: new List<Claim>() {
                        new Claim("userinfo", userInfoencrypt),
                        new Claim(ClaimTypes.UserData, userAgent),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                },
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


            var refreshToken = GenerateRefreshToken();

            return (tokenString, refreshToken, tokenOptions.ValidTo);
        }




        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }


        private async Task SaveLoginInfoToDatabase(Guid userId, string username, string token, string refreshToken, string userAgent, DateTime expirationTime)
        {
            var loginInfo = new UserLoginInformation
            {
                UserId = userId,
                UserName = username,
                Token = token,
                RefreshToken = refreshToken,
                UserAgent = userAgent,
                LoginTime = DateTime.UtcNow.AddHours(6),
                ExpirationTime = expirationTime.AddHours(6)
            };

            _context.tblUserLoginInformations.Add(loginInfo);
            await _context.SaveChangesAsync();
        }










        public async Task<Response<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var response = new Response<RefreshTokenResponseDto>();

            var refreshToken = refreshTokenDto.RefreshToken.Trim();
            var loginInfo = await _context.tblUserLoginInformations.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (loginInfo == null)
            {
                response.Success = false;
                response.Message = "Invalid refresh token";
                return response;
            }

            var userAgentFromDb = loginInfo.UserAgent;
            var currentUserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            if (userAgentFromDb != currentUserAgent)
            {
                response.Success = false;
                response.Message = "User-Agent mismatch";
                return response;
            }

            var userData = await _userManager.FindByNameAsync(loginInfo.UserName);
            if (userData == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }

            var (newTokenString, newRefreshToken, expirationTime) = GenerateTokenAndRefreshToken(userData, currentUserAgent);

            try
            {
                loginInfo.Token = newTokenString;
                loginInfo.RefreshToken = newRefreshToken;
                loginInfo.LoginTime = DateTime.UtcNow.AddHours(6);
                loginInfo.ExpirationTime = expirationTime;

                await _context.SaveChangesAsync();

                // Save login information to database
                await SaveLoginInfoToDatabase(userData.Id, userData.UserName, newTokenString, newRefreshToken, currentUserAgent, expirationTime);

                var refreshTokenResponseDto = new RefreshTokenResponseDto
                {
                    AccessToken = newTokenString,
                    RefreshToken = newRefreshToken
                };

                response.Success = true;
                response.Data = refreshTokenResponseDto;
                response.Message = "Token refreshed successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to refresh token: {ex.Message}";
            }

            return response;
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











        public async Task<Response<bool>> LogoutAsync(LogoutDto logoutDto)
        {
            var response = new Response<bool>();

            var loginInfo = await _context.tblUserLoginInformations.FirstOrDefaultAsync(u => u.RefreshToken == logoutDto.RefreshToken.Trim());
            if (loginInfo != null)
            {
                loginInfo.LogoutTime = DateTime.UtcNow.AddHours(6);
                await _context.SaveChangesAsync();
                response.Data = true;
                response.Message = "You have successfully logged out. Thank you!";
                response.Success = true;
            }
            else
            {
                response.Data = false;
                response.Message = "Invalid refresh token.";
                response.Success = false;
            }

            return response;
        }





    }
}
