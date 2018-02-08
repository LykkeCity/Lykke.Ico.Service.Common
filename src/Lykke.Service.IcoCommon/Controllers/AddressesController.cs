using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.ApiLibrary.Contract;
using Lykke.Service.IcoCommon.Core.Domain.PayInAddresses;
using Lykke.Service.IcoCommon.Models.Addresses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.IcoCommon.Controllers
{
    [Route("/api/addresses")]
    public class AddressesController : Controller
    {
        private IPayInAddressRepository _addressRepository;

        public AddressesController(IPayInAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpPost]
        [SwaggerOperation(nameof(AddPayInAddress))]
        public async Task<IActionResult> AddPayInAddress([FromBody]PayInAddressModel address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _addressRepository.UpsertAsync(
                address.Address, 
                address.Currency, 
                address.CampaignId, 
                address.Email);

            return Ok();
        }

        [HttpDelete]
        [SwaggerOperation(nameof(DeletePayInAddress))]
        public async Task<IActionResult> DeletePayInAddress([FromBody]DeletePayInAddressRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _addressRepository.DeleteAsync(
                request.Address,
                request.Currency);

            return Ok();
        }
    }
}
