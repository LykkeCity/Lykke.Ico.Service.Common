using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Lykke.Service.IcoCommon.Core.Domain;

namespace Lykke.Service.IcoCommon.Models.Tx
{
    public class HandleTransactionsRequest
    {
        public DateTimeOffset BlockTimestamp { get; set; }
        public string BlockId { get; set; }
        public Transaction[] Transactions { get; set; }

        public class Transaction
        {
            [Required]
            public string TransactionId { get; set; }

            [Required]
            public string Address { get; set; }

            public CurrencyType CurrencyType { get; set; }

            [Range(0, Double.MaxValue)]
            public decimal Amount { get; set; }
        }
    }
}
