using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Services;
using RazorLight;

namespace Lykke.Service.IcoCommon.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpService _smtpService;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IRazorRenderService _razorRenderService;

        public EmailService(/*ISmtpService smtpService,*/ IEmailTemplateRepository emailTemplateRepository, IRazorRenderService razorRenderService)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _razorRenderService = razorRenderService;
        }

        public async Task EnqueueEmail(IEmail email)
        {
            await SendEmail(email);
        }

        public async Task SendEmail(IEmail email)
        {
            var body = await _razorRenderService.Render(email.CampaignId, email.TemplateId, email.Params);
        }

        public async Task SaveEmailTemplate(IEmailTemplate emailTemplate)
        {
            await _emailTemplateRepository.Upsert(emailTemplate.CampaignId, emailTemplate.TemplateId, emailTemplate.Subject, emailTemplate.Body);
            await _razorRenderService.UpdateCache(emailTemplate.CampaignId, emailTemplate.TemplateId);
        }
    }
}
