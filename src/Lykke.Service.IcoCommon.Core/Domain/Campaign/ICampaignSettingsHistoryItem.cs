using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Domain.Campaign
{
    public interface ICampaignSettingsHistoryItem : ICampaignSettings
    {
        DateTime ChangedUtc { get; }
        string Username { get; }
    }
}
