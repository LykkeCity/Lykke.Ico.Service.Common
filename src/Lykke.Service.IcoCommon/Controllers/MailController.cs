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

        /// <summary>
        /// Adds email request into queue for subsequent sending
        /// </summary>
        /// <param name="emailData">Email data</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns sent emails
        /// </summary>
        /// <param name="to"></param>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        [HttpGet("{to}")]
        [SwaggerOperation(nameof(GetSentEmails))]
        public async Task<EmailModel[]> GetSentEmails(
            [FromRoute]string to,
            [FromQuery]string campaignId)
        {
            return (await _emailService.GetSentEmailsAsync(to, campaignId))
                .Select(email => EmailModel.Create(email))
                .ToArray();
        }

        /// <summary>
        /// Deletes sent emails
        /// </summary>
        /// <param name="to"></param>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        [HttpDelete("{to}")]
        [SwaggerOperation(nameof(DeleteSentEmails))]
        public async Task DeleteSentEmails(
            [FromRoute]string to,
            [FromQuery]string campaignId)
        {
            await _emailService.DeleteEmailsAsync(to, campaignId);
        }

        /// <summary>
        /// Creates or updates email template
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("templates")]
        [SwaggerOperation(nameof(AddOrUpdateEmailTemplate))]
        public async Task<IActionResult> AddOrUpdateEmailTemplate([FromBody]EmailTemplateAddOrUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _emailTemplateService.AddOrUpdateTemplateAsync(request.EmailTemplate, request.Username);

            return Ok();
        }

        /// <summary>
        /// Return all email templates of all campaigns
        /// </summary>
        /// <returns></returns>
        [HttpGet("templates")]
        [SwaggerOperation(nameof(GetAllEmailTemplates))]
        public async Task<EmailTemplateModel[]> GetAllEmailTemplates()
        {
            return (await _emailTemplateService.GetAllTemplatesAsync())
                .Select(t => EmailTemplateModel.Create(t))
                .ToArray();
        }

        /// <summary>
        /// Returns email templates of specified campaign
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        [HttpGet("templates/{campaignId}")]
        [SwaggerOperation(nameof(GetCampaignEmailTemplates))]
        public async Task<EmailTemplateModel[]> GetCampaignEmailTemplates(
            [FromRoute]string campaignId)
        {
            return (await _emailTemplateService.GetCampaignTemplatesAsync(campaignId))
                .Select(t => EmailTemplateModel.Create(t))
                .ToArray();
        }

        /// <summary>
        /// Returns specific email template
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [HttpGet("templates/{campaignId}/{templateId}")]
        [SwaggerOperation(nameof(GetEmailTemplate))]
        public async Task<EmailTemplateModel> GetEmailTemplate(
            [FromRoute]string campaignId,
            [FromRoute]string templateId)
        {
            return EmailTemplateModel.Create(await _emailTemplateService.GetTemplateAsync(campaignId, templateId));
        }

        /// <summary>
        /// Returns history of changes of specific email template
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [HttpGet("templates/{campaignId}/{templateId}/history")]
        [SwaggerOperation(nameof(GetEmailTemplateHistory))]
        public async Task<EmailTemplateHistoryItemModel[]> GetEmailTemplateHistory(
            [FromRoute]string campaignId,
            [FromRoute]string templateId)
        {
            return (await _emailTemplateService.GetHistoryAsync(campaignId, templateId))
                .Select(hi => EmailTemplateHistoryItemModel.Create(hi))
                .ToArray();
        }

        /// <summary>
        /// Deletes specific email template
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [HttpDelete("templates/{campaignId}/{templateId}")]
        [SwaggerOperation(nameof(DeleteEmailTemplate))]
        public async Task DeleteEmailTemplate(
            [FromRoute]string campaignId,
            [FromRoute]string templateId)
        {
            await _emailTemplateService.DeleteTemplateAsync(campaignId, templateId);
        }
    }
}
