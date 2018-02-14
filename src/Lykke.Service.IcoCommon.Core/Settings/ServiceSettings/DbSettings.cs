using System.Collections.Generic;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.IcoCommon.Core.Settings.ServiceSettings
{
    public class DbSettings
    {
        [AzureTableCheck(false)]
        public string LogsConnString { get; set; }

        [AzureTableCheck(false)]
        public string DataConnString { get; set; }
    }
}
