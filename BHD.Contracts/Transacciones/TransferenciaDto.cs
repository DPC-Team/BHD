using BHD.Domain.Entities;
using BHD.Helpers.Mappings;

namespace BHD.Contracts.Transacciones
{
    public class TransferenciaDto
    {
        public int CuentaOrigen { get; set; }
        public int CuentaDestino { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } = default!;
    }
}
