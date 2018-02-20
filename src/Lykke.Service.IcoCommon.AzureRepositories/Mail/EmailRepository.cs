using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Queue;
using AzureStorage.Tables;
using Common;
using Common.Log;
using Lykke.Service.IcoCommon.Core;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.SettingsReader;
using Microsoft.WindowsAzure.Storage.Table;
using static Lykke.Service.IcoCommon.AzureRepositories.Mail.EmailEntity;

namespace Lykke.Service.IcoCommon.AzureRepositories.Mail
{
    public class EmailRepository : IEmailRepository
    {
        private readonly INoSQLTableStorage<EmailEntity> _tableStorage;
        private readonly IQueueExt _queueExt;

        public EmailRepository(IReloadingManager<string> connectionStringManager, ILog log)
        {
            _tableStorage = AzureTableStorage<EmailEntity>.Create(connectionStringManager, "Emails", log);
            _queueExt = AzureQueueExt.Create(connectionStringManager, Constants.EmailQueue);
        }

        public async Task PushToQueueAsync(IEmailData emailData)
        {
            await _queueExt.PutRawMessageAsync(emailData.ToJson());
        }

        public async Task<IEnumerable<IEmail>> GetAsync(string to, string campaignId = null)
        {
            return string.IsNullOrEmpty(campaignId) 
                ? await _tableStorage.GetDataAsync(GetPartitionKey(to))
                : await _tableStorage.GetDataAsync(GetPartitionKey(to), email => email.CampaignId == campaignId);
        }

        public async Task InsertAsync(IEmail email)
        {
            var entity = new EmailEntity(email)
            {
                SentUtc =
                    email.SentUtc != default(DateTime) ?
                    email.SentUtc :
                    DateTime.UtcNow
            };

            await _tableStorage.InsertAsync(entity);
        }

        public async Task DeleteAsync(string to, string campaignId = null)
        {
            var emails = string.IsNullOrEmpty(campaignId) 
                ? await _tableStorage.GetDataAsync(GetPartitionKey(to))
                : await _tableStorage.GetDataAsync(GetPartitionKey(to), email => email.CampaignId == campaignId);

            if (emails.Any())
            {
                await _tableStorage.DeleteAsync(emails);
            }
        }

        public async Task DeleteAsync(string campaignId)
        {
            var query = new TableQuery<EmailEntity>()
                .Where(TableQuery.GenerateFilterCondition(nameof(EmailEntity.CampaignId), QueryComparisons.Equal, campaignId));

            var entities = new List<EmailEntity>();

            await _tableStorage.ExecuteAsync(query, chunk => entities.AddRange(chunk));

            if (entities.Any())
            {
                await _tableStorage.DeleteAsync(entities);
            }
        }
    }
}
