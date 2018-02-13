using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmail
    {
        DateTime SentUtc { get; }
        string To { get; }
        string Subject { get; }
        string Body { get; }
        string CampaignId { get; }
        string TemplateId { get; }
        Dictionary<string, byte[]> Attachments { get; set; }
    }
}
