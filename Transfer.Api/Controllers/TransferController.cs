using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Transfer.Domain.Entities;
using Transfer.Domain.Interfaces;
using Transfer.Domain.ViewModel;

namespace Transfer.Api.Controllers
{
    [Route("api/fund-transfer")]
    [ApiController]
    public class TransferController : Controller
    {

        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //colcoar dto 
        public ActionResult<TransferStatus> GetLogs()
        {
            var _logs = _transferService.GetLogs();

            if (_logs != null)
            {
                return Ok(_logs);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        //colocar para dto  param e retorno 
        [HttpPost]
        public ActionResult<ClientTransferViewModel> Post([FromBody] ClientTransfer transfer)
        {
           
            var _transfer = _transferService.Post(transfer);

            if (_transfer != null)
            {
                return Ok(_transfer);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //colocar para dto 
        public ActionResult<TransferStatus> GetByTransactionId(Guid transactionId)
        {

            var _transfer = _transferService.GetByTransactionId(transactionId);

            if (_transfer != null)
            {

                return Ok(_transfer);
            }
            else
            {
                return NoContent();
            }
        }

    }
}
