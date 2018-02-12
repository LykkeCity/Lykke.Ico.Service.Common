using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Domain.PayInAddresses
{
    public interface IPayInAddressInfo
    {
        CurrencyType Currency { get; }
        string Address { get; }
        string CampaignId { get; }
        string Email { get; }
    }
}
