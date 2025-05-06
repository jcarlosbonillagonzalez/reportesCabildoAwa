using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportesCabildoAwa.Data.Migrations
{
    /// <inheritdoc />
    public partial class _03052025RelacionPersonaTipoDocumento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoDocumentoIdTipoDocumento",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TipoDocumentoIdTipoDocumento",
                table: "Personas",
                column: "TipoDocumentoIdTipoDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_TipoDocumentos_TipoDocumentoIdTipoDocumento",
                table: "Personas",
                column: "TipoDocumentoIdTipoDocumento",
                principalTable: "TipoDocumentos",
                principalColumn: "IdTipoDocumento",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TipoDocumentos_TipoDocumentoIdTipoDocumento",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_TipoDocumentoIdTipoDocumento",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "TipoDocumentoIdTipoDocumento",
                table: "Personas");
        }
    }
}
