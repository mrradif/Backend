
using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Applications;
using Shared.View.Control_Panel.Components;
using Shared.View.Control_Panel.Modules;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Modules.Get
{
    public class GetModuleWithApplicationService
    {
        private readonly IGetGenericRepo<Module, ControlPanelDbContext, GetModuleWithApplicationResultViewModel, long> _getModuleWithApplicationRepository;
        private readonly IMapper _mapper;

        public GetModuleWithApplicationService(
            IGetGenericRepo<Module, ControlPanelDbContext, GetModuleWithApplicationResultViewModel, long> getModuleWithApplicationRepository,
            IMapper mapper
        )
        {
            _getModuleWithApplicationRepository = getModuleWithApplicationRepository;
            _mapper = mapper;
        }


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start


        // ...............................................................GetAllModulesWithApplicationAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetAllModulesWithApplicationAsync()
        {
            var response = await _getModuleWithApplicationRepository.GetAllAsync(
                m => m.Application
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Modules retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetAllModulesWithApplicationAsync(params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithApplicationRepository.GetAllAsync(includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Modules retrieved successfully.",
                Data = result
            };
        }
        // ..........................Overload End
        // ...............................................................GetAllModulesWithApplicationAsync



        // ...............................................................GetModuleWithApplicationAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetModuleWithApplicationAsync()
        {
            var response = await _getModuleWithApplicationRepository.GetAllAsync(
                b => b.IsDeleted == false,
                b => b.Application
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }


        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetModuleWithApplicationAsync(
            Expression<Func<Module, bool>> predicate,
            params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithApplicationRepository.GetAllAsync(
                predicate,
                includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }
        // ..........................Overload End
        // ...............................................................GetModuleWithApplicationAsync






        // ...............................................................GetModuleWithApplicationCriteriaAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetModuleWithApplicationCriteriaAsync()
        {
            var response = await _getModuleWithApplicationRepository.GetAllAsync(
                m => m.IsDeleted == false,
                m => m.Application
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Modules retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetModuleWithApplicationCriteriaAsync(
            Expression<Func<Module, bool>> predicate,
            params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithApplicationRepository.GetAllAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Modules retrieved successfully.",
                Data = result
            };
        }
        // ..........................Overload End
        // ...............................................................GetModuleWithApplicationCriteriaAsync




        // ............................................................... GetModulesWithApplicationOrderedAsync
        // ........................................ Overload Start
        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetModulesWithApplicationOrderedAsync(
            (
            Expression<Func<Module, bool>> predicate,
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy,
            Expression<Func<Module, object>>[] includes) parameters)
        {
            // Destructure the tuple
            var (predicate, orderBy, includes) = parameters;

            // Fetch data from the repository
            var response = await _getModuleWithApplicationRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetModulesWithApplicationOrderedAsync()
        {
            // Example predicate: Filters active and non-deleted components
            Expression<Func<Module, bool>> predicate = c => c.IsActive && !c.IsDeleted;

            // Example order by: Orders components by their ComponentName
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(c => c.ModuleName);

            // Example includes: Includes the related Application and Module entities
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                c => c.Application
            };

            // Fetch data from the repository
            var response = await _getModuleWithApplicationRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetModuleWithApplicationResultViewModel>>> GetModulesWithApplicationOrderedAsync(ModuleSearchCriteriaWithPagination criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Module, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ModuleName) || c.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);

            // Define the order by clause (ordering by ComponentName)
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(c => c.ModuleName);

            // Define the includes (including related entities like Application and Module)
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                c => c.Application
            };

            // Fetch data from the repository
            var response = await _getModuleWithApplicationRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithApplicationResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }
        // ........................................ Overload End
        // ............................................................... GetModulesWithApplicationOrderedAsync



        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>



        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start


        public async Task<Response<GetModuleWithApplicationResultViewModel>> GetSingleModuleWithApplicationAsync(
          Expression<Func<Module, bool>> predicate,
          params Expression<Func<Module, object>>[] includes)
        {
            // Fetch the single entity using the predicate and includes
            var response = await _getModuleWithApplicationRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithApplicationResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapModuleToViewModel(response.Data);

            return new Response<GetModuleWithApplicationResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetModuleWithApplicationResultViewModel>> GetSingleModuleWithApplicationAsync(
            Expression<Func<Module, bool>> predicate)
        {
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                c => c.Application
            };

            // Fetch the single entity using the predicate and includes
            var response = await _getModuleWithApplicationRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithApplicationResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapModuleToViewModel(response.Data);

            return new Response<GetModuleWithApplicationResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetModuleWithApplicationResultViewModel>> GetSingleModuleWithApplicationAsync(
            ModuleSearchCriteriaWithPagination criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Module, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ModuleName) || c.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);

            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                c => c.Application
            };

            // Fetch the single entity using the predicate and includes
            var response = await _getModuleWithApplicationRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithApplicationResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapModuleToViewModel(response.Data);

            return new Response<GetModuleWithApplicationResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetModuleWithApplicationResultViewModel>> GetSingleModuleWithApplicationAsync(
            ModuleSearchCriteriaWithPagination criteria,
            params Expression<Func<Module, object>>[] includes)
        {
            // Build the predicate based on the criteria
            // Build the predicate based on the criteria
            Expression<Func<Module, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ModuleName) || c.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);


            // Fetch the single entity using the predicate and includes
            var response = await _getModuleWithApplicationRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithApplicationResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapModuleToViewModel(response.Data);

            return new Response<GetModuleWithApplicationResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = result
            };
        }


        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>


        private IEnumerable<GetModuleWithApplicationResultViewModel> MapModulesToViewModel(IEnumerable<GetModuleWithApplicationResultViewModel> modules)
        {
            if (modules == null)
            {
                return Enumerable.Empty<GetModuleWithApplicationResultViewModel>();
            }

            return modules.Select(m => new GetModuleWithApplicationResultViewModel
            {
                ModuleId = m.ModuleId,
                ModuleName = m.ModuleName,
                StateStatus = m.StateStatus,
                ApplicationId = m.ApplicationId,
                ApplicationName = m.Application?.ApplicationName,
                Application = m.Application != null ? _mapper.Map<GetApplicationResultViewModel>(m.Application) : null
            });
        }

        private GetModuleWithApplicationResultViewModel SingleMapModuleToViewModel(GetModuleWithApplicationResultViewModel module)
        {
            if (module == null)
            {
                return null;
            }

            return new GetModuleWithApplicationResultViewModel
            {
                ModuleId = module.ModuleId,
                ModuleName = module.ModuleName,
                StateStatus = module.StateStatus,
                ApplicationId = module.ApplicationId,
                ApplicationName = module.Application?.ApplicationName,
                Application = module.Application != null ? _mapper.Map<GetApplicationResultViewModel>(module.Application) : null
            };
        }




    }
}
