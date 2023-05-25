using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgoPadel.Migrations
{
    /// <inheritdoc />
    public partial class modalidadTorneo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Modalidad",
                table: "Torneo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modalidad",
                table: "Torneo");
        }
    }
}
