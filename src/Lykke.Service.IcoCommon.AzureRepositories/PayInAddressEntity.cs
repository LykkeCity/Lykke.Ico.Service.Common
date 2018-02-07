using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories
{
    public class PayInAddressEntity : TableEntity, IPayInAddressInfo
    {
        public string CampaignId { get; set; }
        public string Email { get; set; }
    }
}
