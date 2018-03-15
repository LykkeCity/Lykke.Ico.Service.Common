using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailTemplateHistoryItemModel : EmailTemplateModel, IEmailTemplateHistoryItem
    {
        public EmailTemplateHistoryItemModel(IEmailTemplateHistoryItem emailTemplateHistoryItem)
        {
            CampaignId = emailTemplateHistoryItem.CampaignId;
            TemplateId = emailTemplateHistoryItem.TemplateId;
            Body = emailTemplateHistoryItem.Body;
            Subject = emailTemplateHistoryItem.Subject;
            IsLayout = emailTemplateHistoryItem.IsLayout;
            Username = emailTemplateHistoryItem.Username;
            ChangedUtc = emailTemplateHistoryItem.ChangedUtc;
        }

        public DateTime ChangedUtc { get; }
        public string Username { get; }
    }
}
