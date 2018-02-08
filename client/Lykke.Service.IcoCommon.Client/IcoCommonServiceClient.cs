using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.IcoCommon.Client.Models;

namespace Lykke.Service.IcoCommon.Client
{
    public class IcoCommonServiceClient : IIcoCommonServiceClient, IDisposable
    {
        private readonly ILog _log;
        private readonly IIcoCommonAPI _api;

        public IcoCommonServiceClient(IIcoCommonAPI api, ILog log)
        {
            _api = api;
            _log = log;
        }

        public IcoCommonServiceClient(string serviceUrl, ILog log) : this(new IcoCommonAPI(new Uri(serviceUrl)), log)
        {
        }

        public void Dispose()
        {
            if (_api != null)
            {
                _api.Dispose();
            }
        }

        public async Task AddPayInAddressAsync(PayInAddressModel address, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _api.AddPayInAddressAsync(address, cancellationToken);
        }

        public async Task<int> HandleTransactionsAsync(IList<TransactionModel> transactions, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await _api.HandleTransactionsAsync(transactions, cancellationToken)) ?? 0;
        }
    }
}
