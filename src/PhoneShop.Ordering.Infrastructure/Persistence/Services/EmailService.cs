using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhoneShop.Ordering.Application.Common.Interfaces;
using PhoneShop.Ordering.Domain.Common;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PhoneShop.Ordering.Infrastructure.Persistence.Services;

public class EmailService : IEmailService
{
    public EmailSetting _setting { get; }
    public ILogger<EmailService> _logger { get; }

    public EmailService(IOptions<EmailSetting> setting, ILogger<EmailService> logger)
    {
        _setting = setting.Value;
        _logger = logger;
    }

    public async Task<bool> SendMailAsync(Email email)
    {
        var client = new SendGridClient(_setting.ApiKey);
        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = _setting.FromAddress,
            Name = _setting.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        _logger.LogInformation("Email sent.");

        if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            return true;

        _logger.LogError("Email sending failed.");

        return false;
    }
}
