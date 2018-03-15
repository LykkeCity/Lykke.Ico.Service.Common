using System;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public interface IEmailTemplateHistoryItem : IEmailTemplate
    {
        DateTime ChangedUtc { get; }
        string Username { get; }
    }
}
