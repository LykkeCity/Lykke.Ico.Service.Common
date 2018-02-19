using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Client.Models;

namespace Lykke.Service.IcoCommon.Client
{
    public interface IIcoCommonServiceClient
    {
        Task AddPayInAddressAsync(PayInAddressModel address);

        Task SendEmailAsync(EmailDataModel emailData);

        Task<IList<EmailModel>> GetSentEmailsAsync(string to, string campaignId = null);

        Task AddOrUpdateEmailTemplateAsync(EmailTemplateModel emailTemplate);

        Task<IList<EmailTemplateModel>> GetCampaignEmailTemplatesAsync(string campaignId);

        Task<EmailTemplateModel> GetEmailTemplateAsync(string campaignId, string templateId);

        Task<int> HandleTransactionsAsync(IList<TransactionModel> transactions);
    }
}
