using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Services;
using RazorLight;

namespace Lykke.Service.IcoCommon.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILog _log;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailRepository _emailRepository;

        public EmailService(IEmailTemplateService emailTemplateService, IEmailRepository emailRepository)
        {
            _emailTemplateService = emailTemplateService;
            _emailRepository = emailRepository;
        }

        public async Task PushEmailToQueueAsync(IEmailData emailData)
        {
            await _emailRepository.EnqueueAsync(emailData);
        }

        public async Task SendEmailAsync(IEmailData emailData)
        {
            await SendEmail(await _emailTemplateService.RenderEmailAsync(emailData));           
        }

        public async Task SendEmail(IEmail email)
        {
            // TODO: smtp send

            await _emailRepository.InsertAsync(email);

            await _log.WriteInfoAsync(nameof(SendEmailAsync),
                $"Campaign: {email.CampaignId}, Template: {email.TemplateId}, To: {email.To}",
                $"Email sent to {email.To}");
        }

        public Task<IEmail[]> GetSentEmailsAsync(string to, string campaignId)
        {
            throw new NotImplementedException();
        }
    }
}
