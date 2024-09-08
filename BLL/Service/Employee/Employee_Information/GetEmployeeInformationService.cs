using AutoMapper;
using BLL.Repository.Generic.Interface.Get;
using DAL.Context.Control_Panel;
using Shared.DTO.Common;
using Shared.Domain.Employee;
using System.Linq.Expressions;
using Shared.View.Employee.Employee_Information;
using Shared.Criteria.Employee.Employee_Information;
using DAL.Context.Employee;

namespace BLL.Service.Employee.Employee_Information
{
    public class GetEmployeeInformationService
    {
        private readonly IGetGenericRepo<EmployeeInformation, EmployeeDbContext, GetEmployeeInformationResultViewModel, long> _getEmployeeInformationRepository;
        private readonly IMapper _mapper;

        public GetEmployeeInformationService(
            IGetGenericRepo<EmployeeInformation, EmployeeDbContext, GetEmployeeInformationResultViewModel, long> getEmployeeInformationRepository,
            IMapper mapper
            )
        {
            _getEmployeeInformationRepository = getEmployeeInformationRepository ?? throw new ArgumentNullException(nameof(getEmployeeInformationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<IEnumerable<GetEmployeeInformationResultViewModel>>> GetAllEmployeeInformationAsync()
        {
            var response = await _getEmployeeInformationRepository.GetAllAsync();
            return response;
        }

        public async Task<Response<IEnumerable<GetEmployeeInformationResultViewModel>>> GetActiveEmployeeInformationAsync()
        {
            var response = await _getEmployeeInformationRepository.GetAllAsync(e => e.IsDeleted == false);
            return response;
        }

        public async Task<Response<IEnumerable<GetEmployeeInformationResultViewModel>>> GetEmployeeInformationByCriteriaAsync(EmployeeSearchCriteria criteria)
        {
            Expression<Func<EmployeeInformation, bool>> predicate = employee =>
                (string.IsNullOrEmpty(criteria.EmployeeCode) || employee.EmployeeCode.Contains(criteria.EmployeeCode)) &&
                (string.IsNullOrEmpty(criteria.FullName) || employee.FullName.Contains(criteria.FullName)) &&
                (!criteria.IsActive || employee.IsActive);

            var response = await _getEmployeeInformationRepository.GetAllAsync(predicate);
            return response;
        }

        public async Task<Response<IEnumerable<GetEmployeeInformationResultViewModel>>> GetEmployeeInformationByCriteriaAsync(Expression<Func<EmployeeInformation, bool>> predicate)
        {
            return await _getEmployeeInformationRepository.GetAllAsync(predicate);
        }

        public async Task<Response<IEnumerable<GetEmployeeInformationResultViewModel>>> GetEmployeeInformationWithOrderingAsync(EmployeeSearchCriteria criteria)
        {
            Expression<Func<EmployeeInformation, bool>> predicate = employee =>
                (string.IsNullOrEmpty(criteria.EmployeeCode) || employee.EmployeeCode.Contains(criteria.EmployeeCode)) &&
                (string.IsNullOrEmpty(criteria.FullName) || employee.FullName.Contains(criteria.FullName)) &&
                (!criteria.IsActive || employee.IsActive);

            Func<IQueryable<EmployeeInformation>, IOrderedQueryable<EmployeeInformation>> orderBy = null;
            orderBy = q => q.OrderByDescending(employee => employee.EmployeeId);

            var response = await _getEmployeeInformationRepository.GetAllAsync(predicate, orderBy);
            return response;
        }

        // Get Single

        public async Task<Response<GetEmployeeInformationResultViewModel>> GetSingleEmployeeByIdAsync(long employeeId)
        {
            return await _getEmployeeInformationRepository.GetSingleAsync(employeeId);
        }

        public async Task<Response<GetEmployeeInformationResultViewModel>> GetSingleEmployeeByCriteriaAsync(EmployeeSearchCriteria criteria)
        {
            Expression<Func<EmployeeInformation, bool>> predicate = employee =>
                (string.IsNullOrEmpty(criteria.EmployeeCode) || employee.EmployeeCode.Contains(criteria.EmployeeCode)) &&
                (string.IsNullOrEmpty(criteria.FullName) || employee.FullName.Contains(criteria.FullName)) &&
                (!criteria.IsActive || employee.IsActive);

            var response = await _getEmployeeInformationRepository.GetSingleAsync(predicate);
            return response;
        }

        public async Task<Response<GetEmployeeInformationResultViewModel>> GetSingleEmployeeByCriteriaAsync(Expression<Func<EmployeeInformation, bool>> predicate)
        {
            return await _getEmployeeInformationRepository.GetSingleAsync(predicate);
        }
    }
}
