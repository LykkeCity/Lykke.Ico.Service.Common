using System;
using System.Collections.Generic;
using System.Text;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.IcoCommon.Core.Settings.ServiceSettings
{
    public class CampaignSettings
    {
        [AzureQueueCheck(false)]
        public string ConnectionString { get; set; }

        [Optional]
        public SmtpSettings Smtp { get; set; }
    }
}
