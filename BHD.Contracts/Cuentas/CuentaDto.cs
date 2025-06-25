using BHD.Domain.Entities;
using BHD.Helpers.Mappings;

namespace BHD.Contracts.Cuentas
{
    public class CuentaDto : IMapFrom<Cuenta>
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = default!;
        public string NombreCliente { get; set; } = default!;
        public decimal Balance { get; set; }
    }
}
