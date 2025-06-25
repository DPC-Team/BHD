using BHD.Contracts.Cuentas;

namespace BHD.Application.Services.Interfaces
{
    public interface ICuentasService
    {
        Task<List<CuentaDto>> GetAllAsync();
    }
}
