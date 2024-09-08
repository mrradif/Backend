using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.DTO.Control_Panel.Administration.User_Creation_Config;



namespace BLL.Service.Control_Panel.User_Creation_Config_Service
{

    public class GetUserCreationConfigService
    {
        private readonly IGetGenericRepo<UserCreationConfig, ControlPanelDbContext, GetUserCreationConfigResultDto, long> _getUserCreationConfigRepository;
        private readonly IMapper _mapper;

        public GetUserCreationConfigService(IGetGenericRepo<UserCreationConfig, ControlPanelDbContext, GetUserCreationConfigResultDto, long> getUserCreattionConfigRepository, IMapper mapper)
        {
            _getUserCreationConfigRepository = getUserCreattionConfigRepository ?? throw new ArgumentNullException(nameof(getUserCreattionConfigRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public async Task<Response<GetUserCreationConfigResultDto>> GetActiveUserConfigAsync()
        {
            //Expression<Func<UserCreationConfig, bool>> predicate = userCreationConfig =>
            //   (userCreationConfig.IsActive == true);

            //return await _getUserCreationConfigRepository.GetSingleLastAsync(predicate);


            return await _getUserCreationConfigRepository.GetSingleAsync(e => e.IsActive == true);
        }


    }

}
