using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Client.Models;

namespace Lykke.Service.IcoCommon.Client
{
    public interface IIcoCommonServiceClient
    {
        Task AddPayInAddressAsync(PayInAddressModel address, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> HandleTransactionsAsync(IList<TransactionModel> transactions, CancellationToken cancellationToken = default(CancellationToken));
    }
}
