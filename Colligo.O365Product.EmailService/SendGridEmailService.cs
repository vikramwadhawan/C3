
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.EmailService
{
    public class SendGridEmailService : Colligo.O365Product.ServiceInterface.EmailService
    {
        protected readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(SendGridEmailService));

        public SendGridEmailService(string apiKey)
        {
            SendGridApiKey = apiKey;
            To = new List<string>();
        }

        public string SendGridApiKey { get; }

        private List<EmailAddress> GenerateEmailAddress(List<string> address)
        {
            var listEmailAddresses = new List<EmailAddress>();
            if (address != null && address.Count > 0)
            {
                foreach (var item in address)
                {
                    listEmailAddresses.Add(new EmailAddress(email: item));
                }
            }
            return listEmailAddresses;
        }

        public override async Task<string> SendEmailAsync()
        {
            try
            {
                var apiKey = SendGridApiKey;
                var fromEmail = new EmailAddress(FromAddress);
                var client = new SendGridClient(apiKey);
                var message = new SendGridMessage();
                message.AddTos(GenerateEmailAddress(To));
                message.From = fromEmail;
                message.AddHeader("recover", "Account Recovery");
                message.Subject = Subject;
                message.HtmlContent = Body;
                Response response = await client.SendEmailAsync(message);
                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    return "success";
            }
            catch (Exception ex)
            {
                _logger.Error("error in SendEmailAsync", ex);
            }
            return "fail";

        }
    }
}
