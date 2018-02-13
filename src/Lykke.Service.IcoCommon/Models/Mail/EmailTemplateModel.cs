using System;
using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailTemplateModel : IEmailTemplate
    {
        public EmailTemplateModel()
        {
        }

        public EmailTemplateModel(IEmailTemplate emailTemplate)
        {
            CampaignId = emailTemplate.CampaignId;
            TemplateId = emailTemplate.TemplateId;
            Body = emailTemplate.Body;
            Subject = emailTemplate.Subject;
        }

        [Required]
        public string CampaignId { get; set; }

        [Required]
        public string TemplateId { get; set; }

        [Required]
        public string Body { get; set; }

        public string Subject { get; set; }
    }
}
