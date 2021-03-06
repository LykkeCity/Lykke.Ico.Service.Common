﻿using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Campaign;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Services;

namespace Lykke.Service.IcoCommon.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILog _log;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailRepository _emailRepository;
        private readonly ISmtpService _smtpService;
        private readonly ICampaignSettingsRepository _campaignSettingsRepository;

        public EmailService(
            ILog log, 
            IEmailTemplateService emailTemplateService,
            IEmailRepository emailRepository,
            ISmtpService smtpService,
            ICampaignSettingsRepository campaignSettingsRepository)
        {
            _log = log;
            _emailTemplateService = emailTemplateService;
            _emailRepository = emailRepository;
            _smtpService = smtpService;
            _campaignSettingsRepository = campaignSettingsRepository;
        }

        public async Task EnqueueEmailAsync(IEmailData emailData)
        {
            var settings = await _campaignSettingsRepository.GetCachedAsync(emailData.CampaignId);

            if (settings != null && !string.IsNullOrEmpty(settings.EmailBlackList) && settings.EmailBlackList.Contains(emailData.To))
            {
                await _log.WriteWarningAsync(nameof(EnqueueEmailAsync),
                    $"Campaign: {emailData.CampaignId}, Template: {emailData.TemplateId}, To: {emailData.To}",
                    $"Black-listed email skipped");
            }
            else
            {
                await _emailRepository.PushToQueueAsync(emailData);

                await _log.WriteInfoAsync(nameof(EnqueueEmailAsync),
                    $"Campaign: {emailData.CampaignId}, Template: {emailData.TemplateId}, To: {emailData.To}",
                    $"Email enqueued");
            }
        }

        public async Task SendEmailAsync(IEmailData emailData)
        {
            var settings = await _campaignSettingsRepository.GetCachedAsync(emailData.CampaignId);

            if (settings != null && !string.IsNullOrEmpty(settings.EmailBlackList) && settings.EmailBlackList.Contains(emailData.To))
            {
                await _log.WriteWarningAsync(nameof(SendEmailAsync),
                    $"Campaign: {emailData.CampaignId}, Template: {emailData.TemplateId}, To: {emailData.To}",
                    $"Black-listed email skipped");
            }
            else
            {
                await SendEmailAsync(await _emailTemplateService.RenderEmailAsync(emailData));
            }
        }

        public async Task SendEmailAsync(IEmail email)
        {
            var campaignSettings = await _campaignSettingsRepository.GetCachedAsync(email.CampaignId,
                reloadIf: x => x?.Smtp == null);

            await _smtpService.SendAsync(email, campaignSettings.Smtp);

            await _emailRepository.InsertAsync(email);

            await _log.WriteInfoAsync(nameof(SendEmailAsync),
                $"Campaign: {email.CampaignId}, Template: {email.TemplateId}, To: {email.To}",
                $"Email sent to {email.To}");
        }

        public async Task<IEmail[]> GetSentEmailsAsync(string to, string campaignId = null)
        {
            return (await _emailRepository.GetAsync(to, campaignId)).ToArray();
        }

        public async Task DeleteEmailsAsync(string to, string campaignId = null)
        {
            await _emailRepository.DeleteAsync(to, campaignId);

            await _log.WriteInfoAsync(nameof(DeleteEmailsAsync), $"Campaign: {campaignId}, To: {to}",
                "Emails deleted");

        }

        public async Task DeleteCampaignEmailsAsync(string campaignId)
        {
            await _emailRepository.DeleteAsync(campaignId);

            await _log.WriteInfoAsync(nameof(DeleteCampaignEmailsAsync), $"Campaign: {campaignId}",
                "Emails deleted");
        }
    }
}
