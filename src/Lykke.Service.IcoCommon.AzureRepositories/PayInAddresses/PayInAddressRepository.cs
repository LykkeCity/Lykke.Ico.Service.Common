using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.SettingsReader;
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

        public async Task UpsertAsync(IPayInAddress payInAddress)
        {
            await _tableStorage.InsertOrReplaceAsync(new PayInAddressEntity(payInAddress));
        }

        public async Task DeleteAsync(string address, CurrencyType currency)
        {
            var partitionKey = GetPartitionKey(address);
            var rowKey = GetRowKey(currency);

            await _tableStorage.DeleteIfExistAsync(partitionKey, rowKey);
        }

        public async Task<IPayInAddress> GetAsync(string address, CurrencyType currency)
        {
            var partitionKey = GetPartitionKey(address);
            var rowKey = GetRowKey(currency);

            return await _tableStorage.GetDataAsync(partitionKey, rowKey);
        }
    }
}
