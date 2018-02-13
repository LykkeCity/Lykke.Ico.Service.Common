using System;
using System.Collections.Generic;
using System.Text;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories.Mail
{
    [ValueTypeMergingStrategyAttribute(ValueTypeMergingStrategy.UpdateAlways)]
    public class EmailTemplateEntity : AzureTableEntity, IEmailTemplate
    {
        public static string GetPartitionKey(string campaignId) => campaignId;
        public static string GetRowKey(string templateId) => templateId;

        public EmailTemplateEntity()
        {
        }

        public EmailTemplateEntity(IEmailTemplate emailTemplate)
        {
            CampaignId = emailTemplate.CampaignId;
            TemplateId = emailTemplate.TemplateId;
            Body = emailTemplate.Body;
            Subject = emailTemplate.Subject;
        }

        [IgnoreProperty]
        public string CampaignId
        {
            get => PartitionKey;
            set => PartitionKey = GetPartitionKey(value);
        }

        [IgnoreProperty]
        public string TemplateId
        {
            get => RowKey;
            set => RowKey = GetRowKey(value);
        }

        public string Body
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }
    }
}
