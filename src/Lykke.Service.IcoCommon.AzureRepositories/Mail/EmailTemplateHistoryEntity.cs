using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories.Mail
{
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateAlways)]
    public class EmailTemplateHistoryEntity : AzureTableEntity, IEmailTemplate
    {
        public EmailTemplateHistoryEntity()
        {
        }

        public EmailTemplateHistoryEntity(IEmailTemplate emailTemplate)
        {
            CampaignId = emailTemplate.CampaignId;
            TemplateId = emailTemplate.TemplateId;
            Body = emailTemplate.Body;
            Subject = emailTemplate.Subject;
        }

        public string CampaignId { get; set; }
        public string TemplateId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
