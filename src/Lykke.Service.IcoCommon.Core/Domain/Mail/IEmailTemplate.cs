using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmailTemplate
    {
        string CampaignId { get; }
        string TemplateId { get; }
        string Subject { get; }
        string Body { get; }
    }
}
