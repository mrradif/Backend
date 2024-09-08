

using BLL.Repository.Access.Implementation;
using BLL.Repository.Access.Interface;
using BLL.Repository.Identity.Role.Implementation;
using BLL.Repository.Identity.User.Implementation;
using BLL.Repository.Identity.User.Interface;
using BLL.Service.Control_Panel.Check;
using BLL.Service.Control_Panel.User_Creation_Config_Service;
using DAL.Context.Control_Panel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Control_Panel.Identity;

namespace BLL.Injector.User
{
    public static class UserServiceInjector
    {
        public static void UserServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                // Password settings
                config.Password.RequireUppercase = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireDigit = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequiredLength = 6;

                // Lockout settings
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Lockout.AllowedForNewUsers = true;

                // Email settings
                config.User.RequireUniqueEmail = true;


                // Two Factor Authentication settings
                config.SignIn.RequireConfirmedEmail = false;

            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ControlPanelDbContext>();


            // Token Validity
            // Confrim Email & All Token
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(24);
            });




            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();

            services.AddScoped<IUserConfigsRepo, UserConfigsRepo>();




            // Login

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<CheckUsernameService>();
            services.AddScoped<IAccessRepo, AccessRepo>();




            // User Creation Config

            services.AddScoped<GetCreateUserCreationConfigService>();
            services.AddScoped<GetUserCreationConfigService>();




        }
    }
}
