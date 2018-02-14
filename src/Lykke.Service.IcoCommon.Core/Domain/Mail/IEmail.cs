using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmail
    {
        string CampaignId { get; }
        string TemplateId { get; }
        string To { get; }
        string Subject { get; }
        string Body { get; }
        Dictionary<string, byte[]> Attachments { get; }
        DateTime SentUtc { get; }
    }
}
