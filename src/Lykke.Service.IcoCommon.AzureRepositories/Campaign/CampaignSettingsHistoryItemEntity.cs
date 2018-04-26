using System;
using System.Globalization;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.Campaign;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories.Campaign
{
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateAlways)]
    public class CampaignSettingsHistoryItemEntity : AzureTableEntity, ICampaignSettingsHistoryItem
    {
        public CampaignSettingsHistoryItemEntity()
        {
        }

        public CampaignSettingsHistoryItemEntity(ICampaignSettings campaignSettings, string username)
        {
            TransactionQueueSasUrl = campaignSettings.TransactionQueueSasUrl;
            Smtp = campaignSettings.Smtp;
            Username = username;
        }

        [IgnoreProperty]
        public DateTime ChangedUtc
        {
            get => DateTime.ParseExact(RowKey, "O", CultureInfo.InvariantCulture);
        }

        [ValueSerializer(typeof(SmtpSettingsSerializer))]
        public SmtpSettings Smtp { get; set; }

        public string TransactionQueueSasUrl { get; set; }
        public string Username { get; set; }
    }
}
