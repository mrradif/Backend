using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Applications;
using System.Linq.Expressions;

namespace API.Controllers.Control_Panel.Applications.Get
{
    public class GetApplicationWithIncludesService
    {
        private readonly IGetGenericRepo<Application, ControlPanelDbContext, GetApplicationWithIncludesResultViewModel, long> _getApplicationWithIncludesRepository;
        private readonly IMapper _mapper;

        public GetApplicationWithIncludesService(
            IGetGenericRepo<Application, ControlPanelDbContext, GetApplicationWithIncludesResultViewModel, long> getApplicationWithIncludesRepository,
            IMapper mapper
        )
        {
            _getApplicationWithIncludesRepository = getApplicationWithIncludesRepository;
            _mapper = mapper;
        }


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        public async Task<Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>> GetAllApplicationWithIncludesAsync(params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationWithIncludesRepository.GetAllAsync(includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            return new Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>
            {
                Success = true,
                Message = $"{response.Data.Count()} Applications retrieved successfully.",
                Data = response.Data
            };
        }




        public async Task<Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>> GetApplicationWithIncludesAndPredicateAsync(
            Expression<Func<Application, bool>> predicate,
            params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationWithIncludesRepository.GetAllAsync(
                predicate,
                includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }


            return new Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>
            {
                Success = true,
                Message = $"{response.Data.Count()} applications retrieved successfully.",
                Data = response.Data
            };
        }




        public async Task<Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>> GetApplicationWithIncludesAndPredicateAndOrderedAsync(
            (
            Expression<Func<Application, bool>> predicate,
            Func<IQueryable<Application>, IOrderedQueryable<Application>> orderBy,
            Expression<Func<Application, object>>[] includes) parameters)
        {
            var (predicate, orderBy, includes) = parameters;

            var response = await _getApplicationWithIncludesRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            return new Response<IEnumerable<GetApplicationWithIncludesResultViewModel>>
            {
                Success = true,
                Message = $"{response.Data.Count()} applications retrieved successfully.",
                Data = response.Data
            };
        }




        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>



        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        public async Task<Response<GetApplicationWithIncludesResultViewModel>> GetSingleApplicationWithIncludesAsync(
          Expression<Func<Application, bool>> predicate,
          params Expression<Func<Application, object>>[] includes)
        {
            var response = await _getApplicationWithIncludesRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetApplicationWithIncludesResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            return new Response<GetApplicationWithIncludesResultViewModel>
            {
                Success = true,
                Message = "Application retrieved successfully.",
                Data = response.Data
            };
        }



        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>


    }
}
