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
        [IgnoreProperty]
        public string CampaignId { get => PartitionKey; }

        [IgnoreProperty]
        public string TemplateId { get => RowKey; }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
