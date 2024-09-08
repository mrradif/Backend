using Shared.Data.Application;
using Shared.Data.OnOff;
using Shared.Helper.Configuration;


namespace Shared.Settings
{
    public static class AppSettings
    {
        internal static string apiUrl = ApiUrl.ITX;
        internal static string clientOrigin = ClientUrl.ITX;

        public static string App_environment = ConfigurationHelper.config.GetSection("App_Environment").Value.ToString();
        public static string Origin
        {
            get
            {
                return ConfigurationHelper.config.GetSection("Clients").GetSection("AngularClient").GetSection("Url").Value;
            }
        }

        public static bool EmailService
        {
            get
            {
                var emailservice = ConfigurationHelper.config.GetSection("EmailService").Value.ToString();
                if (emailservice.ToLower() == SwitchOnOff.On.ToLower())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public static string SymmetricSecurityKey
        {
            get
            {
                return "Y75Np55qVRutusfFERqE3jmIwByFA3IJoMLM0mgZX6ycpgauNwrJKQL5IQHy / eka";
            }
        }


        public static string Key
        {
            get
            {
                return "7391824694761634";
            }
        }


        public static string ClientOrigin
        {
            get
            {
                if (App_environment != null)
                {
                    if (App_environment == ApplicationEnvironment.Local)
                    {
                        return ClientUrl.Local;
                    }
                    else if (App_environment == ApplicationEnvironment.Public)
                    {
                        return clientOrigin;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }



        public static string ApiValidIssuer
        {
            get
            {
                if (App_environment != null)
                {
                    if (App_environment == ApplicationEnvironment.Local)
                    {
                        return ApiUrl.Local;
                    }
                    else if (App_environment == ApplicationEnvironment.Public)
                    {
                        return apiUrl;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }


        public static string ApiValidAudience
        {
            get
            {

                if (App_environment != null)
                {
                    if (App_environment == ApplicationEnvironment.Local)
                    {
                        return ApiUrl.Local;
                    }
                    else if (App_environment == ApplicationEnvironment.Public)
                    {
                        return apiUrl;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }



        public static string ConnectionString(string dbName)
        {
            if (apiUrl == ApiUrl.Hris)
            {
                return string.Format(@"server=192.168.10.92;database={0};user id=sa;password={1};multipleactiveresultsets=true;Trust Server Certificate = True", dbName, "sarec0m2o2@");
            }
            else if (apiUrl == ApiUrl.Wounderman)
            {
                return string.Format(@"server=BDWTHR\BDWTHRSQL2019;database={0};user id=recom;password=r3c0m@WT!321;multipleactiveresultsets=true", dbName);
            }
            else if (apiUrl == ApiUrl.AgaKhan)
            {
                return string.Format(@"server=192.168.10.92;database={0};user id=sa;password=;multipleactiveresultsets=true", dbName);
            }
            else if (apiUrl == ApiUrl.ITX)
            {
                return string.Format(@"server=192.168.10.92;database={0};user id=sa;password={1};multipleactiveresultsets=true;Trust Server Certificate = True", dbName, "sarec0m2o2@");
            }
            else if (apiUrl == ApiUrl.PWC)
            {
                return string.Format(@"Server=tcp:ipzrgmsusssp001.database.windows.net,1433;Initial Catalog={0};User ID=gtyagi006;Password=ReCom#?2023;MultipleActiveResultSets=true", dbName);
            }
            return "";
        }



        public static string PayslipApiURL = ConfigurationHelper.config.GetSection("PayslipUri").Value.ToString();
        public static string TaxCardURL = ConfigurationHelper.config.GetSection("TaxCardUri").Value.ToString();
        public static string BonusPayslipApiURL = ConfigurationHelper.config.GetSection("BonusPayslipUri").Value.ToString();
        public static string PayslipApiKey = ConfigurationHelper.config.GetSection("Key").Value.ToString();


    }
}
