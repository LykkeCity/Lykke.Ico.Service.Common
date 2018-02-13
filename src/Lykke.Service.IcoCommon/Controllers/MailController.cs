using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureStorage.Queue;
using Common;
using Common.Log;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.ApiLibrary.Contract;
using Lykke.JobTriggers.Triggers.Attributes;
using Lykke.JobTriggers.Triggers.Bindings;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.IcoCommon.Controllers
{
    [Route("/api/mail")]
    public class MailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;

        public MailController(IEmailService emailService, IEmailTemplateService emailTemplateService)
        {
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
        }

        [HttpPost]
        [SwaggerOperation(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail([FromBody]EmailDataModel emailData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _emailService.PushEmailToQueueAsync(emailData);

            return Ok();
        }

        [HttpGet("{to}")]
        [SwaggerOperation(nameof(GetSentEmails))]
        public async Task<EmailModel[]> GetSentEmails(
            [FromRoute]string to,
            [FromQuery]string campaignId)
        {
            return (await _emailService.GetSentEmailsAsync(to, campaignId))
                .Select(email => new EmailModel(email))
                .ToArray();
        }

        [HttpPost("templates")]
        [SwaggerOperation(nameof(AddOrUpdateTemplate))]
        public async Task<IActionResult> AddOrUpdateTemplate([FromBody]EmailTemplateModel emailTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _emailTemplateService.AddOrUpdateTemplateAsync(emailTemplate);

            return Ok();
        }

        [HttpGet("templates/{campaignId}/{templateId}")]
        [SwaggerOperation(nameof(GetTemplate))]
        public async Task<EmailTemplateModel> GetTemplate(
            [FromRoute]string campaignId,
            [FromRoute]string templateId)
        {
            return new EmailTemplateModel(await _emailTemplateService.GetTemplateAsync(campaignId, templateId));
        }

        [HttpGet("templates/{campaignId}")]
        [SwaggerOperation(nameof(GetCampaignTemplates))]
        public async Task<EmailTemplateModel[]> GetCampaignTemplates(
            [FromRoute]string campaignId)
        {
            return (await _emailTemplateService.GetCampaignTemplatesAsync(campaignId))
                .Select(t => new EmailTemplateModel(t))
                .ToArray();
        }
    }
}
