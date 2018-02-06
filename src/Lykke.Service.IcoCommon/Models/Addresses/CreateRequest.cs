using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Lykke.Service.IcoCommon.Core.Domain;

namespace Lykke.Service.IcoCommon.Models.Addresses
{
    public class CreateRequest
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string CampaignId { get; set; }

        public CurrencyType CurrencyType { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
