using System.Collections.Concurrent;
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

        public async Task AddOrUpdateTemplateAsync(IEmailTemplate emailTemplate)
        {
            await _templateRepository.UpsertAsync(emailTemplate);

            var engine = _razorCache.GetOrAdd(emailTemplate.CampaignId, BuildEngine);

            if (engine.TemplateCache.Contains(emailTemplate.TemplateId)) engine.TemplateCache.Remove(emailTemplate.TemplateId);

            await engine.CompileTemplateAsync(emailTemplate.TemplateId);
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

        public Task<IEmailTemplate> GetTemplateAsync(string campaignId, string templateId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEmailTemplate[]> GetCampaignTemplatesAsync(string campaignId)
        {
            throw new System.NotImplementedException();
        }
    }
}
