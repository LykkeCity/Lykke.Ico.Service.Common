using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;

namespace Lykke.Service.IcoCommon.Models.Addresses
{
    public class PayInAddressModel : IPayInAddressInfo
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
