using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Microsoft.AspNetCore.Mvc;
using Shared.Criteria.Control_Panel.Search_Component;
using Shared.Domain.Control_Panel.Administration.Org_Config;
using Shared.DTO.Common;
using Shared.View.Control_Panel.Components;
using System.Linq.Expressions;

namespace BLL.Service.Control_Panel.Component_Service
{
    public class GetComponentService
    {
        private readonly IGetGenericRepo<Component, ControlPanelDbContext, GetComponentResultViewModel, long> _getComponentRepository;
        private readonly IMapper _mapper;

        public GetComponentService(
            IGetGenericRepo<Component, ControlPanelDbContext, GetComponentResultViewModel, long> getComponentRepository,
            IMapper mapper
        )
        {
            _getComponentRepository = getComponentRepository ?? throw new ArgumentNullException(nameof(getComponentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<IEnumerable<GetComponentResultViewModel>>> GetAllComponentsAsync()
        {
            var response = await _getComponentRepository.GetAllAsync();
            return response;
        }


        // Overload Start
        public async Task<Response<IEnumerable<GetComponentResultViewModel>>> GetComponentsAsync()
        {
            var response = await _getComponentRepository.GetAllAsync(e => e.IsDeleted == false);
            return response;
        }


        public async Task<Response<IEnumerable<GetComponentResultViewModel>>> GetComponentsByCriteriaAsync(ComponentSearchCriteria criteria)
        {
            Expression<Func<Component, bool>> predicate = component =>
                (string.IsNullOrEmpty(criteria.ComponentName) || component.ComponentName.Contains(criteria.ComponentName)) &&
                (!criteria.IsActive || component.IsActive);

            var response = await _getComponentRepository.GetAllAsync(predicate);
            return response;
        }

        public async Task<Response<IEnumerable<GetComponentResultViewModel>>> GetComponentsByCriteriaAsync(Expression<Func<Component, bool>> predicate)
        {
            return await _getComponentRepository.GetAllAsync(predicate);
        }
        // Overload End



        // Overload Start
        public async Task<Response<IEnumerable<GetComponentResultViewModel>>> GetComponentsWithOrderingAsync(ComponentSearchCriteria criteria)
        {
            Expression<Func<Component, bool>> predicate = component =>
                (string.IsNullOrEmpty(criteria.ComponentName) || component.ComponentName.Contains(criteria.ComponentName)) &&
                (!criteria.IsActive || component.IsActive);

            Func<IQueryable<Component>, IOrderedQueryable<Component>> orderBy = q => q.OrderByDescending(component => component.ComponentId);

            var response = await _getComponentRepository.GetAllAsync(predicate, orderBy);
            return response;
        }

        public async Task<Response<IEnumerable<GetComponentResultViewModel>>> GetComponentsWithOrderingAsync(Expression<Func<Component, bool>> predicate, Func<IQueryable<Component>, IOrderedQueryable<Component>> orderBy = null)
        {
            var response = await _getComponentRepository.GetAllAsync(predicate, orderBy);
            return response;
        }
        // Overload End




        // Get Single Start
        public async Task<Response<GetComponentResultViewModel>> GetSingleComponentByIdAsync(long componentId)
        {
            return await _getComponentRepository.GetSingleAsync(componentId);
        }


        // Overload Start
        public async Task<Response<GetComponentResultViewModel>> GetSingleComponentByCriteriaAsync(ComponentSearchCriteria criteria)
        {
            Expression<Func<Component, bool>> predicate = component =>
                (string.IsNullOrEmpty(criteria.ComponentName) || component.ComponentName.Contains(criteria.ComponentName)) &&
                (!criteria.IsActive || component.IsActive);

            var response = await _getComponentRepository.GetSingleAsync(predicate);
            return response;
        }

        public async Task<Response<GetComponentResultViewModel>> GetSingleComponentByCriteriaAsync(Expression<Func<Component, bool>> predicate)
        {
            return await _getComponentRepository.GetSingleAsync(predicate);
        }
        // Overload End
    }
}
