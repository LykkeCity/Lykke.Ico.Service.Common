using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Models.Mail;

namespace Lykke.Service.IcoCommon.Core.Domain.Mail
{
    public static class EmailExtensions
    {
        public static EmailModel ToModel(this IEmail self)
        {
            return new EmailModel
            {
                
            };
        }
    }
}
