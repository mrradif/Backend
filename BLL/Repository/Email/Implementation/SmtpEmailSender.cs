
//using BLL.Repository.Email.Interface;
//using System.Net;
//using System.Net.Mail;


//namespace BLL.Repository.Email.Implementation
//{
//    public class SmtpEmailSender : IEmailSender
//    {
//        private readonly string _smtpServer;
//        private readonly int _smtpPort;
//        private readonly string _smtpUsername;
//        private readonly string _smtpPassword;

//        public SmtpEmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
//        {
//            _smtpServer = smtpServer;
//            _smtpPort = smtpPort;
//            _smtpUsername = smtpUsername;
//            _smtpPassword = smtpPassword;
//        }

//        public async Task<(bool sentSuccessfully, string emailAddress)> SendEmailAsync(string email, string subject, string message)
//        {
//            return await SendEmailAsync(email, subject, message, isHtml: true);
//        }

//        public async Task<(bool sentSuccessfully, string emailAddress)> SendEmailAsync(string email, string subject, string message, bool isHtml)
//        {
//            using (var client = new SmtpClient(_smtpServer, _smtpPort))
//            {
//                client.UseDefaultCredentials = false;
//                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
//                client.EnableSsl = true;

//                var mailMessage = new MailMessage
//                {
//                    From = new MailAddress(_smtpUsername),
//                    Subject = subject,
//                    Body = message,
//                    IsBodyHtml = isHtml
//                };

//                mailMessage.To.Add(email);

//                try
//                {
//                    await client.SendMailAsync(mailMessage);
//                    return (true, email); 
//                }
//                catch (Exception ex)
//                {
//                    throw new ApplicationException($"Error sending email: {ex.Message}", ex);
//                }
//            }
//        }

//        public async Task<(bool sentSuccessfully, string emailAddress)> SendEmailFromTemplateAsync(string email, string subject, string templateKey, object templateData)
//        {
//            string templateHtml = LoadTemplateHtml(templateKey, templateData);

//            return await SendEmailAsync(email, subject, templateHtml, isHtml: true);
//        }

//        private string LoadTemplateHtml(string templateKey, object templateData)
//        {
//            switch (templateKey)
//            {
//                case "TwoFactorToken":
//                    return GetTwoFactorTokenEmailTemplate(templateData);
//                case "WelcomeEmail":
//                    return GetWelcomeEmailTemplate(templateData);
//                case "EmailConfirmation":
//                    return GetEmailConfirmationTemplate(templateData);
//                default:
//                    throw new ArgumentException($"Unknown template key: {templateKey}");
//            }
//        }

//        private string GetTwoFactorTokenEmailTemplate(object templateData)
//        {
//            string token = (string)templateData;

//            return $@"
//                <html>
//                <body>
//                    <h2 style='color: #007bff;'>Two-Factor Authentication Token</h2>
//                    <p>Your two-factor authentication token is: <strong>{token}</strong></p>
//                    <p>Please use this token to complete the login process.</p>
//                </body>
//                </html>";
//        }

//        private string GetWelcomeEmailTemplate(object templateData)
//        {
//            string username = (string)templateData;

//            return $@"
//                <html>
//                <body>
//                    <h2>Welcome to Our Application!</h2>
//                    <p>Dear {username},</p>
//                    <p>Thank you for joining our application.</p>
//                    <p>Best regards,</p>
//                    <p>The Application Team</p>
//                </body>
//                </html>";
//        }

//        private string GetEmailConfirmationTemplate(object templateData)
//        {
//            dynamic data = templateData;
//            string confirmationLink = data.ConfirmationLink;

//            return $@"
//                <html>
//                <body>
//                    <h2>Confirm Your Email</h2>
//                    <p>Thank you for registering. Please confirm your email by clicking the link below:</p>
//                    <p><a href='{confirmationLink}'>Confirm Email</a></p>
//                    <p>If you did not request this email, please ignore it.</p>
//                </body>
//                </html>";
//        }
//    }
//}




using BLL.Repository.Email.Interface;
using System.Net;
using System.Net.Mail;

namespace BLL.Repository.Email.Implementation
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public SmtpEmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public async Task<(bool sentSuccessfully, string emailAddress)> SendEmailAsync(string email, string subject, string message)
        {
            return await SendEmailAsync(email, subject, message, isHtml: true);
        }

        public async Task<(bool sentSuccessfully, string emailAddress)> SendEmailAsync(string email, string subject, string message, bool isHtml)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUsername),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isHtml
                };

                mailMessage.To.Add(email);

                try
                {
                    await client.SendMailAsync(mailMessage);
                    return (true, email);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error sending email: {ex.Message}", ex);
                }
            }
        }

        public async Task<(bool sentSuccessfully, string emailAddress)> SendEmailFromTemplateAsync(string email, string subject, string templateKey, object templateData)
        {
            string templateHtml = LoadTemplateHtml(templateKey, templateData);

            return await SendEmailAsync(email, subject, templateHtml, isHtml: true);
        }

        private string LoadTemplateHtml(string templateKey, object templateData)
        {
            switch (templateKey)
            {
                case "TwoFactorToken":
                    return GetTwoFactorTokenEmailTemplate(templateData);
                case "WelcomeEmail":
                    return GetWelcomeEmailTemplate(templateData);
                case "EmailConfirmation":
                    return GetEmailConfirmationTemplate(templateData);
                default:
                    throw new ArgumentException($"Unknown template key: {templateKey}");
            }
        }

        private string GetTwoFactorTokenEmailTemplate(object templateData)
        {
            string token = (string)templateData;

            return $@"
                <html>
                <body>
                    <h2 style='color: #007bff;'>Two-Factor Authentication Token</h2>
                    <p>Your two-factor authentication token is: <strong>{token}</strong></p>
                    <p>Please use this token to complete the login process.</p>
                </body>
                </html>";
        }

        private string GetWelcomeEmailTemplate(object templateData)
        {
            string username = (string)templateData;

            return $@"
                <html>
                <body>
                    <h2>Welcome to Our Application!</h2>
                    <p>Dear {username},</p>
                    <p>Thank you for joining our application.</p>
                    <p>Best regards,</p>
                    <p>The Application Team</p>
                </body>
                </html>";
        }

        private string GetEmailConfirmationTemplate(object templateData)
        {
            dynamic data = templateData;
            string confirmationLink = data.ConfirmationLink;

            return $@"
        <html>
        <body style='font-family: Arial, sans-serif; color: #333;'>
            <div style='max-width: 800px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>

                <h2 style='color: #007bff;'>Confirm Your Email</h2>
                <p>Thank you for registering. Please confirm your email by clicking the link below:</p>
                <span style='text-align: center;'>
                    <a href='{confirmationLink}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Confirm Email</a>
                </span>
                <p>If you did not request this email, please ignore it.</p>
            </div>
        </body>
        </html>";
        }

    }
}
