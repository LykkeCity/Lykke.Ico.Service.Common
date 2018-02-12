using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;

namespace Lykke.Service.IcoCommon.Core.Services
{
    public interface IEmailService
    {
        Task EnqueueEmail(IEmail email);
        Task SendEmail(IEmail email);
        Task SaveEmailTemplate(IEmailTemplate emailTemplate);
    }
}
