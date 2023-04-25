using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgoPadel.Migrations
{
    /// <inheritdoc />
    public partial class masTablasBBDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservaPista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PistaId = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaPista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservaPista_Pista_PistaId",
                        column: x => x.PistaId,
                        principalTable: "Pista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservaPista_PistaId",
                table: "ReservaPista",
                column: "PistaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservaPista");
        }
    }
}
