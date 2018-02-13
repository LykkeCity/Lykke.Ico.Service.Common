using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using RazorLight.Razor;

namespace Lykke.Service.IcoCommon.Services
{
    public class AzureTableRazorLightProject : RazorLightProject
    {
        private readonly string _campaignId;
        private readonly IEmailTemplateRepository _templateRepository;

        public AzureTableRazorLightProject(string campaignId, IEmailTemplateRepository templateRepository)
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
            return new AzureTableRazorLightProjectItem(await _templateRepository.GetAsync(_campaignId, templateId));
        }
    }
}
