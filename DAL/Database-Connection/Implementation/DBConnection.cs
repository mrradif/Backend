
using DAL.Dapper_Object;
using DAL.Database_Context.Database_Connection.Interface;
using Microsoft.AspNetCore.Http;



namespace DAL.Database_Context.Database_Connection.Implementation
{
    public class DBConnection : IDBConnection
    {
        public string GetControlPanelConnectionString()
        {
            var connectionString = Database.GetConnectionString(Database.ControlPanel);
            return connectionString;
        }


        public string GetHRMSConnectionString()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null || httpContext.Request == null)
            {
                return Database.GetConnectionString(Database.HRMS);
            }

            var storageName = UserHelper.StorageName();
            var connectionString = Database.GetConnectionString(storageName);

            return connectionString;
        }


    }
}
