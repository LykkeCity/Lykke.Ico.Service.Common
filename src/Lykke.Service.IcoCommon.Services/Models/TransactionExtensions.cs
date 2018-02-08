using Lykke.Service.IcoCommon.Client.Models;

namespace Lykke.Service.IcoCommon.Core.Domain.Transactions
{
    public static class TransactionExtensions
    {
        public static TransactionQueueMessageModel AsQueueMessage(this ITransaction self, string email)
        {
            return new TransactionQueueMessageModel
            {
                Amount = self.Amount,
                BlockId = self.BlockId,
                CreatedUtc = self.CreatedUtc,
                Currency = self.Currency,
                Email = email,
                PayInAddress = self.PayInAddress,
                TransactionId = self.TransactionId,
                UniqueId = self.UniqueId
            };
        }
    }
}
