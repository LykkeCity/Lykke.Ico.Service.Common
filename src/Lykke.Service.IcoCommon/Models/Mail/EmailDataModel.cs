using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailDataModel : IEmailData
    {
        [Required]
        public string CampaignId { get; set; }

        [Required]
        public string TemplateId { get; set; }

        [Required]
        public string To { get; set; }

        public string Subject { get; set; }

        public object Data { get; set; }

        public Dictionary<string, byte[]> Attachments { get; set; }
    }
}
