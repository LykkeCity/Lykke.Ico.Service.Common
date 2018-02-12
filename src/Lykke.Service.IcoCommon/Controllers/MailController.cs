using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.ApiLibrary.Contract;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.IcoCommon.Controllers
{
    [Route("/api/mail")]
    public class MailController : Controller
    {
        private readonly IEmailService _emailService;

        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [SwaggerOperation(nameof(Send))]
        public async Task<IActionResult> Send([FromBody]EmailModel email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _emailService.EnqueueEmail(email);

            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(nameof(GetSent))]
        public async Task<IActionResult> GetSent()
        {
            return null;
        }

        [HttpPost("templates")]
        [SwaggerOperation(nameof(AddOrUpdateTemplate))]
        public async Task<IActionResult> AddOrUpdateTemplate([FromBody]EmailTemplateModel emailTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _emailService.SaveEmailTemplate(emailTemplate);

            return Ok();
        }

        [HttpGet("templates/{campaignId}/{templateId}")]
        [SwaggerOperation(nameof(GetTemplate))]
        public async Task<IActionResult> GetTemplate(
            [FromRoute]string campaignId,
            [FromRoute]string templateId)
        {
            return null;
        }

        [HttpGet("templates/{campaignId}")]
        [SwaggerOperation(nameof(GetCampaignTemplates))]
        public async Task<IActionResult> GetCampaignTemplates(
            [FromRoute]string campaignId)
        {
            return null;
        }
    }
}
