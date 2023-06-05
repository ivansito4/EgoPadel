using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgoPadel.Migrations
{
    /// <inheritdoc />
    public partial class cambiarPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "Pedido",
                newName: "TransaccionId");

            migrationBuilder.AddColumn<string>(
                name: "EstadoVenta",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PrecioTotal",
                table: "Pedido",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "BuscarVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuscarVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuscarVM_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuscarVM_Equipo_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuscarVM_EquipoId",
                table: "BuscarVM",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_BuscarVM_UserId",
                table: "BuscarVM",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuscarVM");

            migrationBuilder.DropColumn(
                name: "EstadoVenta",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "PrecioTotal",
                table: "Pedido");

            migrationBuilder.RenameColumn(
                name: "TransaccionId",
                table: "Pedido",
                newName: "NombreCompleto");
        }
    }
}
