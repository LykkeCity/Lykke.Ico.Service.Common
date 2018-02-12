using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Blob;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.SettingsReader;

namespace Lykke.Service.IcoCommon.AzureRepositories.Mail
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly INoSQLTableStorage<EmailTemplateEntity> _tableStorage;
        private static string GetPartitionKey(string campaignId) => campaignId;
        private static string GetRowKey(string templateId) => templateId;

        public EmailTemplateRepository(IReloadingManager<string> connectionStringManager, ILog log)
        {
            _tableStorage = AzureTableStorage<EmailTemplateEntity>.Create(connectionStringManager, "EmailTemplates", log);
        }

        public async Task Upsert(string campaignId, string templateId, string subject, string body)
        {
            var partitionKey = GetPartitionKey(campaignId);
            var rowKey = GetRowKey(templateId);
            var entity = new EmailTemplateEntity()
            {
                PartitionKey = partitionKey,
                RowKey = rowKey,
                Subject = subject,
                Body = body
            };

            await _tableStorage.InsertOrReplaceAsync(entity);
        }

        public async Task<IEmailTemplate> Get(string campaignId, string templateId)
        {
            var partitionKey = GetPartitionKey(campaignId);
            var rowKey = GetRowKey(templateId);

            return await _tableStorage.GetDataAsync(campaignId, templateId);
        }
    }
}
