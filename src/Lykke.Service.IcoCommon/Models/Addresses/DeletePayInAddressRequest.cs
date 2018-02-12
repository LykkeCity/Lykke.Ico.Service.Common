using System.ComponentModel.DataAnnotations;
using Lykke.Service.IcoCommon.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lykke.Service.IcoCommon.Models.Addresses
{
    [BindRequired]
    public class DeletePayInAddressRequest
    {
        [Required]
        public string Address { get; set; }

        public CurrencyType Currency { get; set; }
    }
}
