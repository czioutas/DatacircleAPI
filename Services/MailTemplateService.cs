using DatacircleAPI.Settings;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace DatacircleAPI.Services
{
    public class MailTemplateService
    { 
        private readonly IOptions<MailTemplateSettings> _mailTemplateSettings;
        public MailTemplateService(IOptions<MailTemplateSettings> mailTemplateSettings)
        {
            this._mailTemplateSettings = mailTemplateSettings;
        }

        public string PrepareContent(Email email)
        {
            string EmailTemplate = email.Template;
            
            BaseMailTemplate bst = (BaseMailTemplate) this._mailTemplateSettings.Value.GetTemplateByName(EmailTemplate);
            
            return bst.Content;
        }
    }
}
