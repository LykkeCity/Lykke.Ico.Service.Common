using System;

namespace Lykke.Service.IcoCommon.Client.Models
{
    public class TransactionQueueMessageModel
    {
        public string Email { get; set; }
        public string UniqueId { get; set; }
        public string BlockId { get; set; }
        public string TransactionId { get; set; }
        public string PayInAddress { get; set; }
        public DateTime CreatedUtc { get; set; }
        public CurrencyType Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
