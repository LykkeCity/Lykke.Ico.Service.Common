using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmailTemplateRepository
    {
        Task UpsertAsync(IEmailTemplate emailTemplate, string username);
        Task<IEmailTemplate> GetAsync(string campaignId, string templateId);
        Task<IEnumerable<IEmailTemplate>> GetCampaignTemplatesAsync(string campaignId);
        Task<IEnumerable<IEmailTemplate>> GetAllTemplatesAsync();
        Task<IEnumerable<IEmailTemplateHistoryItem>> GetHistoryAsync(string campaignId, string templateId);
        Task DeleteAsync(string campaignId, string templateId = null);
    }
}
