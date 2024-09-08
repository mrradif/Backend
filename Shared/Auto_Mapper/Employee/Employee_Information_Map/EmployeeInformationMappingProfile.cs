

using AutoMapper;
using Shared.Domain.Employee;
using Shared.View.Employee.Employee_Information;


namespace Shared.Auto_Mapper.Employee.Employee_Information_Map
{
    public class EmployeeInformationMappingProfile:Profile
    {
        public EmployeeInformationMappingProfile()
        {

            // ....................................................................
            // .......................................Get & Delete
            // ............................ Start

            // Get Application
            CreateMap<EmployeeInformation, GetEmployeeInformationResultViewModel>();


          



            // ............................ End
            // .......................................Get & Delete
            // ....................................................................
        }
    }
}
