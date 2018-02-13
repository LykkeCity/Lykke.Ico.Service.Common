using System;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.JobTriggers.Triggers.Attributes;
using Lykke.Service.IcoCommon.Core;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Mail;

namespace Lykke.Service.IcoCommon.AzureQueueHandlers
{
    public class EmailQueueHandler
    {
        private readonly ILog _log;
        private readonly IEmailService _emailService;

        public EmailQueueHandler(ILog log, IEmailService emailService)
        {
            _log = log;
            _emailService = emailService;
        }

        [QueueTrigger(Constants.EmailQueue)]
        public async Task HandleEmailQueueMessage(EmailDataModel emailData)
        {
            try
            {
                await _emailService.SendEmailAsync(emailData);
            }
            catch (Exception ex)
            {
                await _log.WriteErrorAsync(nameof(HandleEmailQueueMessage),
                    $"Message: {emailData.ToJson()}",
                    ex);

                throw;
            }
        }
    }
}
