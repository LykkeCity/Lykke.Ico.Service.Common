using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmailTemplateRepository
    {
        Task Upsert(string campaignId, string templateId, string subject, string body);
        Task<IEmailTemplate> Get(string campaignId, string templateId);
    }
}
