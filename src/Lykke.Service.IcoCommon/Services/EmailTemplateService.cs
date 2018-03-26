using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Mail;
using Microsoft.Extensions.Caching.Memory;
using RazorLight;

namespace Lykke.Service.IcoCommon.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _templateRepository;
        private readonly IMemoryCache _cache;
        private static string CacheKey(string campaignId) => $"RazorLightEngine_{campaignId}";

        public EmailTemplateService(IEmailTemplateRepository templateRepository, IMemoryCache cache)
        {
            _templateRepository = templateRepository;
            _cache = cache;
        }     

        public RazorLightEngine BuildEngine(string campaignId)
        {
            return new RazorLightEngineBuilder()
                .UseProject(new AzureTableRazorLightProject(campaignId, _templateRepository))
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task AddOrUpdateTemplateAsync(IEmailTemplate emailTemplate, string username)
        {
            await _templateRepository.UpsertAsync(emailTemplate, username);

            _cache.Remove(CacheKey(emailTemplate.CampaignId));
        }

        public async Task<IEmail> RenderEmailAsync(IEmailData emailData)
        {
            var emailModel = new EmailModel
            {
                CampaignId = emailData.CampaignId,
                TemplateId = emailData.TemplateId,
                To = emailData.To,
                Subject = emailData.Subject,
                Body = await _cache.GetOrCreate(CacheKey(emailData.CampaignId), e => BuildEngine(emailData.CampaignId))
                    .CompileRenderAsync(emailData.TemplateId, emailData.Data),
                Attachments = emailData.Attachments,
            };

            if (string.IsNullOrWhiteSpace(emailData.Subject))
            {
                emailModel.Subject = (await _templateRepository.GetAsync(emailData.CampaignId, emailData.TemplateId)).Subject;
            }

            return emailModel;
        }

        public async Task<IEmailTemplate> GetTemplateAsync(string campaignId, string templateId)
        {
            return await _templateRepository.GetAsync(campaignId, templateId);
        }

        public async Task<IEmailTemplate[]> GetCampaignTemplatesAsync(string campaignId)
        {
            return (await _templateRepository.GetCampaignTemplatesAsync(campaignId)).ToArray();
        }

        public async Task<IEmailTemplate[]> GetAllTemplatesAsync()
        {
            return (await _templateRepository.GetAllTemplatesAsync()).ToArray();
        }

        public async Task<IEmailTemplateHistoryItem[]> GetHistoryAsync(string campaignId, string templateId)
        {
            return (await _templateRepository.GetHistoryAsync(campaignId, templateId)).ToArray();
        }

        public async Task DeleteTemplateAsync(string campaignId, string templateId)
        {
            await _templateRepository.DeleteAsync(campaignId, templateId);

            // reset the whole campaign cache due to weird work of RazorLight built-in cache
            _cache.Remove(CacheKey(campaignId));
        }

        public async Task DeleteCampaignTemplatesAsync(string campaignId)
        {
            await _templateRepository.DeleteAsync(campaignId);

            _cache.Remove(CacheKey(campaignId));
        }
    }
}
