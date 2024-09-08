using BLL.Service.Control_Panel.Create;
using BLL.Service.Control_Panel.Organization_Service;
using BLL.Service.Control_Panel.Organization_Policy_Config_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Injector.Control_Panel.Organization_Policy_Config
{
    public static class OrganizationPolicyConfigServiceInjector
    {
        public static void OrganizationPolicyConfigServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            // ..................................................
            // ....................................... Get
            // ...................... Start

            // Get OrganizationPolicyConfigs
            services.AddScoped<GetOrganizationPolicyConfigService>();

            // Get Organization Config With Includes Company Config
            services.AddScoped<GetOrganizationPolicyConfigWithCompanyPolicyConfigService>();

            // Get Organization Config Without Includes Company Config
            services.AddScoped<GetOrganizationConfigWithCompanyConfigService>();




            // ...................... End
            // ....................................... Get
            // ..................................................



            // ..................................................
            // ....................................... Create
            // ...................... Start


            // Create OrganizationPolicyConfig With Add and AddRange

            // Master Details with Includes
            services.AddScoped<CreateOrganizationConfigIncludeCompanyConfigService>();

            // Master Details without Includes
            services.AddScoped<CreateOrganizationConfigWithCompanyConfigService>();

            // Single
            services.AddScoped<CreateOrganizationPolicyConfigService>();

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


            // Delete Organization Config With Company Config
            services.AddScoped<DeleteOrganizationConfigWithCompanyConfigService>();

            // ...................... End
            // ....................................... Delete
            // ..................................................







        }
    }
}