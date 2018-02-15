using System.Collections.Generic;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Blob;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.SettingsReader;
using static Lykke.Service.IcoCommon.AzureRepositories.Mail.EmailTemplateEntity;

namespace Lykke.Service.IcoCommon.AzureRepositories.Mail
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly INoSQLTableStorage<EmailTemplateEntity> _tableStorage;

        public EmailTemplateRepository(IReloadingManager<string> connectionStringManager, ILog log)
        {
            _tableStorage = AzureTableStorage<EmailTemplateEntity>.Create(connectionStringManager, "EmailTemplates", log);
        }

        public async Task UpsertAsync(IEmailTemplate emailTemplate)
        {
            await _tableStorage.InsertOrReplaceAsync(new EmailTemplateEntity(emailTemplate));
        }

        public async Task<IEmailTemplate> GetAsync(string campaignId, string templateId)
        {
            var partitionKey = GetPartitionKey(campaignId);
            var rowKey = GetRowKey(templateId);

            return await _tableStorage.GetDataAsync(partitionKey, rowKey);
        }

        public async Task<IEnumerable<IEmailTemplate>> GetCampaignTemplatesAsync(string campaignId)
        {
            return await _tableStorage.GetDataAsync(GetPartitionKey(campaignId));
        }

        public async Task<IEnumerable<IEmailTemplate>> GetAllTemplatesAsync()
        {
            return await _tableStorage.GetDataAsync();
        }
    }
}
