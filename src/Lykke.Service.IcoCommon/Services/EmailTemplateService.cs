using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Mail;
using RazorLight;

namespace Lykke.Service.IcoCommon.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _templateRepository;
        private readonly ConcurrentDictionary<string, RazorLightEngine> _razorCache = new ConcurrentDictionary<string, RazorLightEngine>();

        public EmailTemplateService(IEmailTemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }     

        public RazorLightEngine BuildEngine(string campaignId)
        {
            return new RazorLightEngineBuilder()
                .UseProject(new AzureTableRazorLightProject(campaignId, _templateRepository))
                .UseMemoryCachingProvider()
                .Build();
        }

        public void ResetCache(string campaignId)
        {
            _razorCache.TryRemove(campaignId, out var _);
        }

        public async Task AddOrUpdateTemplateAsync(IEmailTemplate emailTemplate, string username)
        {
            await _templateRepository.UpsertAsync(emailTemplate, username);

            ResetCache(emailTemplate.CampaignId);
        }

        public async Task<IEmail> RenderEmailAsync(IEmailData emailData)
        {
            var emailModel = new EmailModel
            {
                CampaignId = emailData.CampaignId,
                TemplateId = emailData.TemplateId,
                To = emailData.To,
                Subject = emailData.Subject,
                Body = await _razorCache.GetOrAdd(emailData.CampaignId, BuildEngine)
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

            ResetCache(campaignId);
        }

        public async Task DeleteCampaignTemplatesAsync(string campaignId)
        {
            await _templateRepository.DeleteAsync(campaignId);

            ResetCache(campaignId);
        }
    }
}
