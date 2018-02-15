using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmailTemplateRepository
    {
        Task UpsertAsync(IEmailTemplate emailTemplate);
        Task<IEmailTemplate> GetAsync(string campaignId, string templateId);
        Task<IEnumerable<IEmailTemplate>> GetCampaignTemplatesAsync(string campaignId);
        Task<IEnumerable<IEmailTemplate>> GetAllTemplatesAsync();
    }
}
