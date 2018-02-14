using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;

namespace Lykke.Service.IcoCommon.Core.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task EnqueueTransactionAsync(ITransaction tx, IPayInAddress payInAddress, string connectionString);
    }
}
