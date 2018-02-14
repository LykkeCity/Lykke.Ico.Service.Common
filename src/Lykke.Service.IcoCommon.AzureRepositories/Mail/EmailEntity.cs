using System;
using System.Collections.Generic;
using System.Globalization;
using Common;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.Serializers;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Lykke.Service.IcoCommon.AzureRepositories.Mail
{
    [ValueTypeMergingStrategyAttribute(ValueTypeMergingStrategy.UpdateAlways)]
    public class EmailEntity : AzureTableEntity, IEmail
    {
        public static string GetPartitionKey(string to) => to;
        public static string GetRowKey(DateTime sentUtc) => sentUtc.ToString("O");

        public EmailEntity()
        {
        }

        public EmailEntity(IEmail email)
        {
            SentUtc = email.SentUtc;
            To = email.To;
            Subject = email.Subject;
            Body = email.Body;
            CampaignId = email.CampaignId;
            TemplateId = email.TemplateId;
            Attachments = email.Attachments;
        }

        [IgnoreProperty]
        public string To
        {
            get => PartitionKey;
            set => PartitionKey = GetPartitionKey(value);
        }

        [IgnoreProperty]
        public DateTime SentUtc
        {
            get => DateTime.ParseExact(RowKey, "O", CultureInfo.InvariantCulture);
            set => RowKey = GetRowKey(value);
        }
        public string CampaignId { get; set; }
        public string TemplateId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        [ValueSerializer(typeof(AttachmentsSerializer))]
        public Dictionary<string, byte[]> Attachments { get; set; }

        public class AttachmentsSerializer : IStorageValueSerializer
        {
            public object Deserialize(string serialized) => JsonConvert.DeserializeObject<Dictionary<string, byte[]>>(serialized);
            public string Serialize(object value) => JsonConvert.SerializeObject(value);
        }
    }
}
