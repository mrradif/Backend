using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Control_Panel.Button;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Buttons;
using System.Linq.Expressions;


namespace BLL.Service.Control_Panel.Button_Service
{
    public class GetButtonService
    {
        private readonly IGetGenericRepo<Button, ControlPanelDbContext, GetButtonResultViewModel, long> _getButtonRepository;
        private readonly IMapper _mapper;

        public GetButtonService(
            IGetGenericRepo<Button, ControlPanelDbContext, GetButtonResultViewModel, long> getButtonRepository,
            IMapper mapper
        )
        {
            _getButtonRepository = getButtonRepository ?? throw new ArgumentNullException(nameof(getButtonRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetAllButtonsAsync()
        {
            var response = await _getButtonRepository.GetAllAsync();
            return response;
        }



        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetButtonsAsync Overload Start

        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetButtonsAsync()
        {
            var response = await _getButtonRepository.GetAllAsync(b => b.IsDeleted == false);
            return response;
        }

        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetButtonsAsync(Expression<Func<Button, bool>> predicate)
        {
            var response = await _getButtonRepository.GetAllAsync(predicate);
            return response;
        }

        // ................................................ GetButtonsAsync Overload End
        // -------------------------------------------------------------------------------- >>>




        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetButtonsByCriteriaAsync Overload Start

        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetButtonsByCriteriaAsync(ButtonSearchCriteria criteria)
        {
            Expression<Func<Button, bool>> predicate = button =>
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));

            var response = await _getButtonRepository.GetAllAsync(predicate);
            return response;
        }

        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetButtonsByCriteriaAsync(Expression<Func<Button, bool>> predicate)
        {
            return await _getButtonRepository.GetAllAsync(predicate);
        }

        // ................................................ GetButtonsByCriteriaAsync Overload End
        // -------------------------------------------------------------------------------- >>>




        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetButtonsWithOrderingAsync Overload Start

        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetButtonsWithOrderingAsync(ButtonSearchCriteria criteria)
        {
            Expression<Func<Button, bool>> predicate = button =>
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));

            Func<IQueryable<Button>, IOrderedQueryable<Button>> orderBy = q => q.OrderByDescending(button => button.ButtonId);

            var response = await _getButtonRepository.GetAllAsync(predicate, orderBy);
            return response;
        }

        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetButtonsWithOrderingAsync(ButtonSearchCriteria criteria, Func<IQueryable<Button>, IOrderedQueryable<Button>> orderBy = null)
        {
            Expression<Func<Button, bool>> predicate = button =>
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));

            var response = await _getButtonRepository.GetAllAsync(predicate, orderBy);
            return response;
        }


        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetButtonsWithOrderingAsync(Expression<Func<Button, bool>> predicate, Func<IQueryable<Button>, IOrderedQueryable<Button>> orderBy = null)
        {
            var response = await _getButtonRepository.GetAllAsync(predicate, orderBy);
            return response;
        }


        public async Task<Response<IEnumerable<GetButtonResultViewModel>>> GetButtonsWithOrdering2Async(ButtonSearchCriteria criteria)
        {
            Expression<Func<Button, bool>> predicate = app =>
                (string.IsNullOrEmpty(criteria.ButtonName) || app.ButtonName.Contains(criteria.ButtonName)) &&
                (!criteria.IsActive || app.IsActive);

            Func<IQueryable<Button>, IOrderedQueryable<Button>> orderBy = BuildOrderBy(criteria);

            var response = await _getButtonRepository.GetAllAsync(predicate, orderBy);
            return response;
        }

        private Func<IQueryable<Button>, IOrderedQueryable<Button>> BuildOrderBy(ButtonSearchCriteria criteria)
        {
            return q => q.OrderByDescending(app => app.ButtonId);
        }


        // ................................................ GetButtonsWithOrderingAsync Overload End
        // -------------------------------------------------------------------------------- >>>


        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        public async Task<Response<GetButtonResultViewModel>> GetSingleButtonByIdAsync(long buttonId)
        {
            return await _getButtonRepository.GetSingleAsync(buttonId);
        }


        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetSingleButtonByCriteriaAsync Overload Start

        public async Task<Response<GetButtonResultViewModel>> GetSingleButtonByCriteriaAsync(ButtonSearchCriteria criteria)
        {
            Expression<Func<Button, bool>> predicate = button =>
                (string.IsNullOrEmpty(criteria.ButtonName) || button.ButtonName.Contains(criteria.ButtonName));

            var response = await _getButtonRepository.GetSingleAsync(predicate);
            return response;
        }

        public async Task<Response<GetButtonResultViewModel>> GetSingleButtonByCriteriaAsync(Expression<Func<Button, bool>> predicate)
        {
            return await _getButtonRepository.GetSingleAsync(predicate);
        }

        // ................................................ GetSingleButtonByCriteriaAsync Overload End
        // -------------------------------------------------------------------------------- >>>


        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>






    }
}
