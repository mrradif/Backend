using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Identity.Role;
using Shared.DTO.Control_Panel.Identity.Role.Create;
using Shared.DTO.Control_Panel.Identity.Role.Result;

public interface IRoleRepo
{
    Task<Response<CreateRoleResultDto>> CreateRoleAsync(CreateRoleRequestDto createRoleDto);



    Task<Response<string>> UpdateRoleAsync(UpdateRoleDto updateRoleDto);
    Task<Response<string>> DeleteRoleAsync(string roleName);


    Task<Response<string>> AddRangeAsync(IEnumerable<CreateRoleRequestDto> roleNames);




    Task<Response<List<ApplicationRoleDto>>> GetAllRolesAsync();



    Task<Response<string>> AssignRolesToUserAsync(AssignRolesDto assignRolesDto);



    Task<Response<string>> AddRoleClaimsAsync(RoleClaimsDto roleClaimsDto);
    Task<Response<string>> UpdateRoleClaimsAsync(RoleClaimsDto roleClaimsDto);
    Task<Response<string>> RemoveRoleClaimsAsync(RoleClaimsDto roleClaimsDto);
    Task<Response<List<RoleWithClaimsDto>>> GetRolesWithClaimsAsync();


}
