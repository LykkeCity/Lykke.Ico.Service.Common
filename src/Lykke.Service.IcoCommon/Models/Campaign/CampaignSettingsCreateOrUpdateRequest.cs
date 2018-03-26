using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Models.Campaign
{
    public class CampaignSettingsCreateOrUpdateRequest
    {
        [Required]
        public CampaignSettingsModel CampaignSettings { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
