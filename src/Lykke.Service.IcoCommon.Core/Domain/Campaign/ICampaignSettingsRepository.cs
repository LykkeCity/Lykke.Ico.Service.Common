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
        /// <param name="reloadIf">
        /// Condition to check cached value (value may be null here), 
        /// method reloads value from storage if condition satisfied,
        /// and re-checks condition.
        /// </param>
        /// <returns></returns>
        Task<ICampaignSettings> GetCachedAsync(string campaignId, Func<ICampaignSettings, bool> reloadIf = null);

        Task UpsertAsync(string campaignId, ICampaignSettings campaignSettings, string username);

        Task DeleteAsync(string campaignId);
    }
}
