using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.IcoCommon.Core.Settings.ServiceSettings
{
    public class CampaignSettings
    {
        public string TransactionQueueSasUrl { get; set; }

        [Optional]
        public SmtpSettings Smtp { get; set; }
    }
}
