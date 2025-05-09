using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportesCabildoAwa.Data.Migrations
{
    /// <inheritdoc />
    public partial class _05062025CampoEstadoPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstadoPersona",
                table: "Personas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoPersona",
                table: "Personas");
        }
    }
}
