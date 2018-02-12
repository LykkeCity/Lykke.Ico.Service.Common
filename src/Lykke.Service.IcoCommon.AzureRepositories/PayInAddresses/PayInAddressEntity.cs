using System;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories.PayInAddresses
{
    [ValueTypeMergingStrategyAttribute(ValueTypeMergingStrategy.UpdateAlways)]
    public class PayInAddressEntity : AzureTableEntity, IPayInAddressInfo
    {
        [IgnoreProperty]
        public CurrencyType Currency { get => Enum.Parse<CurrencyType>(RowKey); }

        [IgnoreProperty]
        public string Address { get => PartitionKey; }

        public string CampaignId { get; set; }
        public string Email { get; set; }
    }
}
