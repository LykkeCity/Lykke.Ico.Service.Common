﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Blob;
using AzureStorage.Tables;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.SettingsReader;
using Microsoft.WindowsAzure.Storage.Table;
using static Lykke.Service.IcoCommon.AzureRepositories.Mail.EmailTemplateEntity;

namespace Lykke.Service.IcoCommon.AzureRepositories.Mail
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly INoSQLTableStorage<EmailTemplateEntity> _templateStorage;
        private readonly INoSQLTableStorage<EmailTemplateHistoryEntity> _templateHistoryStorage;

        private static string GetHistoryPartitionKey(string campaignId, string templateId) => $"{campaignId}_{templateId}";
        private static string GetHistoryRowKey(DateTime changedUtc) => changedUtc.ToString("O");

        public EmailTemplateRepository(IReloadingManager<string> connectionStringManager, ILog log)
        {
            _templateStorage = AzureTableStorage<EmailTemplateEntity>.Create(connectionStringManager, "EmailTemplates", log);
            _templateHistoryStorage = AzureTableStorage<EmailTemplateHistoryEntity>.Create(connectionStringManager, "EmailTemplateHistory", log);
        }

        public async Task UpsertAsync(IEmailTemplate emailTemplate)
        {
            var partitionKey = GetPartitionKey(emailTemplate.CampaignId);
            var rowKey = GetRowKey(emailTemplate.TemplateId);
            var entity = await _templateStorage.GetDataAsync(partitionKey, rowKey);

            if (entity == null ||
                entity.Subject != emailTemplate.Subject ||
                entity.Body != emailTemplate.Body ||
                entity.IsLayout != emailTemplate.IsLayout)
            {
                await _templateStorage.InsertOrReplaceAsync(new EmailTemplateEntity(emailTemplate));

                var historyPartitionKey = GetHistoryPartitionKey(emailTemplate.CampaignId, emailTemplate.TemplateId);
                var historyRowKey = GetHistoryRowKey(DateTime.UtcNow);
                var historyEntity = new EmailTemplateHistoryEntity(emailTemplate)
                {
                    PartitionKey = historyPartitionKey,
                    RowKey = historyRowKey
                };

                await _templateHistoryStorage.InsertAsync(historyEntity);
            }
        }

        public async Task<IEmailTemplate> GetAsync(string campaignId, string templateId)
        {
            var partitionKey = GetPartitionKey(campaignId);
            var rowKey = GetRowKey(templateId);

            return await _templateStorage.GetDataAsync(partitionKey, rowKey);
        }

        public async Task<IEnumerable<IEmailTemplate>> GetCampaignTemplatesAsync(string campaignId)
        {
            return await _templateStorage.GetDataAsync(GetPartitionKey(campaignId));
        }

        public async Task<IEnumerable<IEmailTemplate>> GetAllTemplatesAsync()
        {
            return await _templateStorage.GetDataAsync();
        }

        public async Task DeleteAsync(string campaignId, string templateId = null)
        {
            var templates = string.IsNullOrEmpty(templateId)
                ? await _templateStorage.GetDataAsync(GetPartitionKey(campaignId))
                : await _templateStorage.GetDataAsync(GetPartitionKey(campaignId), t => t.TemplateId == templateId);

            if (templates.Any())
            {
                await _templateStorage.DeleteAsync(templates);
            }

            // if it's a campaign deletion then delete history too

            if (string.IsNullOrEmpty(templateId))
            {
                var query = new TableQuery<EmailTemplateHistoryEntity>()
                    .Where(TableQuery.GenerateFilterCondition(nameof(EmailTemplateHistoryEntity.CampaignId), QueryComparisons.Equal, campaignId));

                var entities = new List<EmailTemplateHistoryEntity>();

                await _templateHistoryStorage.ExecuteAsync(query, chunk => entities.AddRange(chunk));

                if (entities.Any())
                {
                    await _templateHistoryStorage.DeleteAsync(entities);
                }
            }
        }
    }
}
