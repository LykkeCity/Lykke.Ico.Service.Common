﻿using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lykke.Service.IcoCommon.Models.Addresses
{
    [BindRequired]
    public class PayInAddressModel : IPayInAddress
    {
        [Required]
        public string Address { get; set; }

        public CurrencyType Currency { get; set; }

        [Required]
        public string CampaignId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
