

namespace BLL.Repository.Email.Interface
{
    public interface IEmailSender
    {
        Task<(bool sentSuccessfully, string emailAddress)> SendEmailAsync(string email, string subject, string message);

        Task<(bool sentSuccessfully, string emailAddress)> SendEmailAsync(string email, string subject, string message, bool isHtml);

        Task<(bool sentSuccessfully, string emailAddress)> SendEmailFromTemplateAsync(string email, string subject, string templateKey, object templateData);
    }
}
