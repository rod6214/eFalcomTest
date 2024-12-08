using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroDistribucion.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Pallets",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProducto = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fila = table.Column<int>(type: "int", nullable: false),
                    Columna = table.Column<int>(type: "int", nullable: false),
                    Ocupado = table.Column<bool>(type: "bit", nullable: false),
                    PalletId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicaciones_Pallets_PalletId",
                        column: x => x.PalletId,
                        principalSchema: "dbo",
                        principalTable: "Pallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UbicacionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalSchema: "dbo",
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_UbicacionId",
                schema: "dbo",
                table: "Movimientos",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_CodigoProducto",
                schema: "dbo",
                table: "Pallets",
                column: "CodigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_PalletId",
                schema: "dbo",
                table: "Ubicaciones",
                column: "PalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Ubicaciones",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pallets",
                schema: "dbo");
        }
    }
}
