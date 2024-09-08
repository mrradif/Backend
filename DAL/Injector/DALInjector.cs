using DAL.Context.Control_Panel;
using DAL.Context.Dapper;
using DAL.Context.Employee;
using DAL.Dapper_Object.Implementation;
using DAL.Dapper_Object.Interface;
using DAL.Database_Context.Database_Connection.Implementation;
using DAL.Database_Context.Database_Connection.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Control_Panel.Identity;


namespace DAL.Injector
{
    public static class DALInjector
    {




        private static void RegisterControlPanelDbContext<TContext>(IServiceCollection services)
               where TContext : DbContext
        {
            services.AddDbContext<TContext>((serviceProvider, options) => {
                var dbConnection = serviceProvider.GetRequiredService<IDBConnection>();
                options.UseSqlServer(dbConnection.GetControlPanelConnectionString());
            });
        }


        private static void RegisterHRISDbContext<TContext>(IServiceCollection services)
       where TContext : DbContext
        {
            services.AddDbContext<TContext>((serviceProvider, options) => {
                var dbConnection = serviceProvider.GetRequiredService<IDBConnection>();
                options.UseSqlServer(dbConnection.GetHRMSConnectionString());
            });
        }


        public static void DALConfigureServices(IServiceCollection services)
        {




            services.AddScoped<IDBConnection, DBConnection>();


            // Dapper
            services.AddSingleton<DapperDbContext>();
            services.AddTransient<IDapperData, DapperData>();


            // DB Context
            RegisterControlPanelDbContext<ControlPanelDbContext>(services);


            // Employee
            RegisterHRISDbContext<EmployeeDbContext>(services);



        }

    }
}
