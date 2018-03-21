using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.IcoCommon.Core.Domain.Campaign
{
    public interface ICampaignSettingsRepository
    {
        /// <summary>
        /// Returns settings from cache, if exist.
        /// Reloads settings if condition satisfied. 
        /// Throws exception if condition satisfied after reloading.
        /// </summary>
        /// <param name="campaignId">Campaign identifier</param>
        /// <param name="reloadIf">Condition to check cached value (value may be null here), method reloads value from storage if condition satisfied</param>
        /// <param name="doubleCheck">True to check value after reloading and throw exception if condition still satisfied, otherwise false</param>
        /// <returns></returns>
        Task<ICampaignSettings> GetCachedAsync(string campaignId, Func<ICampaignSettings, bool> reloadIf = null, bool doubleCheck = false);

        Task UpsertAsync(string campaignId, ICampaignSettings campaignSettings);

        Task DeleteAsync(string campaignId);
    }
}
