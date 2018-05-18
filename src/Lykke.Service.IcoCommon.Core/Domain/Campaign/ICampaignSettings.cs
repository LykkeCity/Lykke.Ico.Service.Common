using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;

namespace Lykke.Service.IcoCommon.Core.Domain.Campaign
{
    public interface ICampaignSettings
    {
        string TransactionQueueSasUrl { get; }

        string EmailBlackList { get; }

        SmtpSettings Smtp { get; }
    }
}
