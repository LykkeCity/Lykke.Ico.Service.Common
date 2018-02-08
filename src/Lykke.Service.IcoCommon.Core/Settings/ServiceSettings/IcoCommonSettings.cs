using System.Collections.Generic;

namespace Lykke.Service.IcoCommon.Core.Settings.ServiceSettings
{
    public class IcoCommonSettings
    {
        public DbSettings Db { get; set; }
        public Dictionary<string, CampaignSettings> Campaigns { get; set; }
    }
}
