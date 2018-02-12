using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Services
{
    public interface IRazorRenderService
    {
        Task<String> Render(string campaignId, string templateId, object model);
        Task UpdateCache(string campaignId, string templateId);
    }
}
