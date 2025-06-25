using BHD.Application.Common.Interfaces;
using BHD.Application.Services.Interfaces;
using BHD.Contracts.Transacciones;
using BHD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BHD.Application.Services
{
    public class TransaccionesService : ITransaccionesService
    {
        private readonly IBHDDbContext _context;
        private readonly IMapper _mapper;

        public TransaccionesService(IBHDDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TransaccionDto>> GetByCuentaIdAsync(int cuentaId)
        {
            var transacciones = await _context.Transacciones
                .Where(t => t.OrigenCuentaId == cuentaId || t.DestinoCuentaId == cuentaId)
                .OrderByDescending(t => t.Fecha)
                .ToListAsync();

            return _mapper.Map<List<TransaccionDto>>(transacciones);
        }

        public async Task TransferirAsync(TransferenciaDto dto)
        {
            if (dto.CuentaOrigen == dto.CuentaDestino)
                throw new ArgumentException("La cuenta destino no puede ser igual a la cuenta origen.");

            if (dto.Monto <= 0)
                throw new ArgumentException("El monto debe ser mayor que cero.");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            var origen = await _context.Cuentas.FindAsync(dto.CuentaOrigen);
            var destino = await _context.Cuentas.FindAsync(dto.CuentaDestino);

            if (origen is null || destino is null)
                throw new KeyNotFoundException("Cuenta origen o destino no encontrada.");

            if (origen.Balance < dto.Monto)
                throw new InvalidOperationException("Saldo insuficiente en la cuenta origen.");

            origen.Balance -= dto.Monto;
            destino.Balance += dto.Monto;

            var transaccion = new Transaccione
            {
                OrigenCuentaId = dto.CuentaOrigen,
                DestinoCuentaId = dto.CuentaDestino,
                Monto = dto.Monto,
                Descripcion = dto.Descripcion
            };

            _context.Transacciones.Add(transaccion);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}
