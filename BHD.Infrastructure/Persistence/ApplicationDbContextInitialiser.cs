using BHD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHD.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly BHDContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
            BHDContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    await _context.Database.MigrateAsync();
                    _logger.LogInformation("Migraciones aplicadas correctamente.");
                }
                else
                {
                    _logger.LogInformation("No hay migraciones pendientes.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            await TrySeedUsersAsync();
        }

        private async Task TrySeedUsersAsync()
        {
            try
            {
                if (!_context.Cuentas.Any())
                {
                    var cuentas = new List<Cuenta>
                    {
                        new Cuenta
                        {
                            NumeroCuenta = "3042161001",
                            NombreCliente = "Juan Pérez",
                            Balance = 10000.00m,
                            Currency = "DOP",
                        },
                        new Cuenta
                        {
                            NumeroCuenta = "3042161002",
                            NombreCliente = "Ana Rodríguez",
                            Balance = 10000.00m,
                            Currency = "DOP",
                        },
                        new Cuenta
                        {
                            NumeroCuenta = "3042161003",
                            NombreCliente = "Carlos Gómez",
                            Balance = 10000.00m,
                            Currency = "DOP",
                        },
                        new Cuenta
                        {
                            NumeroCuenta = "3042161004",
                            NombreCliente = "Darwin Perdomo",
                            Balance = 10000.00m,
                            Currency = "DOP",
                        },
                        new Cuenta
                        {
                            NumeroCuenta = "3042161005",
                            NombreCliente = "Pedro Perez",
                            Balance = 10000.00m,
                            Currency = "DOP",
                        },
                        new Cuenta
                        {
                            NumeroCuenta = "3042161006",
                            NombreCliente = "Maria Sanchez",
                            Balance = 10000.00m,
                            Currency = "DOP",
                        },
                        new Cuenta
                        {
                            NumeroCuenta = "3042161007",
                            NombreCliente = "Pablo Duarte",
                            Balance = 10000.00m,
                            Currency = "DOP",
                        }
                    };

                    await _context.Cuentas.AddRangeAsync(cuentas);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding users.");
            }
        }
    }
}
