using System;
using System.Globalization;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories.Mail
{
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateAlways)]
    public class EmailTemplateHistoryItemEntity : AzureTableEntity, IEmailTemplateHistoryItem
    {
        public EmailTemplateHistoryItemEntity()
        {
        }

        public EmailTemplateHistoryItemEntity(IEmailTemplate emailTemplate, string username)
        {
            CampaignId = emailTemplate.CampaignId;
            TemplateId = emailTemplate.TemplateId;
            Body = emailTemplate.Body;
            Subject = emailTemplate.Subject;
            IsLayout = emailTemplate.IsLayout;
            Username = username;
        }

        [IgnoreProperty]
        public DateTime ChangedUtc
        {
            get => DateTime.ParseExact(RowKey, "O", CultureInfo.InvariantCulture);
        }

        public string CampaignId { get; set; }
        public string TemplateId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsLayout { get; set; }
        public string Username { get; set; }
    }
}
