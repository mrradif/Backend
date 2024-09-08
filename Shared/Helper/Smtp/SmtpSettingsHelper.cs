using Microsoft.Extensions.Configuration;
using Shared.Settings.Smtp;

public static class SmtpSettingsHelper
{
    public static SmtpSettings GetSmtpSettings(IConfiguration configuration)
    {
        var smtpSettings = configuration.GetSection("SmtpSettings");
        return new SmtpSettings
        {
            SmtpServer = smtpSettings["SmtpServer"],
            SmtpPort = int.Parse(smtpSettings["SmtpPort"]), 
            SmtpUsername = smtpSettings["SmtpUsername"],
            SmtpPassword = smtpSettings["SmtpPassword"]
        };
    }
}
