using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.IcoCommon.Controllers
{
    [Route("/api/emails")]
    public class EmailsController : Controller
    {
        [HttpPost]
        [SwaggerOperation(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail()
        {
            return null;
        }

        [HttpGet]
        [SwaggerOperation(nameof(GetSentEmails))]
        public async Task<IActionResult> GetSentEmails()
        {
            return null;
        }

        [HttpPost("templates")]
        [SwaggerOperation(nameof(AddEmailTemplate))]
        public async Task<IActionResult> AddEmailTemplate()
        {
            return null;
        }

        [HttpGet("templates/{campaignId}")]
        [SwaggerOperation(nameof(GetCampaignTemplates))]
        public async Task<IActionResult> GetCampaignTemplates(string campaignId)
        {
            return null;
        }

        [HttpGet("templates/{campaignId}/{templateId}")]
        [SwaggerOperation(nameof(GetTemplate))]
        public async Task<IActionResult> GetTemplate(string campaignId, string templateId)
        {
            return null;
        }
    }
}
