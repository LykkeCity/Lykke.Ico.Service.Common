using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Settings.ServiceSettings
{
    public class QueueSettings
    {
        public string MailConnString { get; set; }
        public Dictionary<string, string> CampaignConnStrings { get; set; }
    }
}
