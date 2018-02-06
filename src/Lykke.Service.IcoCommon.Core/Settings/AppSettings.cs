using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Lykke.Service.IcoCommon.Core.Settings.SlackNotifications;

namespace Lykke.Service.IcoCommon.Core.Settings
{
    public class AppSettings
    {
        public IcoCommonSettings IcoCommonService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
