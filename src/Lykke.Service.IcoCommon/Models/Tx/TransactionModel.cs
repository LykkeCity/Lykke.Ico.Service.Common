using System;
using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core;
using Lykke.Service.IcoCommon.Core.Domain.Transactions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lykke.Service.IcoCommon.Models.Tx
{
    [BindRequired]
    public class TransactionModel : ITransaction
    {
        [Required]
        public string BlockId { get; set; }

        public DateTime CreatedUtc { get; set; }

        [Required]
        public string UniqueId { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public string PayInAddress { get; set; }

        public CurrencyType Currency { get; set; }

        [Range(0, Double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
