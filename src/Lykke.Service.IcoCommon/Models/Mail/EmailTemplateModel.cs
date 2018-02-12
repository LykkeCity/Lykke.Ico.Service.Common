using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailTemplateModel : IEmailTemplate
    {
        [Required]
        public string CampaignId { get; set; }

        [Required]
        public string TemplateId { get; set; }

        [Required]
        public string Body { get; set; }

        public string Subject { get; set; }
    }
}
