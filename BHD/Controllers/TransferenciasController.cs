using BHD.Application.Transacciones.Commands;
using BHD.Contracts.Transacciones;
using Microsoft.AspNetCore.Mvc;

namespace BHD.Controllers
{
    public class TransferenciasController : ApiControllerBase
    {
        [HttpPost("transferir")]
        public async Task<bool> Transferir([FromBody] TransferenciaDto dto)
        {
            await Mediator.Send(new CreateTransferenciaCommand(dto));
            return true;
        }
    }
}
