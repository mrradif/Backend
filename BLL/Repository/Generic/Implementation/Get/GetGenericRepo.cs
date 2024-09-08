using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using DAL.Service.Logger;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Common;
using System;
using System.Linq.Expressions;


namespace BLL.Repository.Generic.Implementation.Get
{
    public class GetGenericRepo<T, TContext, TDtoResult, TId> : IGetGenericRepo<T, TContext, TDtoResult, TId>
        where T : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IMapper _mapper;

        public GetGenericRepo(TContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<TDtoResult>>();

            try
            {
                var entities = await _context.Set<T>().ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();
                response.Success = true;
                response.Data = dtos;
                response.Message = $"{dtos.Count} entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            var response = new Response<IEnumerable<TDtoResult>>();

            try
            {
                var entities = await _context.Set<T>().Where(predicate).ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();
                response.Success = true;
                response.Data = dtos;
                response.Message = $"{dtos.Count} entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities with filter: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var response = new Response<IEnumerable<TDtoResult>>();

            try
            {
                IQueryable<T> query = _context.Set<T>().Where(predicate);

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                var entities = await query.ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();

                response.Success = true;
                response.Data = dtos;
                response.Message = $"{dtos.Count} entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities: {ex.Message}";
            }

            return response;
        }






        // Get Single
        // With Primary Key
        public async Task<Response<TDtoResult>> GetSingleAsync(TId id)
        {
            var response = new Response<TDtoResult>();

            try
            {
                var entities = await _context.Set<T>().FindAsync(id);
                var dtos = _mapper.Map<TDtoResult>(entities);

                response.Success = true;
                response.Data = dtos;
                response.Message = "Entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities with filter: {ex.Message}";
            }

            return response;
        }



        public async Task<Response<TDtoResult>> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            var response = new Response<TDtoResult>();

            try
            {
                var entities = await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
                var dtos = _mapper.Map<TDtoResult>(entities);

                response.Success = true;
                response.Data = dtos;
                response.Message = "Entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities with filter: {ex.Message}";
            }

            return response;
        }














        // With Includes
        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var response = new Response<IEnumerable<TDtoResult>>();

            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                var entities = await query.ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();


                response.Success = true;
                response.Data = dtos;
                response.Message = $"{dtos.Count} entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities: {ex.Message}";
            }

            return response;
        }



        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var response = new Response<IEnumerable<TDtoResult>>();

            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                var entities = await query.ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();


                response.Success = true;
                response.Data = dtos;
                response.Message = $"{dtos.Count} entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<IEnumerable<TDtoResult>>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            var response = new Response<IEnumerable<TDtoResult>>();

            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                var entities = await query.ToListAsync();
                var dtos = entities.Select(entity => _mapper.Map<TDtoResult>(entity)).ToList();


                response.Success = true;
                response.Data = dtos;
                response.Message = $"{dtos.Count} entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities: {ex.Message}";
            }

            return response;
        }







        public async Task<Response<TDtoResult>> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var response = new Response<TDtoResult>();

            try
            {
                IQueryable<T> query = _context.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                var entity = await query.FirstOrDefaultAsync();
                var dtos = _mapper.Map<TDtoResult>(entity);


                response.Success = true;
                response.Data = dtos;
                response.Message = "Entities retrieved successfully.";
            }
            catch (Exception ex)
            {
                //await ErrorLogger<ControlPanelDbContext>.LogErrorAsync(ex);
                response.Success = false;
                response.Message = $"Error retrieving entities: {ex.Message}";
            }

            return response;
        }


    }
}
