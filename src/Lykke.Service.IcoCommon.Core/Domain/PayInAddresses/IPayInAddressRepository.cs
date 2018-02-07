using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Domain.PayInAddresses
{
    public interface IPayInAddressRepository
    {
        Task UpsertAsync(string address, CurrencyType currency, string campaignId, string email);
        Task DeleteAsync(string address, CurrencyType currency);
        Task<IPayInAddressInfo> GetAsync(string address, CurrencyType currency);
    }
}
