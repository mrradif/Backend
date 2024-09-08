

using BLL.Service.Control_Panel.Create;
using BLL.Service.Control_Panel.Organization_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Injector.Organization
{
    public static class OrganizationServiceInjector
    {
        public static void OrganizationServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            // ..................................................
            // ....................................... Get
            // ...................... Start

            // Get Organizations
            services.AddScoped<GetOrganizationService>();

            // Get Organizations With Includes Company
            services.AddScoped<GetOrganizationIncludesCompanyService>();

            // Get Organizations And Company Without Includes
            services.AddScoped<GetOrganzationWithCompanyService>();



            // ...................... End
            // ....................................... Get
            // ..................................................



            // ..................................................
            // ....................................... Create
            // ...................... Start


            // Create Organization With File
            services.AddScoped<CreateOrganizationWithFileService>();

            // Create Organization With Add and AddRange
            services.AddScoped<CreateOrganizationService>();

            // Master Details with Includes
            services.AddScoped<CreateOrganizationWithIncludeCompanyService>();


            // ...................... End
            // ....................................... Create
            // ..................................................




            // ..................................................
            // ....................................... Update
            // ...................... Start


            services.AddScoped<UpdateOrganizationService>();


            // ...................... End
            // ....................................... Update
            // ..................................................




            // ..................................................
            // ....................................... Delete
            // ...................... Start

            services.AddScoped<DeleteOrganizationsService>();

            // ...................... End
            // ....................................... Delete
            // ..................................................


            // ..................................................
            // ....................................... Remove
            // ...................... Start


            services.AddScoped<RemoveOrganizationService>();


            // Delete Organization With Company
            services.AddScoped<DeleteOrganizationWithCompanyService>();

            // ...................... End
            // ....................................... Remove
            // ..................................................







        }
    }
}