using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.Campaign;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;

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
            EmailBlackList = campaignSettings.EmailBlackList;
            Smtp = campaignSettings.Smtp;
        }

        public string TransactionQueueSasUrl { get; set; }
        public string EmailBlackList { get; set; }

        [ValueSerializer(typeof(SmtpSettingsSerializer))]
        public SmtpSettings Smtp { get; set; }
    }
}
