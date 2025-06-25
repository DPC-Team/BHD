using System;
using BHD.Application.Common.Interfaces;
using BHD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BHD.Infrastructure
{
    public partial class BHDContext : DbContext, IBHDDbContext
    {
        public BHDContext()
        {
        }

        public BHDContext(DbContextOptions<BHDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cuenta> Cuentas { get; set; } = null!;
        public virtual DbSet<Transaccione> Transacciones { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Activa)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Balance).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCuenta)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Transaccione>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Monto).HasColumnType("decimal(13, 2)");

                entity.HasOne(d => d.DestinoCuenta)
                    .WithMany(p => p.TransaccioneDestinoCuenta)
                    .HasForeignKey(d => d.DestinoCuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transacciones_Cuentas1");

                entity.HasOne(d => d.OrigenCuenta)
                    .WithMany(p => p.TransaccioneOrigenCuenta)
                    .HasForeignKey(d => d.OrigenCuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transacciones_Cuentas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
