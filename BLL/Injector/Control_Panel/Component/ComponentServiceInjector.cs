
using BLL.Service.Control_Panel.Applications;
using BLL.Service.Control_Panel.Company_Service;
using BLL.Service.Control_Panel.Component_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Injector.Control_Panel.Component
{
    public static class ComponentServiceInjector
    {
        public static void ComponentServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {


            // ..................................................
            // ....................................... Get
            // ...................... Start


            // Get Component
            services.AddScoped<GetComponentService>();


            // Get Component with Application and Module
            services.AddScoped<GetComponentWithApplicationAndModuleService>();

            // ...................... End
            // ....................................... Get
            // ..................................................



            // ..................................................
            // ....................................... Create
            // ...................... Start


            services.AddScoped<CreateComponentService>();



            // ...................... End
            // ....................................... Create
            // ..................................................


        }
    }
}
