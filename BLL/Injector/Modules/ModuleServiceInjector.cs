using BLL.Service.Control_Panel.Modules.Create;
using BLL.Service.Control_Panel.Modules.Delete;
using BLL.Service.Control_Panel.Modules.Get;
using BLL.Service.Control_Panel.Modules.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Injector.Modules
{
    public static class ModuleServiceInjector
    {
        public static void ModuleServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // ..................................................
            // ....................................... Get
            // ...................... Start

            // Get Module
            services.AddScoped<GetModuleService>();

            // Get Module With Pagination
            services.AddScoped<GetModuleWithPaginationService>();

            // Get Module With Component
            services.AddScoped<GetModuleWithComponentsService>();

            // Get Module With Application
            services.AddScoped<GetModuleWithApplicationService>();


            // Get Module With Includes
            services.AddScoped<GetModuleWithIncludesService>();

            // ...................... End
            // ....................................... Get
            // ..................................................


            // ..................................................
            // ....................................... Create
            // ...................... Start

            services.AddScoped<CreateModuleService>();

            // ...................... End
            // ....................................... Create
            // ..................................................


            // ..................................................
            // ....................................... Update
            // ...................... Start

            services.AddScoped<UpdateModuleService>();

            // ...................... End
            // ....................................... Update
            // ..................................................


            // ..................................................
            // ....................................... Delete
            // ...................... Start

            services.AddScoped<DeleteModuleService>();

            // ...................... End
            // ....................................... Delete
            // ..................................................
        }
    }
}
