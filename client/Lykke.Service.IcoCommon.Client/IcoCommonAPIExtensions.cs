// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.IcoCommon.Client
{
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for IcoCommonAPI.
    /// </summary>
    public static partial class IcoCommonAPIExtensions
    {
            /// <summary>
            /// Adds pay-in address info for subsequent transaction check
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='payInAddress'>
            /// </param>
            public static void AddPayInAddress(this IIcoCommonAPI operations, PayInAddressModel payInAddress = default(PayInAddressModel))
            {
                operations.AddPayInAddressAsync(payInAddress).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Adds pay-in address info for subsequent transaction check
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='payInAddress'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task AddPayInAddressAsync(this IIcoCommonAPI operations, PayInAddressModel payInAddress = default(PayInAddressModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.AddPayInAddressWithHttpMessagesAsync(payInAddress, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Deletes specific pay-in address info
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='address'>
            /// </param>
            /// <param name='currency'>
            /// Possible values include: 'btc', 'eth', 'usd'
            /// </param>
            public static void DeletePayInAddress(this IIcoCommonAPI operations, string address, CurrencyType currency)
            {
                operations.DeletePayInAddressAsync(address, currency).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes specific pay-in address info
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='address'>
            /// </param>
            /// <param name='currency'>
            /// Possible values include: 'btc', 'eth', 'usd'
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeletePayInAddressAsync(this IIcoCommonAPI operations, string address, CurrencyType currency, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeletePayInAddressWithHttpMessagesAsync(address, currency, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Returns common campaign settings
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// Campaign identitfier
            /// </param>
            public static CampaignSettingsModel GetCampaignSettings(this IIcoCommonAPI operations, string campaignId)
            {
                return operations.GetCampaignSettingsAsync(campaignId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns common campaign settings
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// Campaign identitfier
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<CampaignSettingsModel> GetCampaignSettingsAsync(this IIcoCommonAPI operations, string campaignId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCampaignSettingsWithHttpMessagesAsync(campaignId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Creates or updates common campaign settings
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// Campaign identitfier
            /// </param>
            /// <param name='request'>
            /// </param>
            public static void CreateOrUpdateCampaignSettings(this IIcoCommonAPI operations, string campaignId, CampaignSettingsCreateOrUpdateRequest request = default(CampaignSettingsCreateOrUpdateRequest))
            {
                operations.CreateOrUpdateCampaignSettingsAsync(campaignId, request).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Creates or updates common campaign settings
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// Campaign identitfier
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task CreateOrUpdateCampaignSettingsAsync(this IIcoCommonAPI operations, string campaignId, CampaignSettingsCreateOrUpdateRequest request = default(CampaignSettingsCreateOrUpdateRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.CreateOrUpdateCampaignSettingsWithHttpMessagesAsync(campaignId, request, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Deletes all campaign data (emails, templates, addresses, settings)
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// Campaign identitfier
            /// </param>
            public static void DeleteCampaign(this IIcoCommonAPI operations, string campaignId)
            {
                operations.DeleteCampaignAsync(campaignId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes all campaign data (emails, templates, addresses, settings)
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// Campaign identitfier
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteCampaignAsync(this IIcoCommonAPI operations, string campaignId, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteCampaignWithHttpMessagesAsync(campaignId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Checks service is alive
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static object IsAlive(this IIcoCommonAPI operations)
            {
                return operations.IsAliveAsync().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Checks service is alive
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> IsAliveAsync(this IIcoCommonAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.IsAliveWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Adds email request into queue for subsequent sending
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='emailData'>
            /// Email data
            /// </param>
            public static void SendEmail(this IIcoCommonAPI operations, EmailDataModel emailData = default(EmailDataModel))
            {
                operations.SendEmailAsync(emailData).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Adds email request into queue for subsequent sending
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='emailData'>
            /// Email data
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task SendEmailAsync(this IIcoCommonAPI operations, EmailDataModel emailData = default(EmailDataModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.SendEmailWithHttpMessagesAsync(emailData, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Returns sent emails
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='to'>
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            public static IList<EmailModel> GetSentEmails(this IIcoCommonAPI operations, string to, string campaignId = default(string))
            {
                return operations.GetSentEmailsAsync(to, campaignId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns sent emails
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='to'>
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<EmailModel>> GetSentEmailsAsync(this IIcoCommonAPI operations, string to, string campaignId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetSentEmailsWithHttpMessagesAsync(to, campaignId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Deletes sent emails
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='to'>
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            public static void DeleteSentEmails(this IIcoCommonAPI operations, string to, string campaignId = default(string))
            {
                operations.DeleteSentEmailsAsync(to, campaignId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes sent emails
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='to'>
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteSentEmailsAsync(this IIcoCommonAPI operations, string to, string campaignId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteSentEmailsWithHttpMessagesAsync(to, campaignId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Return all email templates of all campaigns
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<EmailTemplateModel> GetAllEmailTemplates(this IIcoCommonAPI operations)
            {
                return operations.GetAllEmailTemplatesAsync().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Return all email templates of all campaigns
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<EmailTemplateModel>> GetAllEmailTemplatesAsync(this IIcoCommonAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAllEmailTemplatesWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Creates or updates email template
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='request'>
            /// </param>
            public static void AddOrUpdateEmailTemplate(this IIcoCommonAPI operations, EmailTemplateAddOrUpdateRequest request = default(EmailTemplateAddOrUpdateRequest))
            {
                operations.AddOrUpdateEmailTemplateAsync(request).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Creates or updates email template
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task AddOrUpdateEmailTemplateAsync(this IIcoCommonAPI operations, EmailTemplateAddOrUpdateRequest request = default(EmailTemplateAddOrUpdateRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.AddOrUpdateEmailTemplateWithHttpMessagesAsync(request, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Returns email templates of specified campaign
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            public static IList<EmailTemplateModel> GetCampaignEmailTemplates(this IIcoCommonAPI operations, string campaignId)
            {
                return operations.GetCampaignEmailTemplatesAsync(campaignId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns email templates of specified campaign
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<EmailTemplateModel>> GetCampaignEmailTemplatesAsync(this IIcoCommonAPI operations, string campaignId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCampaignEmailTemplatesWithHttpMessagesAsync(campaignId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Returns specific email template
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='templateId'>
            /// </param>
            public static EmailTemplateModel GetEmailTemplate(this IIcoCommonAPI operations, string campaignId, string templateId)
            {
                return operations.GetEmailTemplateAsync(campaignId, templateId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns specific email template
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='templateId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<EmailTemplateModel> GetEmailTemplateAsync(this IIcoCommonAPI operations, string campaignId, string templateId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetEmailTemplateWithHttpMessagesAsync(campaignId, templateId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Deletes specific email template
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='templateId'>
            /// </param>
            public static void DeleteEmailTemplate(this IIcoCommonAPI operations, string campaignId, string templateId)
            {
                operations.DeleteEmailTemplateAsync(campaignId, templateId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes specific email template
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='templateId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteEmailTemplateAsync(this IIcoCommonAPI operations, string campaignId, string templateId, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteEmailTemplateWithHttpMessagesAsync(campaignId, templateId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Returns history of changes of specific email template
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='templateId'>
            /// </param>
            public static IList<EmailTemplateHistoryItemModel> GetEmailTemplateHistory(this IIcoCommonAPI operations, string campaignId, string templateId)
            {
                return operations.GetEmailTemplateHistoryAsync(campaignId, templateId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Returns history of changes of specific email template
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='campaignId'>
            /// </param>
            /// <param name='templateId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<EmailTemplateHistoryItemModel>> GetEmailTemplateHistoryAsync(this IIcoCommonAPI operations, string campaignId, string templateId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetEmailTemplateHistoryWithHttpMessagesAsync(campaignId, templateId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Checks if transaction is an investor transaction and sends data to campaign
            /// API in this case
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='transactions'>
            /// List of transactions
            /// </param>
            public static int? HandleTransactions(this IIcoCommonAPI operations, IList<TransactionModel> transactions = default(IList<TransactionModel>))
            {
                return operations.HandleTransactionsAsync(transactions).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Checks if transaction is an investor transaction and sends data to campaign
            /// API in this case
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='transactions'>
            /// List of transactions
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<int?> HandleTransactionsAsync(this IIcoCommonAPI operations, IList<TransactionModel> transactions = default(IList<TransactionModel>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.HandleTransactionsWithHttpMessagesAsync(transactions, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
