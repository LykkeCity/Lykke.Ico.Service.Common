using System.Threading.Tasks;
using AzureStorage.Queue;
using Common;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Domain.Transactions;
using Lykke.SettingsReader.ReloadingManager;

namespace Lykke.Service.IcoCommon.AzureRepositories.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task EnqueueTransactionAsync(ITransaction tx, IPayInAddress payInAddress, string connectionString)
        {
            var queue = AzureQueueExt.Create(ConstantReloadingManager.From(connectionString),
                $"{payInAddress.CampaignId.ToLowerInvariant()}-transaction");

            var queueMessage = tx
                .AsQueueMessage(payInAddress.CampaignId)
                .ToJson();

            await queue.PutRawMessageAsync(queueMessage);
        }
    }
}
