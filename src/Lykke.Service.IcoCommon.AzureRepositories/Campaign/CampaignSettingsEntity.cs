using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.Campaign;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories.Campaign
{
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateAlways)]
    public class CampaignSettingsEntity : AzureTableEntity, ICampaignSettings
    {
        public CampaignSettingsEntity()
        {
        }

        public CampaignSettingsEntity(ICampaignSettings campaignSettings)
        {
            TransactionQueueSasUrl = campaignSettings.TransactionQueueSasUrl;
            Smtp = campaignSettings.Smtp;
        }

        public string TransactionQueueSasUrl { get; set; }

        [JsonValueSerializer]
        public SmtpSettings Smtp { get; set; }
    }
}
