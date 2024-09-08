
using BLL.Service.Employee.Employee_Information;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BLL.Injector.Employee.Employee_Information
{
    public static class EmployeeInformationServiceInjector
    {
        public static void EmployeeInformationServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {


            // ..................................................
            // ....................................... Get
            // ...................... Start


            // Get Application
            services.AddScoped<GetEmployeeInformationService>();


          


            // ...................... End
            // ....................................... Get
            // ..................................................


            // ..................................................
            // ....................................... Create
            // ...................... Start


          


            // ...................... End
            // ....................................... Create
            // ..................................................



            // ..................................................
            // ....................................... Update
            // ...................... Start


          


            // ...................... End
            // ....................................... Update
            // ..................................................




            // ..................................................
            // ....................................... Delete
            // ...................... Start


          


            // ...................... End
            // ....................................... Delete
            // ..................................................
        }
    }
}
