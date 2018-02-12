using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using RazorLight.Razor;

namespace Lykke.Service.IcoCommon.Services
{
    public class AzureRazorLightProject : RazorLightProject
    {
        private readonly string _campaignId;
        private readonly IEmailTemplateRepository _templateRepository;

        public AzureRazorLightProject(string campaignId, IEmailTemplateRepository templateRepository)
        {
            _campaignId = campaignId;
            _templateRepository = templateRepository;
        }

        public async override Task<IEnumerable<RazorLightProjectItem>> GetImportsAsync(string templateKey)
        {
            return Enumerable.Empty<RazorLightProjectItem>();
        }

        public async override Task<RazorLightProjectItem> GetItemAsync(string templateId)
        {
            return new AzureRazorLightProjectItem(await _templateRepository.Get(_campaignId, templateId));
        }
    }
}
