using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;

namespace Lykke.Service.IcoCommon.Core.Services
{
    public interface ISmtpService
    {
        Task SendAsync(IEmail email, SmtpSettings smtpSettings);
    }
}
