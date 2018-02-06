using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}