using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmailRepository
    {
        Task PushToQueueAsync(IEmailData emailData);
        Task InsertAsync(IEmail email);
        Task<IEnumerable<IEmail>> GetAsync(string to, string campaignId);
    }
}
