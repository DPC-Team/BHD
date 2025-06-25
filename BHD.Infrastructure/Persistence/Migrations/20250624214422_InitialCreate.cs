using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHD.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                              .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCuenta = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    NombreCliente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Activa = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Currency = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    OrigenCuentaId = table.Column<int>(type: "int", nullable: false),
                    DestinoCuentaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(13,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacciones_Cuentas",
                        column: x => x.OrigenCuentaId,
                        principalTable: "Cuentas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transacciones_Cuentas1",
                        column: x => x.DestinoCuentaId,
                        principalTable: "Cuentas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_DestinoCuentaId",
                table: "Transacciones",
                column: "DestinoCuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_OrigenCuentaId",
                table: "Transacciones",
                column: "OrigenCuentaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "Cuentas");
        }
    }
}
