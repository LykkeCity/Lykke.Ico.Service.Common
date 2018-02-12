using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.JobTriggers.Triggers.Attributes;
using Lykke.Service.IcoCommon.Core;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Mail;

namespace Lykke.Service.IcoCommon.QueueHandlers
{
    public class EmailQueueHandler
    {
        private readonly ILog _log;
        private readonly IEmailService _emailService;

        [QueueTrigger(Constants.EmailQueue)]
        public async Task HandleEmail(EmailModel message)
        {
            await _log.WriteInfoAsync(nameof(HandleEmail),
                $"Message: {message.ToJson()}",
                $"New message");

            await _emailService.SendEmail(message);
        }
    }
}
