using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Domain.Transactions;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Lykke.SettingsReader;
using Common.Log;

namespace Lykke.Service.IcoCommon.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILog _log;
        private IPayInAddressRepository _payInAddressRepository;
        private ITransactionRepository _transactionRepository;
        private IReloadingManager<Dictionary<string, CampaignSettings>> _settings;

        public TransactionService(
            ILog log,
            IPayInAddressRepository payInAddressRepository, 
            ITransactionRepository transactionRepository,
            IReloadingManager<Dictionary<string, CampaignSettings>> settings)
        {
            _log = log;
            _payInAddressRepository = payInAddressRepository;
            _transactionRepository = transactionRepository;
            _settings = settings;
        }

        public async Task<int> HandleTransactionsAsync(ITransaction[] transactions)
        {
            var count = 0;

            foreach (var tx in transactions)
            {
                var payInAddress = await _payInAddressRepository.GetAsync(tx.PayInAddress, tx.Currency);
                if (payInAddress == null)
                {
                    // destination address is not a pay-in address of any ICO investor
                    continue;
                }

                if (!_settings.CurrentValue.TryGetValue(payInAddress.CampaignId, out var campaignSettings))
                {
                    await _log.WriteInfoAsync(nameof(TransactionService), nameof(HandleTransactionsAsync),
                        $"campaignId={payInAddress.CampaignId}", "Campaign settings not found. Reload whole settings");

                    await _settings.Reload();
                }

                if (!_settings.CurrentValue.TryGetValue(payInAddress.CampaignId, out campaignSettings))
                {
                    throw new InvalidOperationException(
                        $"Configuration for campaign \"{payInAddress.CampaignId}\" not found");
                }

                await _transactionRepository.EnqueueTransactionAsync(tx, payInAddress, campaignSettings.ConnectionString);

                count++;
            }

            return count;
        }
    }
}
