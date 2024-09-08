using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Control_Panel.Identity;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Identity.Role;
using Shared.DTO.Control_Panel.Identity.Role.Create;
using Shared.DTO.Control_Panel.Identity.Role.Result;
using System.Security.Claims;


namespace BLL.Repository.Identity.Role.Implementation
{
    public class RoleRepo : IRoleRepo
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleRepo(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response<CreateRoleResultDto>> CreateRoleAsync(CreateRoleRequestDto createRoleDto)
        {
            try
            {
                var role = _mapper.Map<ApplicationRole>(createRoleDto);
                var result = await _roleManager.CreateAsync(role);

                var createRoleResult = _mapper.Map<CreateRoleResultDto>(role);

                if (result.Succeeded)
                {
                    return new Response<CreateRoleResultDto>
                    {
                        Success = true,
                        Message = $"Role '{createRoleDto.Name}' created successfully.",
                        Data = createRoleResult
                    };
                }
                else
                {
                    return new Response<CreateRoleResultDto>
                    {
                        Success = false,
                        Message = $"Failed to create role '{createRoleDto.Name}'."
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<CreateRoleResultDto>
                {
                    Success = false,
                    Message = $"Error creating role '{createRoleDto.Name}': {ex.Message}"
                };
            }
        }



        public async Task<Response<string>> UpdateRoleAsync(UpdateRoleDto updateRoleDto)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(updateRoleDto.RoleId);

                if (role == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Role with ID '{updateRoleDto.RoleId}' not found.",
                        Data = null
                    };
                }

                role.Name = updateRoleDto.NewRoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return new Response<string>
                    {
                        Success = true,
                        Message = $"Role '{updateRoleDto.RoleId}' updated successfully.",
                        Data = role.Id.ToString() 
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Failed to update role '{updateRoleDto.RoleId}'.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Error updating role '{updateRoleDto.RoleId}': {ex.Message}",
                    Data = null
                };
            }
        }



