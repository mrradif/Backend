using API.Controllers.Control_Panel.Applications.Get;
using BLL.Service.Control_Panel.Applications.Create;
using BLL.Service.Control_Panel.Applications.Delete;
using BLL.Service.Control_Panel.Applications.Get;
using BLL.Service.Control_Panel.Applications.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BLL.Injector.Applications
{
    public static class ApplicationServiceInjector
    {
        public static void ApplicationServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {


            // ..................................................
            // ....................................... Get
            // ...................... Start

            // Get Application
            services.AddScoped<GetApplicationService>();

            // Get Application With Pagination
            services.AddScoped<GetApplicationWithPaginationService>();

            // Get Application with Modules
            services.AddScoped<GetApplicationWithModuleService>();

            // Get Application And Module With Pagination
            services.AddScoped<GetApplicationsAndModulesWithPaginationService>();


            // Get Application with Includes
            services.AddScoped<GetApplicationWithIncludesService>();

            // Get Application Includes With Pagination
            services.AddScoped<GetApplicationIncludesWithPaginationService>();

            // ...................... End
            // ....................................... Get
            // ..................................................




            // ..................................................
            // ....................................... Create
            // ...................... Start


            services.AddScoped<CreateApplicationService>();



            // ...................... End
            // ....................................... Create
            // ..................................................



            // ..................................................
            // ....................................... Update
            // ...................... Start


            services.AddScoped<UpdateApplicationService>();


            // ...................... End
            // ....................................... Update
            // ..................................................




            // ..................................................
            // ....................................... Delete
            // ...................... Start


            services.AddScoped<DeleteApplicationService>();


            // ...................... End
            // ....................................... Delete
            // ..................................................
        }
    }
}
