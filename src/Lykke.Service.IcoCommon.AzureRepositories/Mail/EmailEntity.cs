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
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateAlways)]
    public class EmailEntity : AzureTableEntity, IEmail
    {
        public static string GetPartitionKey(string to) => to;
        public static string GetRowKey(DateTime sentUtc) => sentUtc.ToString("O");

        // See: https://blogs.msdn.microsoft.com/avkashchauhan/2011/11/30/how-the-size-of-an-entity-is-caclulated-in-windows-azure-table-storage/
        // String – # of Characters * 2 bytes + 4 bytes for length of string
        // Max coumn size is 64 Kb, so max string len is (65536 - 4) / 2 = 32766
        public const int MaxStringLength = 32766;

        public static string Truncate(string str)
        {
            if (str == null)
            {
                return null;
            }

            // 3 - is for "..."
            const int maxLength = MaxStringLength - 3;

            if (str.Length > maxLength)
            {
                return string.Concat(str.Substring(0, maxLength), "...");
            }

            return str;
        }

        public EmailEntity()
        {
        }

        public EmailEntity(IEmail email)
        {
            SentUtc = email.SentUtc;
            To = email.To;
            Subject = email.Subject;
            Body = Truncate(email.Body);
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
            public const string StubMessage = "too_long_to_save";

            public object Deserialize(string serialized)
            {
                return Deserialize(serialized, typeof(Dictionary<string, byte[]>));
            }

            public object Deserialize(string serialized, Type type)
            {
                if (serialized != StubMessage)
                {
                    return JsonConvert.DeserializeObject(serialized, type);
                }
                else
                {
                    return null;
                }
            }

            public string Serialize(object value)
            {
                return Serialize(value, typeof(Dictionary<string, byte[]>));
            }

            public string Serialize(object value, Type type)
            {
                var json = JsonConvert.SerializeObject(value, type, null);
                if (json.Length <= MaxStringLength)
                {
                    return json;
                }
                else
                {
                    // don't save attachments at all to prevent errors on deserializing
                    return StubMessage;
                }
            }
        }
    }
}
