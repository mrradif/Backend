using BLL.Repository.Generic.Implementation.Remove;
using BLL.Repository.Generic.Implementation.Get;
using BLL.Repository.Generic.Implementation.Post;
using BLL.Repository.Generic.Implementation.Put;
using BLL.Repository.Generic.Interface.Remove;
using BLL.Repository.Generic.Interface.Get;
using BLL.Repository.Generic.Interface.Post;
using BLL.Repository.Generic.Interface.Put;
using BLL.Repository.Generic_Master_Detail.Implementation.Delete;
using BLL.Repository.Generic_Master_Detail.Implementation.Get;
using BLL.Repository.Generic_Master_Detail.Implementation.Post;
using BLL.Repository.Generic_Master_Detail.Implementation.Put;
using BLL.Service.Control_Panel.Organization_Policy_Config_Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BLL.Repository.Generic.Interface.Delete;
using BLL.Repository.Generic.Implementation.Delete;
using BLL.Service.Control_Panel.Delete;
using BLL.Service.Control_Panel.Create;
using BLL.Service.Control_Panel.Organization_Service;
using BLL.Service.Control_Panel.Update;
using BLL.Service.Control_Panel.Company_Service;
using BLL.Service.Control_Panel.Update.Policy_Config;


namespace BLL.Injector
{
    public static class BLLServiceInjector
    {
        public static void BLLServiceConfigureServices(IServiceCollection services, IConfiguration configuration)
        {


            // Register Generic Repository
            services.AddScoped(typeof(IGetGenericRepo<,,,>), typeof(GetGenericRepo<,,,>));
            services.AddScoped(typeof(IGetGenericRepoWithPagination<,,,>), typeof(GetGenericRepoWithPagination<,,,>));

            services.AddScoped(typeof(IPostGenericRepo<,,,>), typeof(PostGenericRepo<,,,>));
            services.AddScoped(typeof(IPutGenericRepo<,,,>), typeof(PutGenericRepo<,,,>));
            services.AddScoped(typeof(IDeleteGenericRepo<,,,>), typeof(DeleteGenericRepo<,,,>));
            services.AddScoped(typeof(IRemoveGenericRepo<,,,>), typeof(RemoveGenericRepo<,,,>));


            // Register Generic Master Detail Repository

            services.AddScoped(typeof(IGetGenericMasterDetailRepo<,,,,>), typeof(GetGenericMasterDetailRepo<,,,,>));
            services.AddScoped(typeof(IPostGenericMasterDetailRepo<,,,,,,>), typeof(PostGenericMasterDetailRepo<,,,,,,>));
            services.AddScoped(typeof(IPutGenericMasterDetailRepo<,,,,,,>), typeof(PutGenericMasterDetailRepo<,,,,,,>));
            services.AddScoped(typeof(IDeleteGenericMasterDetailRepo<,,,,>), typeof(DeleteGenericMasterDetailRepo<,,,,>));





            services.AddScoped<GetOrganizationIncludesCompanyService>();
         




            services.AddScoped<GetCompanyService>();



            services.AddScoped<CreateCompanyService>();
            services.AddScoped<UpdateCompanyService>();
            services.AddScoped<DeleteCompanyService>();


            
           



            services.AddScoped<CreateCompanyPolicyConfigService>();





            services.AddScoped<UpdateOrganizationMasterDetailService>();






        }
    }
}
