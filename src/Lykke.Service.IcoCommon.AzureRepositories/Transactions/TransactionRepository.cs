using System;
using System.Threading.Tasks;
using Common;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Domain.Transactions;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Lykke.Service.IcoCommon.AzureRepositories.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task EnqueueTransactionAsync(ITransaction tx, IPayInAddress payInAddress, string queueSas)
        {
            var message = tx
                .AsQueueMessage(payInAddress.Email)
                .ToJson();

            await new CloudQueue(new Uri(queueSas))
                .AddMessageAsync(new CloudQueueMessage(message));
        }
    }
}
