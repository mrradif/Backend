using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Control_Panel.Search_Component;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Components;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Component_Service
{
    public class GetComponentWithApplicationAndModuleService
    {
        private readonly IGetGenericRepo<Component, ControlPanelDbContext, GetComponentWithApplicationAndModuleResultViewModel, long> _getComponentRepository;
        private readonly IMapper _mapper;

        public GetComponentWithApplicationAndModuleService(
             IGetGenericRepo<Component, ControlPanelDbContext, GetComponentWithApplicationAndModuleResultViewModel, long> getComponentRepository,
            IMapper mapper
            )
        {
            _getComponentRepository = getComponentRepository;
            _mapper = mapper;
        }


        // ...............................................................GetAllComponentsWithApplicationsAndModulesAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>> GetAllComponentsWithApplicationsAndModulesAsync()
        {
            var response = await _getComponentRepository.GetAllAsync(
                c => c.Application,  // Assuming these are the related entities
                c => c.Module         // Include the related Application and Module entities
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapComponentsToViewModel(response.Data);

            return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} components retrieved successfully.",
                Data = result
            };
        }


        // Overloaded method to accept dynamic includes
        public async Task<Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>> GetAllComponentsWithApplicationsAndModulesAsync(params Expression<Func<Component, object>>[] includes)
        {
            var response = await _getComponentRepository.GetAllAsync(
                includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapComponentsToViewModel(response.Data);

            return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} components retrieved successfully.",
                Data = result
            };
        }
        // ..........................Overload End
        // ...............................................................GetAllComponentsWithApplicationsAndModulesAsync





        // ...............................................................GetComponentWithApplicationsAndModulesAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>> GetComponentWithApplicationsAndModulesAsync()
        {
            var response = await _getComponentRepository.GetAllAsync(
                c => c.IsDeleted == false, // Filter non-deleted components
                c => c.Application,        // Include related Application entity
                c => c.Module              // Include related Module entity
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapComponentsToViewModel(response.Data);

            return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} components retrieved successfully.",
                Data = result
            };
        }

        // Second Overload: Retrieves components with applications and modules based on a custom predicate and includes
        public async Task<Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>> GetComponentWithApplicationsAndModulesAsync(
            Expression<Func<Component, bool>> predicate,
            params Expression<Func<Component, object>>[] includes)
        {
            var response = await _getComponentRepository.GetAllAsync(
                predicate, // Custom predicate
                includes // Custom includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapComponentsToViewModel(response.Data);

            return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} components retrieved successfully.",
                Data = result
            };
        }

        // Private method to handle mapping
        private IEnumerable<GetComponentWithApplicationAndModuleResultViewModel> MapComponentsToViewModel(IEnumerable<GetComponentWithApplicationAndModuleResultViewModel> components)
        {
            return components.Select(c => new GetComponentWithApplicationAndModuleResultViewModel
            {
                ComponentId = c.ComponentId,
                ComponentName = c.ComponentName,
                Description = c.Description,
                ApplicationId = c.ApplicationId,
                ApplicationName = c.Application?.ApplicationName, 
                ModuleId = c.ModuleId,
                ModuleName = c.Module?.ModuleName,
                IsApproved = c.IsApproved,
                IsRejected = c.IsRejected,
                IsDeleted = c.IsDeleted,
                IsRestored = c.IsRestored,
                IsCancelled = c.IsCancelled,
                IsActivated = c.IsActivated,
                IsDeactived = c.IsDeactived,
                IsActive = c.IsActive,
                StateStatus = c.StateStatus,


                // Using AutoMapper to map Application and Module to their corresponding view models
                //Application = c.Application != null ? _mapper.Map<GetApplicationResultViewModel>(c.Application) : null,
                //Module = c.Module != null ? _mapper.Map<GetModuleResultViewModel>(c.Module) : null
            });
        }

        // ..........................Overload End
        // ...............................................................GetComponentWithApplicationsAndModulesAsync



        // ............................................................... GetComponentsWithApplicationsAndModulesOrderedAsync
        // ........................................ Overload Start

        public async Task<Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>> GetComponentsWithApplicationsAndModulesOrderedAsync(
            (Expression<Func<Component, bool>> predicate, Func<IQueryable<Component>, IOrderedQueryable<Component>> orderBy, Expression<Func<Component, object>>[] includes) parameters)
        {
            // Destructure the tuple
            var (predicate, orderBy, includes) = parameters;

            // Fetch data from the repository
            var response = await _getComponentRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapComponentsToViewModel(response.Data);

            return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} components retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>> GetComponentsWithApplicationsAndModulesOrderedAsync()
        {
            // Example predicate: Filters active and non-deleted components
            Expression<Func<Component, bool>> predicate = c => c.IsActive && !c.IsDeleted;

            // Example order by: Orders components by their ComponentName
            Func<IQueryable<Component>, IOrderedQueryable<Component>> orderBy = q => q.OrderBy(c => c.ComponentName);

            // Example includes: Includes the related Application and Module entities
            Expression<Func<Component, object>>[] includes = new Expression<Func<Component, object>>[]
            {
        c => c.Application,
        c => c.Module
            };

            // Fetch data from the repository
            var response = await _getComponentRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapComponentsToViewModel(response.Data);

            return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} components retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>> GetComponentsWithApplicationsAndModulesOrderedAsync(ComponentSearchCriteria criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Component, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ComponentName) || c.ComponentName.Contains(criteria.ComponentName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);

            // Define the order by clause (ordering by ComponentName)
            Func<IQueryable<Component>, IOrderedQueryable<Component>> orderBy = q => q.OrderBy(c => c.ComponentName);

            // Define the includes (including related entities like Application and Module)
            Expression<Func<Component, object>>[] includes = new Expression<Func<Component, object>>[]
            {
        c => c.Application,
        c => c.Module
            };

            // Fetch data from the repository
            var response = await _getComponentRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapComponentsToViewModel(response.Data);

            return new Response<IEnumerable<GetComponentWithApplicationAndModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} components retrieved successfully.",
                Data = result
            };
        }

        // ........................................ Overload End
        // ............................................................... GetComponentsWithApplicationsAndModulesOrderedAsync





        // .................................................... Get Single
        // ............................. Start

        public async Task<Response<GetComponentWithApplicationAndModuleResultViewModel>> GetSingleComponentWithApplicationAndModuleAsync(
            Expression<Func<Component, bool>> predicate,
            params Expression<Func<Component, object>>[] includes)
        {
            // Fetch the single entity using the predicate and includes
            var response = await _getComponentRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetComponentWithApplicationAndModuleResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapComponentsToViewModel(response.Data);

            return new Response<GetComponentWithApplicationAndModuleResultViewModel>
            {
                Success = true,
                Message = "Component retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetComponentWithApplicationAndModuleResultViewModel>> GetSingleComponentWithApplicationAndModuleAsync(
            Expression<Func<Component, bool>> predicate)
        {
            // Define the includes (including related entities like Applications and Modules)
            Expression<Func<Component, object>>[] includes = new Expression<Func<Component, object>>[]
            {
                c => c.Application,
                c => c.Module
            };

            // Fetch the single entity using the predicate and includes
            var response = await _getComponentRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetComponentWithApplicationAndModuleResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapComponentsToViewModel(response.Data);

            return new Response<GetComponentWithApplicationAndModuleResultViewModel>
            {
                Success = true,
                Message = "Component retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetComponentWithApplicationAndModuleResultViewModel>> GetSingleComponentWithApplicationAndModuleAsync(
            ComponentSearchCriteria criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Component, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ComponentName) || c.ComponentName.Contains(criteria.ComponentName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);

            // Define the includes (including related entities like Applications and Modules)
            Expression<Func<Component, object>>[] includes = new Expression<Func<Component, object>>[]
            {
                c => c.Application,
                c => c.Module
            };

            // Fetch the single entity using the predicate and includes
            var response = await _getComponentRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetComponentWithApplicationAndModuleResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapComponentsToViewModel(response.Data);

            return new Response<GetComponentWithApplicationAndModuleResultViewModel>
            {
                Success = true,
                Message = "Component retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetComponentWithApplicationAndModuleResultViewModel>> GetSingleComponentWithApplicationAndModuleAsync(
            ComponentSearchCriteria criteria,
            params Expression<Func<Component, object>>[] includes)
        {
            // Build the predicate based on the criteria
            Expression<Func<Component, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ComponentName) || c.ComponentName.Contains(criteria.ComponentName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);

            // Fetch the single entity using the predicate and includes
            var response = await _getComponentRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetComponentWithApplicationAndModuleResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapComponentsToViewModel(response.Data);

            return new Response<GetComponentWithApplicationAndModuleResultViewModel>
            {
                Success = true,
                Message = "Component retrieved successfully.",
                Data = result
            };
        }

        // Private method to handle mapping
        private GetComponentWithApplicationAndModuleResultViewModel SingleMapComponentsToViewModel(GetComponentWithApplicationAndModuleResultViewModel c)
        {
            return new GetComponentWithApplicationAndModuleResultViewModel
            {
                ComponentId = c.ComponentId,
                ComponentName = c.ComponentName,
                Description = c.Description,
                ApplicationId = c.ApplicationId,
                ApplicationName = c.Application?.ApplicationName,
                ModuleId = c.ModuleId,
                ModuleName = c.Module?.ModuleName,
                IsApproved = c.IsApproved,
                IsRejected = c.IsRejected,
                IsDeleted = c.IsDeleted,
                IsRestored = c.IsRestored,
                IsCancelled = c.IsCancelled,
                IsActivated = c.IsActivated,
                IsDeactived = c.IsDeactived,
                IsActive = c.IsActive,
                StateStatus = c.StateStatus


                // Using AutoMapper to map Application and Module to their corresponding view models
                //Application = c.Application != null ? _mapper.Map<GetApplicationResultViewModel>(c.Application) : null,
                //Module = c.Module != null ? _mapper.Map<GetModuleResultViewModel>(c.Module) : null
            };
        }

    }
}
