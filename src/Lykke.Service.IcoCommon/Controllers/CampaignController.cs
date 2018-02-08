using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.IcoCommon.Controllers
{
    [Route("/api/campaign")]
    public class CampaignController : Controller
    {
        [HttpDelete("{campaignId}")]
        [SwaggerOperation(nameof(DeleteCampaign))]
        public async Task<IActionResult> DeleteCampaign(string campaignId)
        {
            return null;
        }
    }
}
