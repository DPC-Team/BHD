using BHD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BHD.Application.Common.Interfaces
{
    public interface IBHDDbContext : IDbContext
    {
        DbSet<Cuenta> Cuentas { get; set; }
        DbSet<Transaccione> Transacciones { get; set; }
    }
}
