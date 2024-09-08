

using BLL.Service.Control_Panel.Button_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Injector.Control_Panel.Module
{
    public static class ButtonServiceInjector
    {
        public static void ButtonServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {


            // ..................................................
            // ....................................... Get
            // ...................... Start


            // Get Button
            services.AddScoped<GetButtonService>();


            // Get Button with Components
            services.AddScoped<GetButtonWithComponentService>();



            // ...................... End
            // ....................................... Get
            // ..................................................





            // ..................................................
            // ....................................... Create
            // ...................... Start


            services.AddScoped<CreateButtonService>();



            // ...................... End
            // ....................................... Create
            // ..................................................



        }
    }
}
