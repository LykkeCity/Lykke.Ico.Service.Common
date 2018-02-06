using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Domain.Addresses
{
    public interface IAddressRepository
    {
        Task UpsertAsync(string address, string campaignId, CurrencyType currencyType, string email);
        Task DeleteAsync(string address, string campaignId);
    }
}
