using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Core.Services
{
    public interface IEmailTemplateService
    {
        Task AddOrUpdateTemplateAsync(IEmailTemplate emailTemplate, string username);
        Task<IEmail> RenderEmailAsync(IEmailData emailData);
        Task<IEmailTemplate> GetTemplateAsync(string campaignId, string templateId);
        Task<IEmailTemplate[]> GetCampaignTemplatesAsync(string campaignId);
        Task<IEmailTemplate[]> GetAllTemplatesAsync();
        Task<IEmailTemplateHistoryItem[]> GetHistoryAsync(string campaignId, string templateId);
        Task DeleteTemplateAsync(string campaignId, string templateId);
        Task DeleteCampaignTemplatesAsync(string campaignId);
    }
}
