using System;
using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailTemplateModel : IEmailTemplate
    {
        public static EmailTemplateModel Create(IEmailTemplate emailTemplate)
        {
            if (emailTemplate == null)
            {
                return null;
            }

            return new EmailTemplateModel
            {
                CampaignId = emailTemplate.CampaignId,
                TemplateId = emailTemplate.TemplateId,
                Body = emailTemplate.Body,
                Subject = emailTemplate.Subject,
                IsLayout = emailTemplate.IsLayout
            };
        }

        [Required]
        public string CampaignId { get; set; }

        [Required]
        public string TemplateId { get; set; }

        [Required]
        public string Body { get; set; }

        public string Subject { get; set; }

        public bool IsLayout { get; set; }
    }
}
