using System;
using System.Threading.Tasks;
using Bilbayt.Core.Interfaces.Email;
using Bilbayt.Infrastructure.AppSettings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Bilbayt.Infrastructure.Services
{
    public class SendGridEmailService : IEmailService
    {
        /// <summary>
        ///     Settings
        /// </summary>
        private readonly SendGridEmailSettings _sendGridEmailSettings;

        /// <summary>
        ///     Send Grid wrapper
        /// </summary>
        private readonly SendGridClient _sendGridClient;

        /// <summary>
        ///     FromEmail from the settings
        /// </summary>
        private string FromEmail => _sendGridEmailSettings.FromEmail;

        /// <summary>
        ///     FromName from the settings
        /// </summary>
        private string FromName => _sendGridEmailSettings.FromName;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="sendGridEmailSettings"></param>
        public SendGridEmailService(IOptions<SendGridEmailSettings> sendGridEmailSettings)
        {
            _sendGridEmailSettings = sendGridEmailSettings.Value ?? throw new ArgumentNullException(nameof(sendGridEmailSettings));
            _sendGridClient = new SendGridClient(_sendGridEmailSettings.SendGridApiKey);

        }

      
        /// <summary>
        ///     Send message
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="toName"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string toEmail, string toName, string subject, string message)
        {
            var sendGridMessage = MailHelper.CreateSingleEmail(
                    new EmailAddress(this.FromEmail, this.FromName),
                    new EmailAddress(toEmail, toName),
                    subject,
                    null,
                    message
                );

            await _sendGridClient.SendEmailAsync(sendGridMessage);
        }
    }


}
