using BHD.Application.Cuentas.Queries;
using BHD.Contracts.Cuentas;
using Microsoft.AspNetCore.Mvc;

namespace BHD.Controllers
{
    public class CuentasController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<CuentaDto>> GetAllCuentas()
        {
            return await Mediator.Send(new GetAllCuentasQuery());
        }

        [HttpGet("{cuentaId}/transacciones")]
        public async Task<IActionResult> GetTransacciones(int cuentaId)
        {
            var result = await Mediator.Send(new GetTransaccionesByCuentaIdQuery(cuentaId));
            return Ok(result);
        }
    }
}
