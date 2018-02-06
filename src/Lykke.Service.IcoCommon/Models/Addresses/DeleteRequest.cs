using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lykke.Service.IcoCommon.Models.Addresses
{
    public class DeleteRequest
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string CampaignId { get; set; }
    }
}
