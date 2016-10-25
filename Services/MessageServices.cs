using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatacircleAPI.Models;
using DatacircleAPI.Settings;
using Microsoft.Extensions.Options;

namespace DatacircleAPI.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly MailTemplateService _mailTemplateService;

        public AuthMessageSender(IOptions<EmailSettings> emailSettings, MailTemplateService mailTemplateService)
        {
            this._emailSettings = emailSettings.Value;
            this._mailTemplateService = mailTemplateService;
        }

        public async Task SendEmailAsync(Email email)
        {
            // curl -s --user 'api:key-0604f8e40b66561d05196dd9ec2d685f' \
            //  https://api.mailgun.net/v3/datacircle.io/messages \
            //  -F from='DataCricle <mailgun@datacircle.io>' \
            //  -F to=z.chris.92@gmail.com \
            //  -F subject='TEST 1' \
            //  -F text='Testing some Mailgun awesomness!'

            using (var client = new HttpClient { BaseAddress = new Uri(_emailSettings.BaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(this._emailSettings.ApiKey)));

                email.Message = this._mailTemplateService.PrepareContent(email);
                email.Subject = this._mailTemplateService.PrepareSubject(email);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("from", this._emailSettings.From),
                    new KeyValuePair<string, string>("to", email.RecipientEmailAddress),
                    new KeyValuePair<string, string>("subject", email.Subject),
                    new KeyValuePair<string, string>("text", email.Message)
                });

                await client.PostAsync(this._emailSettings.RequestUri, content).ConfigureAwait(false);                
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
