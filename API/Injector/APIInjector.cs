
using BLL.Injector;
using DAL.Injector;
using Shared.Injector;

namespace API.Injector
{
    public static class APIInjector
    {
        public static void APIConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            // Injecting BLL
            BLLInjector.BLLConfigureServices(services, configuration);


            // Injecting DAL
            DALInjector.DALConfigureServices(services);

            // Injecting Shared
            SharedInjector.SharedConfigureServices(services, configuration);

        }
    }
}
