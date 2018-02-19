using System.Linq;
using System.Threading.Tasks;
using Lykke.Common.ApiLibrary.Contract;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            await _emailService.EnqueueEmailAsync(emailData);

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

        [HttpDelete("{to}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [SwaggerOperation(nameof(DeleteSentEmails))]
        public async Task<IActionResult> DeleteSentEmails(
            [FromRoute]string to,
            [FromQuery]string campaignId)
        {
            return Ok(await _emailService.DeleteEmailsAsync(to, campaignId));
        }

        [HttpPost("templates")]
        [SwaggerOperation(nameof(AddOrUpdateEmailTemplate))]
        public async Task<IActionResult> AddOrUpdateEmailTemplate([FromBody]EmailTemplateModel emailTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _emailTemplateService.AddOrUpdateTemplateAsync(emailTemplate);

            return Ok();
        }

        [HttpGet("templates")]
        [SwaggerOperation(nameof(GetAllEmailTemplates))]
        public async Task<EmailTemplateModel[]> GetAllEmailTemplates()
        {
            return (await _emailTemplateService.GetAllTemplatesAsync())
                .Select(t => new EmailTemplateModel(t))
                .ToArray();
        }

        [HttpGet("templates/{campaignId}")]
        [SwaggerOperation(nameof(GetCampaignEmailTemplates))]
        public async Task<EmailTemplateModel[]> GetCampaignEmailTemplates(
            [FromRoute]string campaignId)
        {
            return (await _emailTemplateService.GetCampaignTemplatesAsync(campaignId))
                .Select(t => new EmailTemplateModel(t))
                .ToArray();
        }

        [HttpGet("templates/{campaignId}/{templateId}")]
        [SwaggerOperation(nameof(GetEmailTemplate))]
        public async Task<EmailTemplateModel> GetEmailTemplate(
            [FromRoute]string campaignId,
            [FromRoute]string templateId)
        {
            return new EmailTemplateModel(await _emailTemplateService.GetTemplateAsync(campaignId, templateId));
        }

        [HttpDelete("templates/{campaignId}/{templateId}")]
        [SwaggerOperation(nameof(GetEmailTemplate))]
        public async Task DeleteEmailTemplate(
            [FromRoute]string campaignId,
            [FromRoute]string templateId)
        {
            await _emailTemplateService.DeleteTemplateAsync(campaignId, templateId);
        }
    }
}
