using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core;

namespace Lykke.Service.IcoCommon.Models.Addresses
{
    public class CreateRequest
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
