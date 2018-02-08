using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories
{
    [ValueTypeMergingStrategyAttribute(ValueTypeMergingStrategy.UpdateAlways)]
    public class PayInAddressEntity : AzureTableEntity, IPayInAddressInfo
    {
        public string CampaignId { get; set; }
        public string Email { get; set; }
    }
}
