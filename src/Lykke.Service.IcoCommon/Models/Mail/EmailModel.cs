using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailModel : IEmail
    {
        public static EmailModel Create(IEmail email)
        {
            if (email == null)
            {
                return null;
            }

            return new EmailModel
            {
                CampaignId = email.CampaignId,
                TemplateId = email.TemplateId,
                To = email.To,
                Subject = email.Subject,
                Body = email.Body,
                Attachments = email.Attachments,
                SentUtc = email.SentUtc
            };
        }

        public string CampaignId { get; set; }
        public string TemplateId { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Dictionary<string, byte[]> Attachments { get; set; }
        public DateTime SentUtc { get; set; }
    }
}
