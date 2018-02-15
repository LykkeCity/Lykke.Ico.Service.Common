using System.Threading.Tasks;
using AzureStorage.Queue;
using Common;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Domain.Transactions;
using Lykke.SettingsReader.ReloadingManager;
using Common.Log;

namespace Lykke.Service.IcoCommon.AzureRepositories.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ILog _log;

        public TransactionRepository(ILog log)
        {
            _log = log;
        }

        public async Task EnqueueTransactionAsync(ITransaction tx, IPayInAddress payInAddress, string connectionString)
        {
            var queueName = $"{payInAddress.CampaignId.ToLowerInvariant()}-transaction";
            var queue = AzureQueueExt.Create(ConstantReloadingManager.From(connectionString), queueName);

            var message = tx
                .AsQueueMessage(payInAddress.Email)
                .ToJson();

            await _log.WriteInfoAsync(nameof(TransactionRepository), nameof(EnqueueTransactionAsync),
                $"queue={queueName}, message={message}", "Send transaction message to queue");

            await queue.PutRawMessageAsync(message);
        }
    }
}
