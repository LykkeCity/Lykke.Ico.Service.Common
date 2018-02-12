using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Services
{
    public interface ISmtpService
    {
        Task Send(string campaignId, string to, string subject, string body, Dictionary<string, byte[]> attachments = null);
    }
}
