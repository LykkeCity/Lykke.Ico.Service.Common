using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Lykke.SettingsReader;

namespace Lykke.Service.IcoCommon.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILog _log;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailRepository _emailRepository;
        private readonly ISmtpService _smtpService;
        private readonly IReloadingManager<Dictionary<string, CampaignSettings>> _settings;
        private readonly SmtpSettings _defaultSmtpSettings;

        public EmailService(
            ILog log, 
            IEmailTemplateService emailTemplateService,
            IEmailRepository emailRepository,
            ISmtpService smtpService,
            IReloadingManager<Dictionary<string, CampaignSettings>> settings)
        {
            _log = log;
            _emailTemplateService = emailTemplateService;
            _emailRepository = emailRepository;
            _smtpService = smtpService;
            _settings = settings;
        }

        public async Task EnqueueEmailAsync(IEmailData emailData)
        {
            await _emailRepository.PushToQueueAsync(emailData);

            await _log.WriteInfoAsync(nameof(EnqueueEmailAsync),
                $"Campaign: {emailData.CampaignId}, Template: {emailData.TemplateId}, To: {emailData.To}",
                $"Email enqueued");
        }

        public async Task SendEmailAsync(IEmailData emailData)
        {
            await SendEmailAsync(await _emailTemplateService.RenderEmailAsync(emailData));           
        }

        public async Task SendEmailAsync(IEmail email)
        {
            CampaignSettings campaignSettings = null;

            if (!_settings.CurrentValue.TryGetValue(email.CampaignId, out campaignSettings))
            {
                await _settings.Reload();
            }

            if (!_settings.CurrentValue.TryGetValue(email.CampaignId, out campaignSettings))
            {
                await _log.WriteWarningAsync(nameof(SendEmailAsync),
                    $"Campaign: {email.CampaignId}, Template: {email.TemplateId}, To: {email.To}",
                    $"Configuration for campaign \"{email.CampaignId}\" not found. Default SMTP settings are used to send the email.");
            }

            //await _smtpService.SendAsync(email, campaignSettings?.Smtp);

            await _emailRepository.InsertAsync(email);

            await _log.WriteInfoAsync(nameof(SendEmailAsync),
                $"Campaign: {email.CampaignId}, Template: {email.TemplateId}, To: {email.To}",
                $"Email sent to {email.To}");
        }

        public async Task<IEmail[]> GetSentEmailsAsync(string to, string campaignId = null)
        {
            return (await _emailRepository.GetAsync(to, campaignId)).ToArray();
        }

        public async Task<int> DeleteEmailsAsync(string to, string campaignId = null)
        {
            return await _emailRepository.DeleteAsync(to, campaignId);
        }
    }
}
