using Shared.Data.Application;
using Shared.Helper.Configuration;
using Shared.Settings;


namespace DAL.Dapper_Object
{
    public static class Database
    {
        static string AppEnvironment = AppSettings.App_environment;


        // For Migration
        // ............................ Starting
        // ...............................................................

        public static string ControlPanel { get { return "CPanel"; } }
        public static string HRMS { get { return "HRIS"; } }
        public static string Payroll { get { return "HRIS"; } }

        // For Migration
        // ............................ Ending
        // ...............................................................



        public static string GetConnectionString(string dbName)
        {
            if (dbName != null) {
                if (dbName == "ControlPanel") {
                    return MakeConnectionString(dbName);
                }
                else {
                    return MakeConnectionString(dbName);
                }
            }
            else {
                throw new System.Exception("Database name is empty");
            }
        }



        private static string MakeConnectionString(string dbName)
        {
            string fullConString = string.Empty;

            if (AppEnvironment == ApplicationEnvironment.Local) {

                fullConString = string.Format(@ConfigurationHelper.config.GetSection("ConnectionStrings").
                    GetSection(ConfigurationHelper.config.GetSection("Active_ConnectionString").Value).Value, dbName);

            }

            else if (AppEnvironment == ApplicationEnvironment.Public) {
                return AppSettings.ConnectionString(dbName);
            }
            return fullConString;
        }



    }
}
