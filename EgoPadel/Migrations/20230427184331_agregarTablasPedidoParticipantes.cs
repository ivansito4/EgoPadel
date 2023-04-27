using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgoPadel.Migrations
{
    /// <inheritdoc />
    public partial class agregarTablasPedidoParticipantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioAppId",
                table: "ReservaPista",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<float>(type: "real", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Torneo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<byte>(type: "tinyint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NroPlazas = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Premio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantesEquipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TorneoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantesEquipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantesEquipos_Torneo_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantesIndividual",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TorneoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantesIndividual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantesIndividual_Torneo_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservaPista_UsuarioAppId",
                table: "ReservaPista",
                column: "UsuarioAppId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantesEquipos_TorneoId",
                table: "ParticipantesEquipos",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantesIndividual_TorneoId",
                table: "ParticipantesIndividual",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ProductoId",
                table: "Pedido",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservaPista_AspNetUsers_UsuarioAppId",
                table: "ReservaPista",
                column: "UsuarioAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservaPista_AspNetUsers_UsuarioAppId",
                table: "ReservaPista");

            migrationBuilder.DropTable(
                name: "ParticipantesEquipos");

            migrationBuilder.DropTable(
                name: "ParticipantesIndividual");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Torneo");

            migrationBuilder.DropIndex(
                name: "IX_ReservaPista_UsuarioAppId",
                table: "ReservaPista");

            migrationBuilder.DropColumn(
                name: "UsuarioAppId",
                table: "ReservaPista");
        }
    }
}
