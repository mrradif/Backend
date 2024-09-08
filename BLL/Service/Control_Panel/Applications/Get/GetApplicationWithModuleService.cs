using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Criteria.Application;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Applications.Get
{
    public class GetApplicationWithModuleService
    {
        private readonly IGetGenericRepo<Application, ControlPanelDbContext, GetApplicationWithModuleResultViewModel, long> _getApplicationRepository;
        private readonly IMapper _mapper;

        public GetApplicationWithModuleService(
             IGetGenericRepo<Application, ControlPanelDbContext, GetApplicationWithModuleResultViewModel, long> getApplicationRepository,
            IMapper mapper
            )
        {
            _getApplicationRepository = getApplicationRepository;
            _mapper = mapper;
        }


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start


        // ...............................................................GetAllApplicationWithModulesAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetAllApplicationWithModulesAsync()
        {
            var response = await _getApplicationRepository.GetAllAsync(
                c => c.Modules
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Applications retrieved successfully.",
                Data = result
            };
        }

        // Overloaded method to accept dynamic includes
        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetAllApplicationWithModulesAsync(params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationRepository.GetAllAsync(
                includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Applications retrieved successfully.",
                Data = result
            };
        }

        // ..........................Overload End
        // ...............................................................GetAllApplicationWithModulesAsync






        // ...............................................................GetApplicationWithModulesAsync
        // ..........................Overload Start

        // First Overload: Retrieves buttons with components and applies default filtering
        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetApplicationWithModulesAsync()
        {
            var response = await _getApplicationRepository.GetAllAsync(
                b => b.IsDeleted == false, // Filter non-deleted buttons
                b => b.Modules // Include the related Component entity
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} applications retrieved successfully.",
                Data = result
            };
        }

        // Second Overload: Retrieves buttons with components based on a custom predicate and includes
        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetApplicationWithModulesAsync(
            Expression<Func<Application, bool>> predicate,
            params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationRepository.GetAllAsync(
                predicate, // Custom predicate
                includes // Custom includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} buttons retrieved successfully.",
                Data = result
            };
        }


        // ..........................Overload End
        // ...............................................................GetApplicationWithModulesAsync




        // ...............................................................GetApplicationsWithModulesCriteriaAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithModulesCriteriaAsync()
        {
            var response = await _getApplicationRepository.GetAllAsync(
                c => c.IsDeleted == false, // Filter non-deleted components
                c => c.Modules
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Applications retrieved successfully.",
                Data = result
            };
        }

        // Second Overload: Retrieves components with applications and modules based on a custom predicate and includes
        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithModulesCriteriaAsync(
            Expression<Func<Application, bool>> predicate,
            params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationRepository.GetAllAsync(
                predicate, // Custom predicate
                includes // Custom includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Applications retrieved successfully.",
                Data = result
            };
        }
        // ..........................Overload End
        // ...............................................................GetApplicationsWithModulesCriteriaAsync





        // ............................................................... GetApplicationsWithModulesOrderedAsync
        // ........................................ Overload Start
        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithModulesOrderedAsync(
            (Expression<Func<Application, bool>> predicate, Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy, Expression<Func<Application, object>>[] includes) parameters)
        {
            // Destructure the tuple
            var (predicate, orderBy, includes) = parameters;

            // Fetch data from the repository
            var response = await _getApplicationRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Applications retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithModulesOrderedAsync()
        {
            // Example predicate: Filters active and non-deleted components
            Expression<Func<Application, bool>> predicate = c => c.IsActive && !c.IsDeleted;

            // Example order by: Orders components by their ComponentName
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(c => c.ApplicationName);

            // Example includes: Includes the related Application and Module entities
            Expression<Func<Application, object>>[] includes = new Expression<Func<Application, object>>[]
            {
                c => c.Modules
            };

            // Fetch data from the repository
            var response = await _getApplicationRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} Applications retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<IEnumerable<GetApplicationWithModuleResultViewModel>>> GetApplicationsWithModulesOrderedAsync(ApplicationSearchCriteria criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Application, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || c.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);

            // Define the order by clause (ordering by ComponentName)
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy = q => q.OrderBy(c => c.ApplicationName);

            // Define the includes (including related entities like Application and Module)
            Expression<Func<Application, object>>[] includes = new Expression<Func<Application, object>>[]
            {
                c => c.Modules
            };

            // Fetch data from the repository
            var response = await _getApplicationRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapApplicationsToViewModel(response.Data);

            return new Response<IEnumerable<GetApplicationWithModuleResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} components retrieved successfully.",
                Data = result
            };
        }
        // ........................................ Overload End
        // ............................................................... GetApplicationsWithModulesOrderedAsync



        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>




        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        public async Task<Response<GetApplicationWithModuleResultViewModel>> GetSingleApplicationWithModuleAsync(
            Expression<Func<Application, bool>> predicate,
            params Expression<Func<Application, object>>[] includes)
        {
            // Fetch the single entity using the predicate and includes
            var response = await _getApplicationRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetApplicationWithModuleResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapApplicationsViewModel(response.Data);

            return new Response<GetApplicationWithModuleResultViewModel>
            {
                Success = true,
                Message = "Component retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetApplicationWithModuleResultViewModel>> GetSingleApplicationWithModuleAsync(
            Expression<Func<Application, bool>> predicate)
        {
            // Define the includes (including related entities like Applications and Modules)
            Expression<Func<Application, object>>[] includes = new Expression<Func<Application, object>>[]
            {
                c => c.Modules
            };

            // Fetch the single entity using the predicate and includes
            var response = await _getApplicationRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetApplicationWithModuleResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapApplicationsViewModel(response.Data);

            return new Response<GetApplicationWithModuleResultViewModel>
            {
                Success = true,
                Message = "Component retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetApplicationWithModuleResultViewModel>> GetSingleApplicationWithModuleAsync(
            ApplicationSearchCriteria criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Application, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || c.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);

            // Define the includes (including related entities like Applications and Modules)
            Expression<Func<Application, object>>[] includes = new Expression<Func<Application, object>>[]
            {
                c => c.Modules
            };

            // Fetch the single entity using the predicate and includes
            var response = await _getApplicationRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetApplicationWithModuleResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapApplicationsViewModel(response.Data);

            return new Response<GetApplicationWithModuleResultViewModel>
            {
                Success = true,
                Message = "Component retrieved successfully.",
                Data = result
            };
        }

        public async Task<Response<GetApplicationWithModuleResultViewModel>> GetSingleApplicationWithModuleAsync(
            ApplicationSearchCriteria criteria,
            params Expression<Func<Application, object>>[] includes)
        {
            // Build the predicate based on the criteria
            // Build the predicate based on the criteria
            Expression<Func<Application, bool>> predicate = c =>
                (string.IsNullOrEmpty(criteria.ApplicationName) || c.ApplicationName.Contains(criteria.ApplicationName)) &&
                (!criteria.IsActive || c.IsActive == criteria.IsActive);


            // Fetch the single entity using the predicate and includes
            var response = await _getApplicationRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetApplicationWithModuleResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapApplicationsViewModel(response.Data);

            return new Response<GetApplicationWithModuleResultViewModel>
            {
                Success = true,
                Message = "Component retrieved successfully.",
                Data = result
            };
        }



        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>



        private IEnumerable<GetApplicationWithModuleResultViewModel> MapApplicationsToViewModel(IEnumerable<GetApplicationWithModuleResultViewModel> components)
        {
            return components.Select(c => new GetApplicationWithModuleResultViewModel
            {
                ApplicationType = c.ApplicationType,
                ApplicationName = c.ApplicationName,
                ApplicationId = c.ApplicationId,
                StateStatus = c.StateStatus,


                // Using AutoMapper to map Application and Module to their corresponding view models
                //Application = c.Application != null ? _mapper.Map<GetApplicationResultViewModel>(c.Application) : null,
                //Module = c.Module != null ? _mapper.Map<GetModuleResultViewModel>(c.Module) : null
            });
        }

        private GetApplicationWithModuleResultViewModel SingleMapApplicationsViewModel(GetApplicationWithModuleResultViewModel c)
        {
            return new GetApplicationWithModuleResultViewModel
            {
                ApplicationType = c.ApplicationType,
                ApplicationName = c.ApplicationName,
                ApplicationId = c.ApplicationId,
                StateStatus = c.StateStatus,


                // Using AutoMapper to map Application and Module to their corresponding view models
                //Application = c.Application != null ? _mapper.Map<GetApplicationResultViewModel>(c.Application) : null,
                //Module = c.Module != null ? _mapper.Map<GetModuleResultViewModel>(c.Module) : null
            };
        }




    }
}

