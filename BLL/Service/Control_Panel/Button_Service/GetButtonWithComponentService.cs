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
    public class GetButtonWithComponentService
    {
        private readonly IGetGenericRepo<Button, ControlPanelDbContext, GetButtonWithComponetResultViewModel, long> _getButtonRepository;
        private readonly IMapper _mapper;

        public GetButtonWithComponentService(
             IGetGenericRepo<Button, ControlPanelDbContext, GetButtonWithComponetResultViewModel, long> getButtonRepository,
            IMapper mapper
            )
        {
            _getButtonRepository = getButtonRepository;
            _mapper = mapper;
        }


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start



        // ...............................................................GetAllButtonsWithComponentsAsync
        // ..........................Overload Start
        public async Task<Response<IEnumerable<GetButtonWithComponetResultViewModel>>> GetAllButtonsWithComponentsAsync()
        {
            var response = await _getButtonRepository.GetAllAsync(
                b => b.Components // Include the related Component entity
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapButtonsToViewModel(response.Data); // Use the mapping method

            return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} buttons retrieved successfully.",
                Data = result
            };
        }

        // Overloaded method to accept dynamic includes
        public async Task<Response<IEnumerable<GetButtonWithComponetResultViewModel>>> GetAllButtonsWithComponentsAsync(params Expression<Func<Button, object>>[] includes)
        {
            var response = await _getButtonRepository.GetAllAsync(
                includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapButtonsToViewModel(response.Data); // Use the mapping method

            return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} buttons retrieved successfully.",
                Data = result
            };
        }


        // ..........................Overload End
        // ...............................................................GetAllButtonsWithComponentsAsync





        // ...............................................................GetButtonsWithComponentsAsync
        // ..........................Overload Start

        // First Overload: Retrieves buttons with components and applies default filtering
        public async Task<Response<IEnumerable<GetButtonWithComponetResultViewModel>>> GetButtonsWithComponentsAsync()
        {
            var response = await _getButtonRepository.GetAllAsync(
                b => b.IsDeleted == false, // Filter non-deleted buttons
                b => b.Components // Include the related Component entity
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapButtonsToViewModel(response.Data);

            return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} buttons retrieved successfully.",
                Data = result
            };
        }

        // Second Overload: Retrieves buttons with components based on a custom predicate and includes
        public async Task<Response<IEnumerable<GetButtonWithComponetResultViewModel>>> GetButtonsWithComponentsAsync(
            Expression<Func<Button, bool>> predicate,
            params Expression<Func<Button, object>>[] includes)
        {
            var response = await _getButtonRepository.GetAllAsync(
                predicate, // Custom predicate
                includes // Custom includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapButtonsToViewModel(response.Data);

            return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} buttons retrieved successfully.",
                Data = result
            };
        }

        // Private method to handle mapping
        private IEnumerable<GetButtonWithComponetResultViewModel> MapButtonsToViewModel(IEnumerable<GetButtonWithComponetResultViewModel> buttons)
        {
            return buttons.Select(b => new GetButtonWithComponetResultViewModel
            {
                ButtonId = b.ButtonId,
                ButtonName = b.ButtonName,
                Tooltip = b.Tooltip,
                OnClickActionName = b.OnClickActionName,
                Icon = b.Icon,
                CssClass = b.CssClass,
                IsVisible = b.IsVisible,
                IsEnabled = b.IsEnabled,
                StateStatus = b.StateStatus,
                ComponentId = b.Components?.ComponentId,
                ComponentName = b.Components?.ComponentName
              
            });
        }

        // ..........................Overload End
        // ...............................................................GetButtonsWithComponentsAsync




        // ............................................................... GetButtonsWithComponentsOrderedAsync
        // ........................................ Overload Start

        public async Task<Response<IEnumerable<GetButtonWithComponetResultViewModel>>> GetButtonsWithComponentsOrderedAsync(
            (Expression<Func<Button, bool>> predicate, Func<IQueryable<Button>, IOrderedQueryable<Button>> orderBy, Expression<Func<Button, object>>[] includes) parameters)
        {
            // Extract parameters from the tuple
            var (predicate, orderBy, includes) = parameters;

            // Fetch data from the repository
            var response = await _getButtonRepository.GetAllAsync(
                predicate, 
                orderBy,   
                includes   
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapButtonsToViewModel(response.Data);

            return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} buttons retrieved successfully.",
                Data = result
            };
        }


        public async Task<Response<IEnumerable<GetButtonWithComponetResultViewModel>>> GetButtonsWithComponentsOrderedAsync()
        {
            // Example predicate: Filters active buttons belonging to a specific component
            Expression<Func<Button, bool>> predicate = b => b.IsActive == true && b.IsDeleted == false;

            // Example order by: Orders buttons by their ButtonName
            Func<IQueryable<Button>, IOrderedQueryable<Button>> orderBy = q => q.OrderBy(b => b.ButtonName);

            // Example includes: Includes the Component related entity
            Expression<Func<Button, object>>[] includes = new Expression<Func<Button, object>>[]
            {
            b => b.Components
                };

            // Create a tuple from the parameters
            var parameters = (predicate, orderBy, includes);

            // Fetch data from the repository using the parameters
            var response = await _getButtonRepository.GetAllAsync(
                parameters.predicate,
                parameters.orderBy,
                parameters.includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapButtonsToViewModel(response.Data);

            return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} buttons retrieved successfully.",
                Data = result
            };
        }



        public async Task<Response<IEnumerable<GetButtonWithComponetResultViewModel>>> GetButtonsWithComponentsOrderedAsync(ButtonSearchCriteria criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Button, bool>> predicate = b =>
                (string.IsNullOrEmpty(criteria.ButtonName) || b.ButtonName.Contains(criteria.ButtonName)) &&
                (!criteria.IsActive || b.IsActive == criteria.IsActive);

            // Define the order by clause (ordering by ButtonName)
            Func<IQueryable<Button>, IOrderedQueryable<Button>> orderBy = q => q.OrderBy(b => b.ButtonName);

            // Define the includes (including related entities like Components)
            Expression<Func<Button, object>>[] includes = new Expression<Func<Button, object>>[]
            {
            b => b.Components
                };

            // Create a tuple from the parameters
            var parameters = (predicate, orderBy, includes);

            // Fetch data from the repository using the parameters
            var response = await _getButtonRepository.GetAllAsync(
                parameters.predicate,
                parameters.orderBy,
                parameters.includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = MapButtonsToViewModel(response.Data);

            return new Response<IEnumerable<GetButtonWithComponetResultViewModel>>
            {
                Success = true,
                Message = $"{result.Count()} buttons retrieved successfully.",
                Data = result
            };
        }



        // ........................................ Overload End
        // ............................................................... GetButtonsWithComponentsOrderedAsync





        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>





        // .................................................... Get Single
        // ............................. Start


        public async Task<Response<GetButtonWithComponetResultViewModel>> GetSingleButtonWithComponentAsync(
            Expression<Func<Button, bool>> predicate,
            params Expression<Func<Button, object>>[] includes)
        {
            // Fetch the single entity using the predicate and includes
            var response = await _getButtonRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetButtonWithComponetResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapButtonsToViewModel(response.Data);

            return new Response<GetButtonWithComponetResultViewModel>
            {
                Success = true,
                Message = "Button retrieved successfully.",
                Data = result
            };
        }


        public async Task<Response<GetButtonWithComponetResultViewModel>> GetSingleButtonWithComponentAsync(Expression<Func<Button, bool>> predicate)
        {
            // Define the includes (including related entities like Components)
            Expression<Func<Button, object>>[] includes = new Expression<Func<Button, object>>[]
            {
            b => b.Components
                };

            // Fetch the single entity using the predicate and includes
            var response = await _getButtonRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetButtonWithComponetResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapButtonsToViewModel(response.Data);

            return new Response<GetButtonWithComponetResultViewModel>
            {
                Success = true,
                Message = "Button retrieved successfully.",
                Data = result
            };
        }





        public async Task<Response<GetButtonWithComponetResultViewModel>> GetSingleButtonWithComponentAsync(ButtonSearchCriteria criteria)
        {
            // Build the predicate based on the criteria
            Expression<Func<Button, bool>> predicate = b =>
                (string.IsNullOrEmpty(criteria.ButtonName) || b.ButtonName.Contains(criteria.ButtonName)) &&
                (!criteria.IsActive || b.IsActive == criteria.IsActive);

            // Define the includes (including related entities like Components)
            Expression<Func<Button, object>>[] includes = new Expression<Func<Button, object>>[]
            {
            b => b.Components
                };

            // Fetch the single entity using the predicate and includes
            var response = await _getButtonRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetButtonWithComponetResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapButtonsToViewModel(response.Data);

            return new Response<GetButtonWithComponetResultViewModel>
            {
                Success = true,
                Message = "Button retrieved successfully.",
                Data = result
            };
        }



        public async Task<Response<GetButtonWithComponetResultViewModel>> GetSingleButtonWithComponentAsync(
            ButtonSearchCriteria criteria,
            params Expression<Func<Button, object>>[] includes)
        {
            // Build the predicate based on the criteria
            Expression<Func<Button, bool>> predicate = b =>
                (string.IsNullOrEmpty(criteria.ButtonName) || b.ButtonName.Contains(criteria.ButtonName)) &&
                (!criteria.IsActive || b.IsActive == criteria.IsActive);

            // Fetch the single entity using the predicate and includes
            var response = await _getButtonRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetButtonWithComponetResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            var result = SingleMapButtonsToViewModel(response.Data);

            return new Response<GetButtonWithComponetResultViewModel>
            {
                Success = true,
                Message = "Button retrieved successfully.",
                Data = result
            };
        }



        // Private method to handle mapping
        private GetButtonWithComponetResultViewModel SingleMapButtonsToViewModel(GetButtonWithComponetResultViewModel button)
        {
            return new GetButtonWithComponetResultViewModel
            {
                ButtonId = button.ButtonId,
                ButtonName = button.ButtonName,
                Tooltip = button.Tooltip,
                OnClickActionName = button.OnClickActionName,
                Icon = button.Icon,
                CssClass = button.CssClass,
                IsVisible = button.IsVisible,
                IsEnabled = button.IsEnabled,
                StateStatus = button.StateStatus,
                ComponentId = button.Components?.ComponentId,
                ComponentName = button.Components?.ComponentName
            };
        }



    }
}
