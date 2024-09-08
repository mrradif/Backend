using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Auto_Mapper.Control_Panel.Administration;
using Shared.Auto_Mapper.Control_Panel.Administration.Application_Map;
using Shared.Auto_Mapper.Control_Panel.Administration.Button_Map;
using Shared.Auto_Mapper.Control_Panel.Administration.Company_Map;
using Shared.Auto_Mapper.Control_Panel.Administration.Component_Map;
using Shared.Auto_Mapper.Control_Panel.Administration.Module_Map;
using Shared.Auto_Mapper.Control_Panel.Administration.Organization_Map;
using Shared.Auto_Mapper.Control_Panel.Administration.Organization_Policy_Config_Map;
using Shared.Auto_Mapper.Control_Panel.Identity;
using Shared.Auto_Mapper.Control_Panel.Master_Detail;
using Shared.Auto_Mapper.Employee.Employee_Information_Map;
using Shared.Helper.Configuration;


namespace Shared.Injector
{
    public static class SharedInjector
    {
        public static void SharedConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurationHelper.Initialize(configuration);

            // Auto Mapper 

            // Identity
            services.AddAutoMapper(typeof(IdentityMappingProfile));


            // User Creation Config
            services.AddAutoMapper(typeof(UserCreationConfigMappingProfile));



            services.AddAutoMapper(typeof(OrganizationMappingProfile));
            services.AddAutoMapper(typeof(CompanyMappingProfile));


            // Application
            services.AddAutoMapper(typeof(AppplicationMappingProfile));

            // Module
            services.AddAutoMapper(typeof(ModuleMappingProfile));


            services.AddAutoMapper(typeof(MasterDetailMappingProfile));


            services.AddAutoMapper(typeof(OrganizationPolicyConfigMappingProfile));
            services.AddAutoMapper(typeof(CompanyPolicyConfigMappingProfile));



            // Employee
            services.AddAutoMapper(typeof(EmployeeInformationMappingProfile));


            // Component
            services.AddAutoMapper(typeof(ComponentMappingProfile));

            // Button
            services.AddAutoMapper(typeof(ButtonMappingProfile));

        }
    }
}
