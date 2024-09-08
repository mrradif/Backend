using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.Helper.Pagination;
using Shared.View.Control_Panel.Modules;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Modules.Get
{
    public class GetModuleWithPaginationService
    {
        private readonly IMapper _mapper;

        private readonly IGetGenericRepoWithPagination<Module, ControlPanelDbContext, GetModuleResultViewModel, long> _getModuleWithPaginationRepository;

        private readonly IGetGenericRepoWithPagination<Module, ControlPanelDbContext, GetModuleWithMainMenuResultViewModel, long> _getModuleWithPaginationWithMainMenuRepository;

        public GetModuleWithPaginationService(
            IMapper mapper,
            IGetGenericRepoWithPagination<Module, ControlPanelDbContext, GetModuleResultViewModel, long> getModuleWithPaginationRepository,
            IGetGenericRepoWithPagination<Module, ControlPanelDbContext, GetModuleWithMainMenuResultViewModel, long> getModuleWithPaginationWithMainMenuRepository
            )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _getModuleWithPaginationRepository = getModuleWithPaginationRepository;
            _getModuleWithPaginationWithMainMenuRepository = getModuleWithPaginationWithMainMenuRepository;
        }

        // With Pagination
        public async Task<PaginationResponse<PaginatedList<GetModuleResultViewModel>>> GetModulesWithPaginationAsync(int pageNumber, int pageSize)
        {
            var response = await _getModuleWithPaginationRepository.GetAllAsync(pageNumber, pageSize);
            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetModuleResultViewModel>>> GetModulesWithPaginationAsync(ModuleSearchCriteriaWithPagination criteria, int pageNumber, int pageSize)
        {
            Expression<Func<Module, bool>> predicate = mod =>
                (string.IsNullOrEmpty(criteria.ModuleName) || mod.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || mod.IsActive);

            var response = await _getModuleWithPaginationRepository.GetAllAsync(predicate, pageNumber, pageSize);
            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetModuleResultViewModel>>> GetModulesWithPaginationWithOrderByAsync(ModuleSearchCriteriaWithPagination criteria, int pageNumber, int pageSize)
        {
            try
            {
                Expression<Func<Module, bool>> predicate = mod =>
                    (string.IsNullOrEmpty(criteria.ModuleName) || mod.ModuleName.Contains(criteria.ModuleName)) &&
                    (!criteria.IsActive || mod.IsActive);

                Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(mod => mod.ModuleId);

                var response = await _getModuleWithPaginationRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize);

                if (response.Success)
                {
                    return new PaginationResponse<PaginatedList<GetModuleResultViewModel>>(response.Data);
                }
                return new PaginationResponse<PaginatedList<GetModuleResultViewModel>>(response.Message);
            }
            catch (Exception ex)
            {
                return new PaginationResponse<PaginatedList<GetModuleResultViewModel>>(ex.Message);
            }
        }

        public async Task<PaginationResponse<PaginatedList<GetModuleResultViewModel>>> GetModulesWithPaginationWithOrderByAsync(ModuleSearchCriteriaWithPagination criteria, int pageNumber, int pageSize, Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy)
        {
            try
            {
                Expression<Func<Module, bool>> predicate = mod =>
                    (string.IsNullOrEmpty(criteria.ModuleName) || mod.ModuleName.Contains(criteria.ModuleName)) &&
                    (!criteria.IsActive || mod.IsActive);

                if (orderBy == null)
                {
                    orderBy = q => q.OrderBy(mod => mod.ModuleId);
                }

                var response = await _getModuleWithPaginationRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize);

                if (response.Success)
                {
                    return new PaginationResponse<PaginatedList<GetModuleResultViewModel>>(response.Data);
                }
                return new PaginationResponse<PaginatedList<GetModuleResultViewModel>>(response.Message);
            }
            catch (Exception ex)
            {
                return new PaginationResponse<PaginatedList<GetModuleResultViewModel>>(ex.Message);
            }
        }

        // With Pagination With Includes
        public async Task<PaginationResponse<PaginatedList<GetModuleWithMainMenuResultViewModel>>> GetModulesWithPaginationAndIncludesAsync(int pageNumber, int pageSize)
        {
            var response = await _getModuleWithPaginationWithMainMenuRepository.GetAllAsync(pageNumber, pageSize, m => m.MainMenus);
            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetModuleWithMainMenuResultViewModel>>> GetModulesWithPaginationAndIncludesWithOrderByAsync(int pageNumber, int pageSize)
        {
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(mod => mod.ModuleName);

            var response = await _getModuleWithPaginationWithMainMenuRepository.GetAllAsync(pageNumber, pageSize, orderBy, m => m.MainMenus);

            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetModuleWithMainMenuResultViewModel>>> GetModulesWithPaginationAndIncludesWithOrderByAsync(int pageNumber, int pageSize, Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy)
        {
            if (orderBy == null)
            {
                orderBy = q => q.OrderBy(mod => mod.ModuleId);
            }

            var response = await _getModuleWithPaginationWithMainMenuRepository.GetAllAsync(pageNumber, pageSize, orderBy, m => m.MainMenus);

            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetModuleWithMainMenuResultViewModel>>> GetModulesWithPaginationAndIncludesAsync(ModuleSearchCriteriaWithPagination criteria, int pageNumber, int pageSize)
        {
            Expression<Func<Module, bool>> predicate = mod =>
                (string.IsNullOrEmpty(criteria.ModuleName) || mod.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || mod.IsActive);

            var response = await _getModuleWithPaginationWithMainMenuRepository.GetAllAsync(predicate, pageNumber, pageSize, m => m.MainMenus);
            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetModuleWithMainMenuResultViewModel>>> GetModulesWithPaginationIncludesAndOrderingAsync(ModuleSearchCriteriaWithPagination criteria, int pageNumber, int pageSize, Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy)
        {
            Expression<Func<Module, bool>> predicate = mod =>
                (string.IsNullOrEmpty(criteria.ModuleName) || mod.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || mod.IsActive);

            if (orderBy == null)
            {
                orderBy = q => q.OrderBy(mod => mod.ModuleId);
            }

            var response = await _getModuleWithPaginationWithMainMenuRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize, m => m.MainMenus);

            return response;
        }

        public async Task<PaginationResponse<PaginatedList<GetModuleWithMainMenuResultViewModel>>> GetModulesWithPaginationIncludesAndOrderingAsync(ModuleSearchCriteriaWithPagination criteria, int pageNumber, int pageSize)
        {
            Expression<Func<Module, bool>> predicate = mod =>
                (string.IsNullOrEmpty(criteria.ModuleName) || mod.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || mod.IsActive);

            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(mod => mod.ModuleId);

            var response = await _getModuleWithPaginationWithMainMenuRepository.GetAllAsync(predicate, orderBy, pageNumber, pageSize, m => m.MainMenus);

            return response;
        }
    }
}
