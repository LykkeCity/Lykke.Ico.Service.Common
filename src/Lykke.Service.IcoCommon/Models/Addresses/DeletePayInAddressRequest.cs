﻿using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core;

namespace Lykke.Service.IcoCommon.Models.Addresses
{
    public class DeletePayInAddressRequest
    {
        [Required]
        public string Address { get; set; }

        public CurrencyType Currency { get; set; }
    }
}