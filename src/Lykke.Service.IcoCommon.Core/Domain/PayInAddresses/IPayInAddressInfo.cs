using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.IcoCommon.Core.Domain.PayInAddresses
{
    public interface IPayInAddressInfo
    {
        string CampaignId { get; }
        string Email { get; }
    }
}
