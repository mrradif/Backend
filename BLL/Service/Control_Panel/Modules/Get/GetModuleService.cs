using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Modules;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Modules.Get
{
    public class GetModuleService
    {
        private readonly IGetGenericRepo<Module, ControlPanelDbContext, GetModuleResultViewModel, long> _getModuleRepository;
        private readonly IMapper _mapper;

        public GetModuleService(
            IGetGenericRepo<Module, ControlPanelDbContext, GetModuleResultViewModel, long> getModuleRepository,
            IMapper mapper
        )
        {
            _getModuleRepository = getModuleRepository ?? throw new ArgumentNullException(nameof(getModuleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetAllModulesAsync()
        {
            var response = await _getModuleRepository.GetAllAsync();
            return response;
        }

        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetModulesAsync Overload Start

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetModulesAsync()
        {
            var response = await _getModuleRepository.GetAllAsync(e => e.IsDeleted == false);
            return response;
        }

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetModulesAsync(Expression<Func<Module, bool>> predicate)
        {
            var response = await _getModuleRepository.GetAllAsync(predicate);
            return response;
        }

        // ................................................ GetModulesAsync Overload End
        // -------------------------------------------------------------------------------- >>>




        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetModulesByCriteriaAsync Overload Start

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetModulesByCriteriaAsync(ModuleSearchCriteriaWithPagination criteria)
        {
            Expression<Func<Module, bool>> predicate = module =>
                (string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || module.IsActive);

            var response = await _getModuleRepository.GetAllAsync(predicate);
            return response;
        }

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetModulesByCriteriaAsync(Expression<Func<Module, bool>> predicate)
        {
            return await _getModuleRepository.GetAllAsync(predicate);
        }

        // ................................................ GetModulesByCriteriaAsync Overload End
        // -------------------------------------------------------------------------------- >>>


        // -------------------------------------------------------------------------------- >>>
        // ................................................ GetModulesWithOrderingAsync Overload Start

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetModulesWithOrderingAsync(ModuleSearchCriteriaWithPagination criteria)
        {
            Expression<Func<Module, bool>> predicate = module =>
                (string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || module.IsActive);

            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderByDescending(module => module.ModuleId);

            var response = await _getModuleRepository.GetAllAsync(predicate, orderBy);
            return response;
        }

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetModulesWithOrderingAsync(ModuleSearchCriteriaWithPagination criteria, Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = null)
        {
            Expression<Func<Module, bool>> predicate = module =>
                (string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || module.IsActive);

            var response = await _getModuleRepository.GetAllAsync(predicate, orderBy);
            return response;
        }

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetModulesWithOrderingAsync(Expression<Func<Module, bool>> predicate, Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = null)
        {
            var response = await _getModuleRepository.GetAllAsync(predicate, orderBy);
            return response;
        }

        public async Task<Response<IEnumerable<GetModuleResultViewModel>>> GetModulesWithOrdering2Async(ModuleSearchCriteriaWithPagination criteria)
        {
            Expression<Func<Module, bool>> predicate = module =>
                (string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || module.IsActive);

            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = BuildOrderBy(criteria);

            var response = await _getModuleRepository.GetAllAsync(predicate, orderBy);
            return response;
        }

        private Func<IQueryable<Module>, IOrderedQueryable<Module>> BuildOrderBy(ModuleSearchCriteriaWithPagination criteria)
        {
            return q => q.OrderByDescending(module => module.ModuleId);
        }

        // ................................................ GetModulesWithOrderingAsync Overload End
        // -------------------------------------------------------------------------------- >>>


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        public async Task<Response<GetModuleResultViewModel>> GetSingleModuleByIdAsync(long moduleId)
        {
            return await _getModuleRepository.GetSingleAsync(moduleId);
        }

        public async Task<Response<GetModuleResultViewModel>> GetSingleModuleByCriteriaAsync(ModuleSearchCriteriaWithPagination criteria)
        {
            Expression<Func<Module, bool>> predicate = module =>
                (string.IsNullOrEmpty(criteria.ModuleName) || module.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || module.IsActive);

            var response = await _getModuleRepository.GetSingleAsync(predicate);
            return response;
        }

        public async Task<Response<GetModuleResultViewModel>> GetSingleModuleByCriteriaAsync(Expression<Func<Module, bool>> predicate)
        {
            return await _getModuleRepository.GetSingleAsync(predicate);
        }

        // ................................................ Get Single End
        // ---------------------------------------------------------------------------------------------------------------------- >>>
    }
}
