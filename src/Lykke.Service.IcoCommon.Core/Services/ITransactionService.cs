using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.Transactions;

namespace Lykke.Service.IcoCommon.Core.Services
{
    public interface ITransactionService
    {
        Task<int> HandleTransactions(ITransaction[] transactions);
    }
}
