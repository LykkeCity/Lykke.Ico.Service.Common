using System;
using Common;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.IcoCommon.AzureRepositories.PayInAddresses
{
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateAlways)]
    public class PayInAddressEntity : AzureTableEntity, IPayInAddress
    {
        public static string GetPartitionKey(string address) => address;
        public static string GetRowKey(CurrencyType currencyType) => Enum.GetName(typeof(CurrencyType), currencyType);

        public PayInAddressEntity()
        {
        }

        public PayInAddressEntity(IPayInAddress payInAddress)
        {
            Address = payInAddress.Address;
            Currency = payInAddress.Currency;
            CampaignId = payInAddress.CampaignId;
            Email = payInAddress.Email;
        }

        [IgnoreProperty]
        public string Address
        {
            get => PartitionKey;
            set => PartitionKey = GetPartitionKey(value);
        }

        [IgnoreProperty]
        public CurrencyType Currency
        {
            get => RowKey.ParseEnum<CurrencyType>();
            set => RowKey = GetRowKey(value);
        }

        public string CampaignId
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
    }
}
