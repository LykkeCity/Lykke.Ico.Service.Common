using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Log;
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
        private readonly ILog _log;
        private IPayInAddressRepository _addressRepository;

        public AddressesController(ILog log, IPayInAddressRepository addressRepository)
        {
            _log = log;
            _addressRepository = addressRepository;
        }

        /// <summary>
        /// Adds pay-in address info for subsequent transaction check
        /// </summary>
        /// <param name="payInAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(nameof(AddPayInAddress))]
        public async Task<IActionResult> AddPayInAddress([FromBody]PayInAddressModel payInAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            var entity = await _addressRepository.GetAsync(payInAddress.Address, payInAddress.Currency);

            if (entity != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, 
                    ErrorResponse.Create($"Pay-in address {payInAddress.Address} is already in use"));
            }

            await _addressRepository.InsertAsync(payInAddress);

            await _log.WriteInfoAsync(nameof(AddPayInAddress), payInAddress.ToJson(), 
                "Pay-in address added");

            return Ok();
        }

        /// <summary>
        /// Deletes specific pay-in address info
        /// </summary>
        /// <param name="address"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        [HttpDelete("{address}/{currency}")]
        [SwaggerOperation(nameof(DeletePayInAddress))]
        public async Task<IActionResult> DeletePayInAddress(
            [FromRoute]string address, 
            [FromRoute]CurrencyType currency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            }

            await _addressRepository.DeleteAsync(address, currency);

            await _log.WriteInfoAsync(nameof(AddPayInAddress), $"Address: {address}, Currency: {Enum.GetName(typeof(CurrencyType), currency)}", 
                "Pay-in address deleted");

            return Ok();
        }
    }
}
