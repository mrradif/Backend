
using BLL.Injector.Applications;
using BLL.Injector.Control_Panel.Component;
using BLL.Injector.Control_Panel.Module;
using BLL.Injector.Control_Panel.Organization_Policy_Config;
using BLL.Injector.Employee.Employee_Information;
using BLL.Injector.Modules;
using BLL.Injector.Organization;
using BLL.Injector.User;
using BLL.Repository.Email.Implementation;
using BLL.Repository.Email.Interface;
using DAL.Context.Control_Panel;
using DAL.Service.Logger;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Helper.File;


namespace BLL.Injector
{
    public static class BLLInjector
    {
        public static void BLLConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
           

            // Context Accessor
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<ControlPanelDbContext>();
            services.AddScoped<ErrorLogger>(); 




            // ..........................................................................
            // ................................................. Services
            // .................................... Start

            UserServiceInjector.UserServiceConfigureServices(services, configuration);
            BLLServiceInjector.BLLServiceConfigureServices(services, configuration);
            OrganizationServiceInjector.OrganizationServiceConfigureServices(services, configuration);
            OrganizationPolicyConfigServiceInjector.OrganizationPolicyConfigServiceConfigureServices(services, configuration);

            // Apllication
            ApplicationServiceInjector.ApplicationServiceConfigureServices(services, configuration);

            // Module
            ModuleServiceInjector.ModuleServiceConfigureServices(services, configuration);



            // Employee
            EmployeeInformationServiceInjector.EmployeeInformationServiceConfigureServices(services, configuration);    



            // Component
            ComponentServiceInjector.ComponentServiceConfigureServices(services, configuration);

            // Button
            ButtonServiceInjector.ButtonServiceConfigureServices(services, configuration);



            // .................................... End
            // ................................................. Services
            // ..........................................................................


            // Email Sending

            services.AddSingleton<IEmailSender>(new SmtpEmailSender(
                SmtpSettingsHelper.GetSmtpSettings(configuration).SmtpServer,
                SmtpSettingsHelper.GetSmtpSettings(configuration).SmtpPort,
                SmtpSettingsHelper.GetSmtpSettings(configuration).SmtpUsername,
                SmtpSettingsHelper.GetSmtpSettings(configuration).SmtpPassword
            ));


            // File Save
            services.AddScoped<FileManager>(provider =>
            {
                var uploadsDirectory = configuration["UploadsDirectory"];
                return new FileManager(uploadsDirectory);
            });



        }
    }
}
