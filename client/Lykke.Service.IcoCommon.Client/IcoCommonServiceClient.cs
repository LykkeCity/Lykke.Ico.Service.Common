using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.IcoCommon.Client.Models;

namespace Lykke.Service.IcoCommon.Client
{
    public class IcoCommonServiceClient : IIcoCommonServiceClient, IDisposable
    {
        private readonly ILog _log;
        private readonly IIcoCommonAPI _api;

        public IcoCommonServiceClient(IIcoCommonAPI api, ILog log)
        {
            _api = api;
            _log = log;
        }

        public IcoCommonServiceClient(string serviceUrl, ILog log) : this(new IcoCommonAPI(new Uri(serviceUrl)), log)
        {
        }

        public void Dispose()
        {
            if (_api != null)
            {
                _api.Dispose();
            }
        }

        public async Task AddPayInAddressAsync(PayInAddressModel address)
        {
            await _api.AddPayInAddressAsync(address);
        }

        public async Task SendEmailAsync(EmailDataModel emailData)
        {
            await _api.SendEmailAsync(emailData);
        }

        public async Task<IList<EmailModel>> GetSentEmailsAsync(string to, string campaignId = null)
        {
            return await _api.GetSentEmailsAsync(to, campaignId);
        }

        public async Task DeleteSentEmailsAsync(string to, string campaignId = null)
        {
            await _api.DeleteSentEmailsAsync(to, campaignId);
        }

        public async Task AddOrUpdateEmailTemplateAsync(EmailTemplateModel emailTemplate)
        {
            await _api.AddOrUpdateEmailTemplateAsync(emailTemplate);
        }

        public async Task<IList<EmailTemplateModel>> GetAllEmailTemplatesAsync()
        {
            return await _api.GetAllEmailTemplatesAsync();
        }

        public async Task<IList<EmailTemplateModel>> GetCampaignEmailTemplatesAsync(string campaignId)
        {
            return await _api.GetCampaignEmailTemplatesAsync(campaignId);
        }

        public async Task<EmailTemplateModel> GetEmailTemplateAsync(string campaignId, string templateId)
        {
            return await _api.GetEmailTemplateAsync(campaignId, templateId);
        }

        public async Task<int> HandleTransactionsAsync(IList<TransactionModel> transactions)
        {
            return (await _api.HandleTransactionsAsync(transactions)) ?? 0;
        }
    }
}
