using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Client.Models;

namespace Lykke.Service.IcoCommon.Client
{
    public interface IIcoCommonServiceClient
    {
        Task AddPayInAddressAsync(PayInAddressModel address, CancellationToken cancellationToken = default(CancellationToken));

        Task SendEmailAsync(EmailDataModel emailData, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<EmailModel>> GetSentEmailsAsync(string to, string campaignId = null, CancellationToken cancellationToken = default(CancellationToken));

        Task AddOrUpdateEmailTemplateAsync(EmailTemplateModel emailTemplate, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<EmailTemplateModel>> GetCampaignEmailTemplatesAsync(string campaignId, CancellationToken cancellationToken = default(CancellationToken));

        Task<EmailTemplateModel> GetEmailTemplateAsync(string campaignId, string templateId, CancellationToken cancellationToken = default(CancellationToken));

        Task<int> HandleTransactionsAsync(IList<TransactionModel> transactions, CancellationToken cancellationToken = default(CancellationToken));
    }
}
