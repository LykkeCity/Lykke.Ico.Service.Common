using System;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.IcoCommon.Core;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.SettingsReader;

namespace Lykke.Service.IcoCommon.AzureRepositories.PayInAddresses
{
    public class PayInAddressRepository : IPayInAddressRepository
    {
        private readonly INoSQLTableStorage<PayInAddressEntity> _tableStorage;
        private static string GetPartitionKey(string address) => address;
        private static string GetRowKey(CurrencyType currencyType) => Enum.GetName(typeof(CurrencyType), currencyType);

        public PayInAddressRepository(IReloadingManager<string> connectionStringManager, ILog log)
        {
            _tableStorage = AzureTableStorage<PayInAddressEntity>.Create(connectionStringManager, "PayInAddresses", log);
        }

        public async Task UpsertAsync(string address, CurrencyType currency, string campaignId, string email)
        {
            var partitionKey = GetPartitionKey(address);
            var rowKey = GetRowKey(currency);

            await _tableStorage.InsertOrReplaceAsync(new PayInAddressEntity
            {
                PartitionKey = partitionKey,
                RowKey = rowKey,
                CampaignId = campaignId,
                Email = email
            });
        }

        public async Task DeleteAsync(string address, CurrencyType currency)
        {
            var partitionKey = GetPartitionKey(address);
            var rowKey = GetRowKey(currency);

            await _tableStorage.DeleteIfExistAsync(partitionKey, rowKey);
        }

        public async Task<IPayInAddressInfo> GetAsync(string address, CurrencyType currency)
        {
            var partitionKey = GetPartitionKey(address);
            var rowKey = GetRowKey(currency);

            return await _tableStorage.GetDataAsync(partitionKey, rowKey);
        }
    }
}
