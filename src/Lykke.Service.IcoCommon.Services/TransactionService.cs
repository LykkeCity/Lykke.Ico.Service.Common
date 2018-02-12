using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureStorage.Queue;
using Common;
using Lykke.Service.IcoCommon.Core;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Domain.Transactions;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Lykke.SettingsReader;
using Lykke.SettingsReader.ReloadingManager;

namespace Lykke.Service.IcoCommon.Services
{
    public class TransactionService : ITransactionService
    {
        private IPayInAddressRepository _payInAddressRepository;
        private IReloadingManager<Dictionary<string, CampaignSettings>> _settings;

        public TransactionService(
            IPayInAddressRepository payInAddressRepository, 
            IReloadingManager<Dictionary<string, CampaignSettings>> settings)
        {
            _payInAddressRepository = payInAddressRepository;
            _settings = settings;
        }

        public async Task<int> HandleTransactions(ITransaction[] transactions)
        {
            var count = 0;

            foreach (var tx in transactions)
            {
                var info = await _payInAddressRepository.GetAsync(tx.PayInAddress, tx.Currency);
                if (info == null)
                {
                    // destination address is not a pay-in address of any ICO investor
                    continue;
                }

                CampaignSettings campaignSettings = null;

                if (!_settings.CurrentValue.TryGetValue(info.CampaignId, out campaignSettings))
                {
                    await _settings.Reload();
                }

                if (!_settings.CurrentValue.TryGetValue(info.CampaignId, out campaignSettings))
                {
                    throw new InvalidOperationException($"Configuration for campaign \"{info.CampaignId}\" not found");
                }

                var queue = AzureQueueExt.Create(ConstantReloadingManager.From(campaignSettings.ConnectionString),
                    $"{info.CampaignId.ToLowerInvariant()}-transaction");

                var rawMessage = tx
                    .AsQueueMessage(info.Email)
                    .ToJson();

                await queue.PutRawMessageAsync(rawMessage);

                count++;
            }

            return count;
        }
    }
}
