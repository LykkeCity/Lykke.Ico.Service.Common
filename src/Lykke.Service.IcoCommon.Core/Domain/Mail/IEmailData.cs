using System.Collections.Generic;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmailData
    {
        string CampaignId { get; }
        string TemplateId { get; }
        string To { get; }
        string Subject { get; }
        object Data { get; }
        Dictionary<string, byte[]> Attachments { get; }
    }
}
