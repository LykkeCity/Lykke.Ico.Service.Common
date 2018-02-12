using System;

namespace Lykke.Service.IcoCommon.Core.Domain.Transactions
{
    public interface ITransaction
    {
        string UniqueId { get; }
        string BlockId { get; }
        string TransactionId { get; }
        string PayInAddress { get; }
        DateTime CreatedUtc { get; }
        CurrencyType Currency { get; }
        decimal Amount { get; }
    }
}
