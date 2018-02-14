using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Domain.PayInAddresses
{
    public interface IPayInAddressRepository
    {
        Task InsertAsync(IPayInAddress payInAddress);
        Task DeleteAsync(string address, CurrencyType currency);
        Task<IPayInAddress> GetAsync(string address, CurrencyType currency);
    }
}
