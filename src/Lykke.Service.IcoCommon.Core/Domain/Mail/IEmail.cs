using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmail
    {
        string EmailTo { get; }
        string CampaignId { get; }
        string TemplateId { get; }
        object Params { get; }
    }
}