        public async Task<Response<string>> DeleteRoleAsync(string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Role with Name '{roleName}' not found.",
                        Data = null
                    };
                }

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return new Response<string>
                    {
                        Success = true,
                        Message = $"Role '{roleName}' deleted successfully.",
                        Data = role.Id.ToString() 
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Failed to delete role '{roleName}'.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Error deleting role '{roleName}': {ex.Message}",
                    Data = null
                };
            }
        }



        public async Task<Response<string>> AddRangeAsync(IEnumerable<CreateRoleRequestDto> roleNames)
        {
            try
            {
                var successes = new List<string>();
                var failures = new List<string>();

                foreach (var roleName in roleNames)
                {
                    //var role = new ApplicationRole { Name = roleName };
                    var role = _mapper.Map<ApplicationRole>(roleName);
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        successes.Add(role.Id.ToString());
                    }
                    else
                    {
                        failures.Add($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }

                if (failures.Any())
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Error adding roles: {string.Join("; ", failures)}",
                        Data = null
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Success = true,
                        Message = $"{successes.Count} roles added successfully.",
                        Data = string.Join(",", successes)
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Exception adding roles: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<Response<List<ApplicationRoleDto>>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var rolesDto = _mapper.Map<List<ApplicationRoleDto>>(roles);
                var rolesCount = rolesDto.Count;

                if (rolesCount > 0)
                {
                    return new Response<List<ApplicationRoleDto>>
                    {
                        Success = true,
                        Message = $"Roles retrieved successfully. Total roles count: {rolesCount}.",
                        Data = rolesDto
                    };
                }
                else
                {
                    return new Response<List<ApplicationRoleDto>>
                    {
                        Success = true,
                        Message = "No roles found.",
                        Data = rolesDto
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<ApplicationRoleDto>>
                {
                    Success = false,
                    Message = $"Error retrieving roles: {ex.Message}",
                    Data = null
                };
            }
        }






        public async Task<Response<string>> AssignRolesToUserAsync(AssignRolesDto assignRolesDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(assignRolesDto.UserId);
                if (user == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"User with ID '{assignRolesDto.UserId}' not found.",
                        Data = null
                    };
                }

                // Get existing roles assigned to the user
                var existingRoles = await _userManager.GetRolesAsync(user);

                // Roles to add
                var rolesToAdd = assignRolesDto.RoleIds.Except(existingRoles);

                // Roles to remove
                var rolesToRemove = existingRoles.Except(assignRolesDto.RoleIds);

                // Add roles
                foreach (var roleName in rolesToAdd)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
                        if (!addToRoleResult.Succeeded)
                        {
                            return new Response<string>
                            {
                                Success = false,
                                Message = $"Failed to assign role '{roleName}' to user '{user.UserName}'.",
                                Data = null
                            };
                        }
                    }
                }

                // Remove roles
                foreach (var roleName in rolesToRemove)
                {
                    var removeFromRoleResult = await _userManager.RemoveFromRoleAsync(user, roleName);
                    if (!removeFromRoleResult.Succeeded)
                    {
                        return new Response<string>
                        {
                            Success = false,
                            Message = $"Failed to remove role '{roleName}' from user '{user.UserName}'.",
                            Data = null
                        };
                    }
                }

                return new Response<string>
                {
                    Success = true,
                    Message = $"Roles assigned to user '{user.UserName}' successfully.",
                    Data = user.UserName 
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Error assigning roles to user: {ex.Message}",
                    Data = null
                };
            }
        }







        public async Task<Response<string>> AddRoleClaimsAsync(RoleClaimsDto roleClaimsDto)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleClaimsDto.RoleId.ToString());
                if (role == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Role with ID '{roleClaimsDto.RoleId}' not found.",
                        Data = null
                    };
                }

                foreach (var claimDto in roleClaimsDto.RoleClaims)
                {
                    var claim = new Claim(claimDto.ClaimType, claimDto.ClaimValue);

                    var result = await _roleManager.AddClaimAsync(role, claim);
                    if (!result.Succeeded)
                    {
                        return new Response<string>
                        {
                            Success = false,
                            Message = $"Failed to add claim '{claim.Type}' to role '{role.Name}'.",
                            Data = null
                        };
                    }
                }

                return new Response<string>
                {
                    Success = true,
                    Message = $"Claims added to role '{role.Name}' successfully.",
                    Data = role.Id.ToString() // Return role ID or name as needed
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Error adding claims to role: {ex.Message}",
                    Data = null
                };
            }
        }



        public async Task<Response<string>> UpdateRoleClaimsAsync(RoleClaimsDto roleClaimsDto)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleClaimsDto.RoleId.ToString());
                if (role == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Role with ID '{roleClaimsDto.RoleId}' not found.",
                        Data = null
                    };
                }

                // Remove existing claims
                var existingClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in existingClaims)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }

                // Add new claims
                foreach (var claimDto in roleClaimsDto.RoleClaims)
                {
                    var claim = new Claim(claimDto.ClaimType, claimDto.ClaimValue);

                    var result = await _roleManager.AddClaimAsync(role, claim);
                    if (!result.Succeeded)
                    {
                        return new Response<string>
                        {
                            Success = false,
                            Message = $"Failed to update claim '{claim.Type}' for role '{role.Name}'.",
                            Data = null
                        };
                    }
                }

                return new Response<string>
                {
                    Success = true,
                    Message = $"Claims updated for role '{role.Name}' successfully.",
                    Data = role.Id.ToString() // Return role ID or name as needed
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Error updating claims for role: {ex.Message}",
                    Data = null
                };
            }
        }



        public async Task<Response<string>> RemoveRoleClaimsAsync(RoleClaimsDto roleClaimsDto)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleClaimsDto.RoleId.ToString());
                if (role == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = $"Role with ID '{roleClaimsDto.RoleId}' not found.",
                        Data = null
                    };
                }

                foreach (var claimDto in roleClaimsDto.RoleClaims)
                {
                    var claim = new Claim(claimDto.ClaimType, claimDto.ClaimValue);

                    var result = await _roleManager.RemoveClaimAsync(role, claim);
                    if (!result.Succeeded)
                    {
                        return new Response<string>
                        {
                            Success = false,
                            Message = $"Failed to remove claim '{claim.Type}' from role '{role.Name}'.",
                            Data = null
                        };
                    }
                }

                return new Response<string>
                {
                    Success = true,
                    Message = $"Claims removed from role '{role.Name}' successfully.",
                    Data = role.Id.ToString() // Return role ID or name as needed
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Success = false,
                    Message = $"Error removing claims from role: {ex.Message}",
                    Data = null
                };
            }
        }






        public async Task<Response<List<RoleWithClaimsDto>>> GetRolesWithClaimsAsync()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var rolesWithClaims = new List<RoleWithClaimsDto>();

                foreach (var role in roles)
                {
                    var claims = await _roleManager.GetClaimsAsync(role);
                    var roleWithClaimsDto = new RoleWithClaimsDto
                    {
                        RoleId = role.Id,
                        RoleName = role.Name,
                        Claims = claims.Select(c => new ClaimDto { ClaimType = c.Type, ClaimValue = c.Value }).ToList()
                    };
                    rolesWithClaims.Add(roleWithClaimsDto);
                }

                if (rolesWithClaims.Any())
                {
                    return new Response<List<RoleWithClaimsDto>>
                    {
                        Success = true,
                        Message = $"Roles with claims retrieved successfully. Total count: {rolesWithClaims.Count}.",
                        Data = rolesWithClaims
                    };
                }
                else
                {
                    return new Response<List<RoleWithClaimsDto>>
                    {
                        Success = true,
                        Message = "No roles found.",
                        Data = rolesWithClaims
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<List<RoleWithClaimsDto>>
                {
                    Success = false,
                    Message = $"Error retrieving roles with claims: {ex.Message}",
                    Data = null
                };
            }
        }

    }
}

