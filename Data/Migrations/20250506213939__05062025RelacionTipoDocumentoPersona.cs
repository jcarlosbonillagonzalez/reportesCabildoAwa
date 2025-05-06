using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportesCabildoAwa.Data.Migrations
{
    public partial class _05062025RelacionTipoDocumentoPersona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Paso 1: Agrega la columna como nullable temporalmente
            migrationBuilder.AddColumn<int>(
                name: "IdTipoDocumento",
                table: "Personas",
                type: "int",
                nullable: true);

            // Paso 2: Asigna valor por defecto (1) a registros existentes
            migrationBuilder.Sql("UPDATE Personas SET IdTipoDocumento = 1");

            // Paso 3: Cambia la columna a no nullable
            migrationBuilder.AlterColumn<int>(
                name: "IdTipoDocumento",
                table: "Personas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Paso 4: Crea el índice
            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdTipoDocumento",
                table: "Personas",
                column: "IdTipoDocumento");

            // Paso 5: Agrega la clave foránea
            migrationBuilder.AddForeignKey(
                name: "FK_Personas_TipoDocumentos_IdTipoDocumento",
                table: "Personas",
                column: "IdTipoDocumento",
                principalTable: "TipoDocumentos",
                principalColumn: "IdTipoDocumento",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TipoDocumentos_IdTipoDocumento",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_IdTipoDocumento",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "IdTipoDocumento",
                table: "Personas");
        }
    }
}
