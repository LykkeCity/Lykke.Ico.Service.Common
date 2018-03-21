using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Campaign;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;

namespace Lykke.Service.IcoCommon.Models.Campaign
{
    public class CampaignSettingsModel : ICampaignSettings
    {
        public static CampaignSettingsModel Create(ICampaignSettings settings)
        {
            if (settings == null)
            {
                return null;
            }

            return new CampaignSettingsModel
            {
                TransactionQueueSasUrl = settings.TransactionQueueSasUrl,
                Smtp = settings.Smtp
            };
        }

        public string TransactionQueueSasUrl { get; set; }
        public SmtpSettings Smtp { get; set; }
    }
}
