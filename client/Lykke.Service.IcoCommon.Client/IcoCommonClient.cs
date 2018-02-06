using System;
using Common.Log;

namespace Lykke.Service.IcoCommon.Client
{
    public class IcoCommonClient : IIcoCommonClient, IDisposable
    {
        private readonly ILog _log;

        public IcoCommonClient(string serviceUrl, ILog log)
        {
            _log = log;
        }

        public void Dispose()
        {
            //if (_service == null)
            //    return;
            //_service.Dispose();
            //_service = null;
        }
    }
}
