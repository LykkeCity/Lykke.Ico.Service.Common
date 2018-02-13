using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailModel : IEmail
    {
        public EmailModel()
        {
        }

        public EmailModel(IEmail email)
        {
            SentUtc = email.SentUtc;
            To = email.To;
            Subject = email.Subject;
            Body = email.Body;
            CampaignId = email.CampaignId;
            TemplateId = email.TemplateId;
            Attachments = email.Attachments;
        }

        public DateTime SentUtc { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CampaignId { get; set; }
        public string TemplateId { get; set; }
        public Dictionary<string, byte[]> Attachments { get; set; }
    }
}
