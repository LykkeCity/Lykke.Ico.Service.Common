using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.IcoCommon.Controllers
{
    [Route("/api/campaign")]
    public class CampaignController : Controller
    {
        private readonly ILog _log;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IPayInAddressRepository _addressRepository;


        public CampaignController(ILog log, IEmailService emailService, IEmailTemplateService emailTemplateService, IPayInAddressRepository addressRepository)
        {
            _log = log;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _addressRepository = addressRepository;
        }

        [HttpDelete("{campaignId}")]
        [SwaggerOperation(nameof(DeleteCampaign))]
        public async Task DeleteCampaign(string campaignId)
        {
            await _emailService.DeleteCampaignEmailsAsync(campaignId);
            await _emailTemplateService.DeleteCampaignTemplatesAsync(campaignId);
            await _addressRepository.DeleteAsync(campaignId);
            await _log.WriteInfoAsync(nameof(DeleteCampaign), campaignId, "Campaign deleted");
        }
    }
}
