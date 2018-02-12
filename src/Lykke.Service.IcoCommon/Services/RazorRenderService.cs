using System.Collections.Concurrent;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Services;
using RazorLight;

namespace Lykke.Service.IcoCommon.Services
{
    public class RazorRenderService : IRazorRenderService
    {
        private readonly IEmailTemplateRepository _templateRepository;
        private readonly ConcurrentDictionary<string, RazorLightEngine> _razorEngines = new ConcurrentDictionary<string, RazorLightEngine>();

        public RazorRenderService(IEmailTemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<string> Render(string campaignId, string templateId, object model)
        {
            return await _razorEngines.GetOrAdd(campaignId, BuildEngine).CompileRenderAsync(templateId, model);
        }

        public async Task UpdateCache(string campaignId, string templateId)
        {
            var engine = _razorEngines.GetOrAdd(campaignId, BuildEngine);

            if (engine.TemplateCache.Contains(templateId))
            {
                engine.TemplateCache.Remove(templateId);
            }

            await engine.CompileTemplateAsync(templateId);
        }

        public RazorLightEngine BuildEngine(string campaignId)
        {
            return new RazorLightEngineBuilder()
                .UseProject(new AzureRazorLightProject(campaignId, _templateRepository))
                .UseMemoryCachingProvider()
                .Build();
        }
    }
}
