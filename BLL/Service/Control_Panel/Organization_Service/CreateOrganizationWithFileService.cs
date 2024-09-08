using Shared.DTO.Common;
using DAL.Context.Control_Panel;
using System.Linq.Expressions;
using BLL.Repository.Generic.Interface.Post;
using Shared.Helper.File;
using Shared.DTO.Control_Panel.Administration.Organization;
using Shared.Domain.Control_Panel.Administration.Org_Config;


public class CreateOrganizationWithFileService
{
    private readonly IPostGenericRepo<Organization, ControlPanelDbContext, CreateOrganizationRequestDto, CreateOrganizationResultDto> _postRepo;
    private readonly FileManager _fileManager;

    public CreateOrganizationWithFileService(IPostGenericRepo<Organization, ControlPanelDbContext, CreateOrganizationRequestDto, CreateOrganizationResultDto> postRepo, FileManager fileManager)
    {
        _postRepo = postRepo;
        _fileManager = fileManager;
    }

    public async Task<Response<CreateOrganizationResultDto>> AddOrganizationAsync(CreateOrganizationRequestWithFileDto formDto)
    {
    
        try
        {
            var existsResponse = await _postRepo.CheckExistsAsync(o => o.OrgUniqueId == formDto.OrgUniqueId);
            if (existsResponse.Success && existsResponse.Data != null)
            {
                return new Response<CreateOrganizationResultDto>
                {
                    Success = false,
                    Message = "Organization already exists",
                    Data = existsResponse.Data
                };
            }

            if (!FileHelper.IsValidFile(formDto.OrgPic) || !FileHelper.IsValidFile(formDto.ReportPic))
            {
                return new Response<CreateOrganizationResultDto>
                {
                    Success = false,
                    Message = "Invalid file format. Only JPEG, PNG, and GIF are allowed."
                };
            }

            var createOrganizationDto = MapToCreateDto(formDto);

            //var user = UserHelper.AppUser();
            //if (user != null)
            //{
            //    createOrganizationDto.CreatedBy = user.EmployeeId;
            //    createOrganizationDto.CreatedDate = DateTime.UtcNow;
            //}

            var addResult = await _postRepo.AddAsync(createOrganizationDto);

            if (addResult.Success)
            {
                return addResult;
            }
            else
            {
                return addResult;
            }
        }
        catch (Exception ex)
        {
            return new Response<CreateOrganizationResultDto>
            {
                Success = false,
                Message = $"Error occurred while adding organization: {ex.Message}"
            };
        }
    
    }



    private CreateOrganizationRequestDto MapToCreateDto(CreateOrganizationRequestWithFileDto formDto)
    {
        var createDto = new CreateOrganizationRequestDto
        {
            OrgUniqueId = formDto.OrgUniqueId,
            OrgCode = formDto.OrgCode,
            OrganizationName = formDto.OrganizationName,
            ShortName = formDto.ShortName,
            SiteThumbnailPath = formDto.SiteThumbnailPath,
            Address = formDto.Address,
            Email = formDto.Email,
            PhoneNumber = formDto.PhoneNumber,
            MobileNumber = formDto.MobileNumber,
            Fax = formDto.Fax,
            Website = formDto.Website,
            ContractStartDate = formDto.ContractStartDate,
            ContractExpireDate = formDto.ContractExpireDate,
            Remarks = formDto.Remarks,
            OrgLogoPath = formDto.OrgLogoPath,
            ReportLogoPath = formDto.ReportLogoPath,
            AppId = formDto.AppId,
            AppName = formDto.AppName,
            StorageName = formDto.StorageName,
            IsActive = formDto.IsActive,
            //CreatedDate = DateTime.Now
        };

        if (formDto.OrgPic != null)
        {
            string orgPicPath = _fileManager.SaveFileToPhysicalDrive(formDto.OrgPic, createDto.OrganizationName).Result;
            createDto.OrgPic = FileHelper.ConvertToByteArray(formDto.OrgPic);
            createDto.OrgPicPath = orgPicPath;
            createDto.OrgPicFileName = formDto.OrgPic.FileName;
            createDto.OrgPicFileExtension = Path.GetExtension(formDto.OrgPic.FileName);
            createDto.OrgPicContentType = formDto.OrgPic.ContentType;
        }

        if (formDto.ReportPic != null)
        {
            string reportPicPath = _fileManager.SaveFileToPhysicalDrive(formDto.ReportPic, createDto.OrganizationName).Result;
            createDto.ReportPic = FileHelper.ConvertToByteArray(formDto.ReportPic);
            createDto.ReportPicPath = reportPicPath;
            createDto.ReportPicFileName = formDto.ReportPic.FileName;
            createDto.ReportPicFileExtension = Path.GetExtension(formDto.ReportPic.FileName);
            createDto.ReportPicContentType = formDto.ReportPic.ContentType;
        }

        return createDto;
    }

    private Expression<Func<Organization, bool>> CreatePredicate(CreateOrganizationRequestDto dto)
    {
        return e =>
            e.OrgUniqueId == dto.OrgUniqueId &&
            e.OrgCode == dto.OrgCode &&
            e.OrganizationName == dto.OrganizationName;
    }


}
