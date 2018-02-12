using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailModel : IEmail
    {
        [Required]
        public string EmailTo { get; set; }

        [Required]
        public string CampaignId { get; set; }

        [Required]
        public string TemplateId { get; set; }

        public object Params { get; set; }
    }
}
