using DAL.Context.Control_Panel;
using Shared.DTO.Control_Panel.Administration.Update;
using Shared.DTO.Common;
using System.Linq.Expressions;
using Shared.DTO.Control_Panel.Administration.Company;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using BLL.Repository.Generic.Interface.Put;

namespace BLL.Service.Control_Panel.Update
{
    public class UpdateCompanyService
    {
        private readonly IPutGenericRepo<Company, ControlPanelDbContext, UpdateCompanyDto, CreateCompanyResultDto> _putRepo;

        public UpdateCompanyService(IPutGenericRepo<Company, ControlPanelDbContext, UpdateCompanyDto, CreateCompanyResultDto> putRepo)
        {
            _putRepo = putRepo;
        }

        public async Task<UpdateResponse<CreateCompanyResultDto>> UpdateCompanyAsync(UpdateCompanyDto updateCompanyDto)
        {
            var user = UserHelper.AppUser();
            if (user != null)
            {
                updateCompanyDto.UpdatedBy = user.EmployeeId;
                updateCompanyDto.UpdatedDate = DateTime.UtcNow;
            }

            return await _putRepo.UpdateAsync(updateCompanyDto);
        }

        public async Task<UpdateResponse<UpdateRangeResult<CreateCompanyResultDto>>> UpdateCompaniesAsync(IEnumerable<UpdateCompanyDto> updateCompanyDtos)
        {
            var user = UserHelper.AppUser();

            if (user != null)
            {
                foreach (var dto in updateCompanyDtos)
                {
                    dto.UpdatedBy = user.EmployeeId;
                    dto.UpdatedDate = DateTime.UtcNow;
                }
            }

            var predicates = updateCompanyDtos.Select(CreatePredicate).ToList();

            return await _putRepo.UpdateRangeAsync(updateCompanyDtos, predicates);
        }

        private Expression<Func<Company, bool>> CreatePredicate(UpdateCompanyDto dto)
        {
            return e =>
                e.CompanyId == dto.CompanyId ||
                e.ComUniqueId == dto.ComUniqueId ||
                e.CompanyCode == dto.CompanyCode ||
                e.CompanyName == dto.CompanyName;
        }
    }
}
