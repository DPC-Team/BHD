namespace BHD.Domain.Entities
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            TransaccioneDestinoCuenta = new HashSet<Transaccione>();
            TransaccioneOrigenCuenta = new HashSet<Transaccione>();
        }

        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public decimal Balance { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool? Activa { get; set; }
        public string Currency { get; set; } = null!;

        public virtual ICollection<Transaccione> TransaccioneDestinoCuenta { get; set; }
        public virtual ICollection<Transaccione> TransaccioneOrigenCuenta { get; set; }
    }
}
