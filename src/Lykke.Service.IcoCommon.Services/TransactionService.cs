using System;
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
        private IReloadingManager<QueueSettings> _queueSettings;

        public TransactionService(IPayInAddressRepository payInAddressRepository, IReloadingManager<QueueSettings> queueSettings)
        {
            _payInAddressRepository = payInAddressRepository;
            _queueSettings = queueSettings;
        }

        public async Task<int> HandleTransactions(ITransaction[] transactions)
        {
            var queueSettings = await _queueSettings.Reload();
            var count = 0;

            foreach (var tx in transactions)
            {
                var info = await _payInAddressRepository.GetAsync(tx.PayInAddress, tx.Currency);
                if (info == null)
                {
                    // destination address is not a pay-in address of any ICO investor
                    continue;
                }

                if (queueSettings.CampaignConnStrings.TryGetValue(info.CampaignId, out var connectionString))
                {
                    var reloadingManager = ConstantReloadingManager.From(connectionString);
                    var queue = AzureQueueExt.Create(reloadingManager, $"{info.CampaignId.ToLowerInvariant()}-transactions");

                    await queue.PutRawMessageAsync(new TransactionMessage(info.Email, tx).ToJson());

                    count++;
                }
                else
                {
                    throw new InvalidOperationException($"Configuration for \"{info.CampaignId}\" not found");
                }
            }

            return count;
        }

        public class TransactionMessage : ITransaction
        {
            private ITransaction _tx;

            public TransactionMessage(string email, ITransaction tx)
            {
                Email = email;
                _tx = tx;
            }
            
            public string Email { get; }
            public string UniqueId { get => _tx.UniqueId; }
            public string BlockId { get => _tx.BlockId; }
            public string TransactionId { get => _tx.TransactionId; }
            public string PayInAddress { get => _tx.PayInAddress; }
            public DateTime CreatedUtc { get => _tx.CreatedUtc; }
            public CurrencyType Currency { get => _tx.Currency; }
            public decimal Amount { get => _tx.Amount; }
        }
    }
}
