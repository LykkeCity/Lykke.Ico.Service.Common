using System.Threading.Tasks;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.ApiLibrary.Contract;
using Lykke.Service.IcoCommon.Core.Services;
using Lykke.Service.IcoCommon.Models.Tx;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.IcoCommon.Controllers
{
    [Route("/api/tx")]
    public class TxController : Controller
    {
        private ITransactionService _transactionService;

        public TxController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> HandleTransactions([FromBody]TransactionModel[] transactions)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorResponseFactory.Create(ModelState));
            else
                return Ok(await _transactionService.HandleTransactions(transactions));
        }
    }
}
