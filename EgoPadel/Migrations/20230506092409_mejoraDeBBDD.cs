using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgoPadel.Migrations
{
    /// <inheritdoc />
    public partial class mejoraDeBBDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Producto_ProductoId",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservaPista_AspNetUsers_UsuarioAppId",
                table: "ReservaPista");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_ProductoId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "PrecioTotal",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Pedido");

            migrationBuilder.RenameColumn(
                name: "UsuarioAppId",
                table: "ReservaPista",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservaPista_UsuarioAppId",
                table: "ReservaPista",
                newName: "IX_ReservaPista_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Pedido",
                newName: "FechaOrden");

            migrationBuilder.AlterColumn<string>(
                name: "Premio",
                table: "Torneo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Torneo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAplicacionId",
                table: "Pedido",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "ParticipantesIndividual",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EquipoId",
                table: "ParticipantesEquipos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_UsuarioAplicacionId",
                table: "Pedido",
                column: "UsuarioAplicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantesIndividual_UsuarioId",
                table: "ParticipantesIndividual",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantesEquipos_EquipoId",
                table: "ParticipantesEquipos",
                column: "EquipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantesEquipos_Equipo_EquipoId",
                table: "ParticipantesEquipos",
                column: "EquipoId",
                principalTable: "Equipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantesIndividual_AspNetUsers_UsuarioId",
                table: "ParticipantesIndividual",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_AspNetUsers_UsuarioAplicacionId",
                table: "Pedido",
                column: "UsuarioAplicacionId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservaPista_AspNetUsers_UsuarioId",
                table: "ReservaPista",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantesEquipos_Equipo_EquipoId",
                table: "ParticipantesEquipos");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantesIndividual_AspNetUsers_UsuarioId",
                table: "ParticipantesIndividual");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_AspNetUsers_UsuarioAplicacionId",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservaPista_AspNetUsers_UsuarioId",
                table: "ReservaPista");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_UsuarioAplicacionId",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantesIndividual_UsuarioId",
                table: "ParticipantesIndividual");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantesEquipos_EquipoId",
                table: "ParticipantesEquipos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "UsuarioAplicacionId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ParticipantesIndividual");

            migrationBuilder.DropColumn(
                name: "EquipoId",
                table: "ParticipantesEquipos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "ReservaPista",
                newName: "UsuarioAppId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservaPista_UsuarioId",
                table: "ReservaPista",
                newName: "IX_ReservaPista_UsuarioAppId");

            migrationBuilder.RenameColumn(
                name: "FechaOrden",
                table: "Pedido",
                newName: "Fecha");

            migrationBuilder.AlterColumn<string>(
                name: "Premio",
                table: "Torneo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Torneo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PrecioTotal",
                table: "Pedido",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ProductoId",
                table: "Pedido",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Producto_ProductoId",
                table: "Pedido",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservaPista_AspNetUsers_UsuarioAppId",
                table: "ReservaPista",
                column: "UsuarioAppId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
