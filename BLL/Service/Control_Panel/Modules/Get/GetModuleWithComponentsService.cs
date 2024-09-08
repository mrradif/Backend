using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Modules;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Components;
using Shared.View.Control_Panel.Modules;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Modules.Get
{
    public class GetModuleWithComponentsService
    {
        private readonly IGetGenericRepo<Module, ControlPanelDbContext, GetModuleWithComponentsResultViewModel, long> _getModuleWithComponentsRepository;
        private readonly IMapper _mapper;

        public GetModuleWithComponentsService(
            IGetGenericRepo<Module, ControlPanelDbContext, GetModuleWithComponentsResultViewModel, long> getModuleWithComponentsRepository,
            IMapper mapper
        )
        {
            _getModuleWithComponentsRepository = getModuleWithComponentsRepository;
            _mapper = mapper;
        }


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        // ...............................................................GetAllModulesWithComponentsAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetAllModulesWithComponentsAsync()
        {
            var response = await _getModuleWithComponentsRepository.GetAllAsync(
                m => m.Components
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Modules retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetAllModulesWithComponentsAsync(params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithComponentsRepository.GetAllAsync(includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Modules retrieved successfully.",
                Data = result
            };
        }
        // ..........................Overload End
        // ...............................................................GetAllModulesWithComponentsAsync



        // ...............................................................GetModulesWithComponentsAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetModulesWithComponentsAsync()
        {
            var response = await _getModuleWithComponentsRepository.GetAllAsync(
                b => b.IsDeleted == false, 
                b => b.Components
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }


        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetModulesWithComponentsAsync(
            Expression<Func<Module, bool>> predicate,
            params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithComponentsRepository.GetAllAsync(
                predicate,
                includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }
        // ..........................Overload End
        // ...............................................................GetApplicationWithModulesAsync






        // ...............................................................GetModuleWithComponentsCriteriaAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetModuleWithComponentsCriteriaAsync()
        {
            var response = await _getModuleWithComponentsRepository.GetAllAsync(
                m => m.IsDeleted == false,
                m => m.Components
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Modules retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetModuleWithComponentsCriteriaAsync(
            Expression<Func<Module, bool>> predicate,
            params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithComponentsRepository.GetAllAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Modules retrieved successfully.",
                Data = result
            };
        }
        // ..........................Overload End
        // ...............................................................GetModuleWithComponentsCriteriaAsync




        // ............................................................... GetModulesWithComponentsOrderedAsync
        // ........................................ Overload Start
        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetModulesWithComponentsOrderedAsync(
            (
            Expression<Func<Module, bool>> predicate, 
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy, 
            Expression<Func<Module, object>>[] includes) parameters)
        {
            // Destructure the tuple
            var (predicate, orderBy, includes) = parameters;

            // Fetch data from the repository
            var response = await _getModuleWithComponentsRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetModulesWithComponentsOrderedAsync()
        {
            // Example predicate: Filters active and non-deleted components
            Expression<Func<Module, bool>> predicate = c => c.IsActive && !c.IsDeleted;

            // Example order by: Orders components by their ComponentName
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy = q => q.OrderBy(c => c.ModuleName);

            // Example includes: Includes the related Application and Module entities
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                c => c.Components
            };

            // Fetch data from the repository
            var response = await _getModuleWithComponentsRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetModuleWithComponentsResultViewModel>>> GetModulesWithComponentsOrderedAsync(ModuleSearchCriteriaWithPagination criteria)
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
                c => c.Components
            };

            // Fetch data from the repository
            var response = await _getModuleWithComponentsRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapModulesToViewModel(response.Data);

            return new Response<IEnumerable<GetModuleWithComponentsResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} modules retrieved successfully.",
                Data = result
            };
        }
        // ........................................ Overload End
        // ............................................................... GetModulesWithComponentsOrderedAsync





        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>



        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        public async Task<Response<GetModuleWithComponentsResultViewModel>> GetSingleModuleWithComponenAsync(
          Expression<Func<Module, bool>> predicate,
          params Expression<Func<Module, object>>[] includes)
        {
            // Fetch the single entity using the predicate and includes
            var response = await _getModuleWithComponentsRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithComponentsResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapModuleToViewModel(response.Data);

            return new Response<GetModuleWithComponentsResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetModuleWithComponentsResultViewModel>> GetSingleModuleWithComponenAsync(
            Expression<Func<Module, bool>> predicate)
        {
            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                c => c.Components
            };

            // Fetch the single entity using the predicate and includes
            var response = await _getModuleWithComponentsRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithComponentsResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapModuleToViewModel(response.Data);

            return new Response<GetModuleWithComponentsResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetModuleWithComponentsResultViewModel>> GetSingleModuleWithComponenAsync(
            ModuleSearchCriteriaWithPagination criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Module, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ModuleName) || c.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);

            Expression<Func<Module, object>>[] includes = new Expression<Func<Module, object>>[]
            {
                c => c.Components
            };

            // Fetch the single entity using the predicate and includes
            var response = await _getModuleWithComponentsRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithComponentsResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapModuleToViewModel(response.Data);

            return new Response<GetModuleWithComponentsResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetModuleWithComponentsResultViewModel>> GetSingleModuleWithComponenAsync(
            ModuleSearchCriteriaWithPagination criteria,
            params Expression<Func<Module, object>>[] includes)
        {
            // Build the predicate based on the criteria
            // Build the predicate based on the criteria
            Expression<Func<Module, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ModuleName) || c.ModuleName.Contains(criteria.ModuleName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);


            // Fetch the single entity using the predicate and includes
            var response = await _getModuleWithComponentsRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithComponentsResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapModuleToViewModel(response.Data);

            return new Response<GetModuleWithComponentsResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = result
            };
        }


        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>

        private IEnumerable<GetModuleWithComponentsResultViewModel> MapModulesToViewModel(IEnumerable<GetModuleWithComponentsResultViewModel> modules)
        {
            return modules.Select(m => new GetModuleWithComponentsResultViewModel
            {
                ModuleId = m.ModuleId,
                ModuleName = m.ModuleName,
                StateStatus = m.StateStatus,
                // Use AutoMapper for mapping other related entities if needed
                Components = m.Components != null ? _mapper.Map<List<GetComponentResultViewModel>>(m.Components) : null
            });
        }

        private GetModuleWithComponentsResultViewModel SingleMapModuleToViewModel(GetModuleWithComponentsResultViewModel module)
        {
            return new GetModuleWithComponentsResultViewModel
            {
                ModuleId = module.ModuleId,
                ModuleName = module.ModuleName,
                StateStatus = module.StateStatus,
                // Use AutoMapper for mapping other related entities if needed
                Components = module.Components != null ? _mapper.Map<List<GetComponentResultViewModel>>(module.Components) : null
            };
        }



    }
}
