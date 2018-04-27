﻿using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Campaign;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Domain.Transactions;
using Lykke.Service.IcoCommon.Core.Services;

namespace Lykke.Service.IcoCommon.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILog _log;
        private readonly IPayInAddressRepository _payInAddressRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICampaignSettingsRepository _campaignSettingsRepository;

        public TransactionService(
            ILog log,
            IPayInAddressRepository payInAddressRepository, 
            ITransactionRepository transactionRepository,
            ICampaignSettingsRepository campaignSettingsRepository)
        {
            _log = log;
            _payInAddressRepository = payInAddressRepository;
            _transactionRepository = transactionRepository;
            _campaignSettingsRepository = campaignSettingsRepository;
        }

        public async Task<int> HandleTransactionsAsync(ITransaction[] transactions)
        {
            await _log.WriteInfoAsync(nameof(HandleTransactionsAsync),
                $"Blocks: {transactions.GroupBy(t => new { t.BlockId, t.Currency }).Select(g => g.Key).ToJson()}",
                $"Transactions processing started");

            var count = 0;

            foreach (var tx in transactions)
            {
                var payInAddress = await _payInAddressRepository.GetAsync(tx.PayInAddress, tx.Currency);

                if (payInAddress == null)
                {
                    // destination address is not a pay-in address of any ICO investor
                    continue;
                }

                var campaignSettings = await _campaignSettingsRepository.GetCachedAsync(payInAddress.CampaignId, 
                    reloadIf: x => string.IsNullOrEmpty(x?.TransactionQueueSasUrl));

                await _transactionRepository.EnqueueTransactionAsync(tx, payInAddress, campaignSettings.TransactionQueueSasUrl);

                await _log.WriteInfoAsync(nameof(HandleTransactionsAsync),
                    $"Transaction: {tx.ToJson()}", 
                    $"Transaction sent to {payInAddress.CampaignId} queue");

                count++;
            }

            await _log.WriteInfoAsync(nameof(HandleTransactionsAsync),
                $"Investments: {count}",
                $"Transactions processing completed");

            return count;
        }
    }
}
