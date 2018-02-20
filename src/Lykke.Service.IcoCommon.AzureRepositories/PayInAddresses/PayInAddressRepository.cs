using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.SettingsReader;
using Microsoft.WindowsAzure.Storage.Table;
using static Lykke.Service.IcoCommon.AzureRepositories.PayInAddresses.PayInAddressEntity;

namespace Lykke.Service.IcoCommon.AzureRepositories.PayInAddresses
{
    public class PayInAddressRepository : IPayInAddressRepository
    {
        private readonly INoSQLTableStorage<PayInAddressEntity> _tableStorage;

        public PayInAddressRepository(IReloadingManager<string> connectionStringManager, ILog log)
        {
            _tableStorage = AzureTableStorage<PayInAddressEntity>.Create(connectionStringManager, "PayInAddresses", log);
        }

        public async Task InsertAsync(IPayInAddress payInAddress)
        {
            await _tableStorage.InsertAsync(new PayInAddressEntity(payInAddress));
        }

        public async Task<IPayInAddress> GetAsync(string address, CurrencyType currency)
        {
            var partitionKey = GetPartitionKey(address);
            var rowKey = GetRowKey(currency);

            return await _tableStorage.GetDataAsync(partitionKey, rowKey);
        }

        public async Task DeleteAsync(string address, CurrencyType currency)
        {
            var partitionKey = GetPartitionKey(address);
            var rowKey = GetRowKey(currency);

            await _tableStorage.DeleteIfExistAsync(partitionKey, rowKey);
        }

        public async Task DeleteAsync(string campaignId)
        {
            var query = new TableQuery<PayInAddressEntity>()
                .Where(TableQuery.GenerateFilterCondition(nameof(PayInAddressEntity.CampaignId), QueryComparisons.Equal, campaignId));

            var entities = new List<PayInAddressEntity>();

            await _tableStorage.ExecuteAsync(query, chunk => entities.AddRange(chunk));

            if (entities.Any())
            {
                await _tableStorage.DeleteAsync(entities);
            }
        }
    }
}
