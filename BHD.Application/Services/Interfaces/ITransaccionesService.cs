using BHD.Contracts.Transacciones;

namespace BHD.Application.Services.Interfaces
{
    public interface ITransaccionesService
    {
        Task<List<TransaccionDto>> GetByCuentaIdAsync(int cuentaId);
        Task TransferirAsync(TransferenciaDto dto);
    }
}
