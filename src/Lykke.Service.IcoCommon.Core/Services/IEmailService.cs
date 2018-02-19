﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Core.Services
{
    public interface IEmailService
    {
        Task EnqueueEmailAsync(IEmailData emailData);
        Task SendEmailAsync(IEmailData emailData);
        Task<IEmail[]> GetSentEmailsAsync(string to, string campaignId);
        Task<int> DeleteEmailsAsync(string email, string campaignId);
    }
}
