using System;
using System.Collections.Generic;

namespace BHD.Domain.Entities
{
    public partial class Transaccione
    {
        public Guid Id { get; set; }
        public int OrigenCuentaId { get; set; }
        public int DestinoCuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal Monto { get; set; }

        public virtual Cuenta DestinoCuenta { get; set; } = null!;
        public virtual Cuenta OrigenCuenta { get; set; } = null!;
    }
}
