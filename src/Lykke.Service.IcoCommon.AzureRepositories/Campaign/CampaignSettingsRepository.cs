using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Campaign;
using Lykke.SettingsReader;

namespace Lykke.Service.IcoCommon.AzureRepositories.Campaign
{
    public class CampaignSettingsRepository : ICampaignSettingsRepository
    {
        private readonly INoSQLTableStorage<CampaignSettingsEntity> _table;
        private readonly ConcurrentDictionary<string, CampaignSettingsEntity> _cache = new ConcurrentDictionary<string, CampaignSettingsEntity>();

        private static string GetPartitionKey(string campaignId) => campaignId;
        private static string GetRowKey() => string.Empty;

        public CampaignSettingsRepository(IReloadingManager<string> connectionStringManager, ILog log)
        {
            _table = AzureTableStorage<CampaignSettingsEntity>.Create(connectionStringManager, "Campaigns", log);
        }

        public async Task<ICampaignSettings> GetCachedAsync(string campaignId, Func<ICampaignSettings, bool> reloadIf = null, bool doubleCheck = false)
        {
            if (!_cache.TryGetValue(campaignId, out var value) || (reloadIf != null && reloadIf(value)))
            {
                var partitionKey = GetPartitionKey(campaignId);
                var rowKey = GetRowKey();

                value = await _table.GetDataAsync(partitionKey, rowKey);

                // return value may be null, so double-check condition must treat nulls properly,
                // otherwise (if we make double-check on not-null value only) it may lead to NRE in calling code
                if (doubleCheck && reloadIf != null && reloadIf(value))
                {
                    throw new InvalidOperationException($"Incomplete campaign settings for {campaignId}");
                }

                if (value != null)
                {
                    _cache[campaignId] = value;
                }
            }

            return value;
        }

        public async Task UpsertAsync(string campaignId, ICampaignSettings campaignSettings)
        {
            var partitionKey = GetPartitionKey(campaignId);
            var rowKey = GetRowKey();
            var entity = new CampaignSettingsEntity(campaignSettings) { PartitionKey = partitionKey, RowKey = rowKey };

            await _table.InsertOrReplaceAsync(entity);

            _cache[campaignId] = entity;
        }

        public async Task DeleteAsync(string campaignId)
        {
            var partitionKey = GetPartitionKey(campaignId);
            var rowKey = GetRowKey();

            await _table.DeleteIfExistAsync(partitionKey, rowKey);
        }
    }
}
