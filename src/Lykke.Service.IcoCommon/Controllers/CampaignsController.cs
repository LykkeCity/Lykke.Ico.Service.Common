using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.Campaign;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Campaign;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.IcoCommon.Controllers
{
    [Route("/api/campaigns")]
    public class CampaignsController : Controller
    {
        private readonly ILog _log;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IPayInAddressRepository _addressRepository;
        private readonly ICampaignSettingsRepository _campaignSettingsRepository;

        public CampaignsController(ILog log, IEmailService emailService, IEmailTemplateService emailTemplateService,
            IPayInAddressRepository addressRepository, ICampaignSettingsRepository campaignSettingsRepository)
        {
            _log = log;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _addressRepository = addressRepository;
            _campaignSettingsRepository = campaignSettingsRepository;
        }

        /// <summary>
        /// Returns common campaign settings
        /// </summary>
        /// <param name="campaignId">Campaign identitfier</param>
        /// <returns></returns>
        [HttpGet("{campaignId}/settings")]
        [SwaggerOperation(nameof(GetCampaignSettings))]
        public async Task<CampaignSettingsModel> GetCampaignSettings([FromRoute] string campaignId)
        {
            return CampaignSettingsModel.Create(await _campaignSettingsRepository.GetCachedAsync(campaignId));
        }

        /// <summary>
        /// Creates or updates common campaign settings
        /// </summary>
        /// <param name="campaignId">Campaign identitfier</param>
        /// <param name="campaignSettings">Common campaign settings</param>
        /// <returns></returns>
        [HttpPost("{campaignId}/settings")]
        [SwaggerOperation(nameof(CreateOrUpdateCampaignSettings))]
        public async Task CreateOrUpdateCampaignSettings([FromRoute] string campaignId, [FromBody] CampaignSettingsModel campaignSettings)
        {
            await _campaignSettingsRepository.UpsertAsync(campaignId, campaignSettings);
        }

        /// <summary>
        /// Deletes all campaign data (emails, templates, addresses, settings)
        /// </summary>
        /// <param name="campaignId">Campaign identitfier</param>
        /// <returns></returns>
        [HttpDelete("{campaignId}")]
        [SwaggerOperation(nameof(DeleteCampaign))]
        public async Task DeleteCampaign([FromRoute] string campaignId)
        {
            await _emailService.DeleteCampaignEmailsAsync(campaignId);
            await _emailTemplateService.DeleteCampaignTemplatesAsync(campaignId);
            await _addressRepository.DeleteAsync(campaignId);
            await _campaignSettingsRepository.DeleteAsync(campaignId);
            await _log.WriteInfoAsync(nameof(DeleteCampaign), campaignId, "Campaign deleted");
        }
    }
}
