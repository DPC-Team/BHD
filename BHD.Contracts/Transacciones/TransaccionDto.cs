using AutoMapper;
using BHD.Domain.Entities;
using BHD.Helpers.Mappings;

namespace BHD.Contracts.Transacciones
{
    public class TransaccionDto : IMapFrom<Transaccione>
    {
        public string CuentaOrigen { get; set; } = default!;
        public string CuentaDestino { get; set; } = default!;
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } = default!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Transaccione, TransaccionDto>()
                .ForMember(d => d.CuentaDestino, exp => exp.MapFrom(s => s.DestinoCuenta.NumeroCuenta))
                .ForMember(d => d.CuentaOrigen, exp => exp.MapFrom(s => s.OrigenCuenta.NumeroCuenta));
        }
    }
}
