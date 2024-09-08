

using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.Domain.Control_Panel.Administration.App_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Modules;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Modules.Get
{
    public class GetModuleWithIncludesService
    {
        private readonly IGetGenericRepo<Module, ControlPanelDbContext, GetModuleWithIncludesResultViewModel, long> _getModuleWithIncludesRepository;
        private readonly IMapper _mapper;

        public GetModuleWithIncludesService(
            IGetGenericRepo<Module, ControlPanelDbContext, GetModuleWithIncludesResultViewModel, long> getModuleWithIncludesRepository,
            IMapper mapper
        )
        {
            _getModuleWithIncludesRepository = getModuleWithIncludesRepository;
            _mapper = mapper;
        }


        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get All Start

        public async Task<Response<IEnumerable<GetModuleWithIncludesResultViewModel>>> GetAllModuleWithIncludesAsync(params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithIncludesRepository.GetAllAsync(includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithIncludesResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            return new Response<IEnumerable<GetModuleWithIncludesResultViewModel>>
            {
                Success = true,
                Message = $"{response.Data.Count()} Modules retrieved successfully.",
                Data = response.Data
            };
        }




        public async Task<Response<IEnumerable<GetModuleWithIncludesResultViewModel>>> GetModuleWithIncludesAndPredicateAsync(
            Expression<Func<Module, bool>> predicate,
            params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithIncludesRepository.GetAllAsync(
                predicate,
                includes
            );

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithIncludesResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }


            return new Response<IEnumerable<GetModuleWithIncludesResultViewModel>>
            {
                Success = true,
                Message = $"{response.Data.Count()} modules retrieved successfully.",
                Data = response.Data
            };
        }




        public async Task<Response<IEnumerable<GetModuleWithIncludesResultViewModel>>> GetModuleWithIncludesAndPredicateAndOrderedAsync(
            (
            Expression<Func<Module, bool>> predicate,
            Func<IQueryable<Module>, IOrderedQueryable<Module>> orderBy,
            Expression<Func<Module, object>>[] includes) parameters)
        {
            var (predicate, orderBy, includes) = parameters;

            var response = await _getModuleWithIncludesRepository.GetAllAsync(predicate, orderBy, includes);

            if (!response.Success)
            {
                return new Response<IEnumerable<GetModuleWithIncludesResultViewModel>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            return new Response<IEnumerable<GetModuleWithIncludesResultViewModel>>
            {
                Success = true,
                Message = $"{response.Data.Count()} modules retrieved successfully.",
                Data = response.Data
            };
        }

     


        // ................................................ Get All End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>



        // ---------------------------------------------------------------------------------------------------------------------- >>>
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ................................................ Get Single Start

        public async Task<Response<GetModuleWithIncludesResultViewModel>> GetSingleModuleWithIncludesAsync(
          Expression<Func<Module, bool>> predicate,
          params Expression<Func<Module, object>>[] includes)
        {
            var response = await _getModuleWithIncludesRepository.GetSingleAsync(predicate, includes);

            if (!response.Success)
            {
                return new Response<GetModuleWithIncludesResultViewModel>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null
                };
            }

            return new Response<GetModuleWithIncludesResultViewModel>
            {
                Success = true,
                Message = "Module retrieved successfully.",
                Data = response.Data
            };
        }



        // ................................................ Get Single End
        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ---------------------------------------------------------------------------------------------------------------------- >>>



    }
}
