using Microsoft.EntityFrameworkCore.Migrations;

namespace CBS_CC.Migrations
{
    public partial class Agregandocampomatriculaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatriculaNota_Matricula_MatriculaId",
                table: "MatriculaNota");

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaId",
                table: "MatriculaNota",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MatriculaNota_Matricula_MatriculaId",
                table: "MatriculaNota",
                column: "MatriculaId",
                principalTable: "Matricula",
                principalColumn: "MatriculaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatriculaNota_Matricula_MatriculaId",
                table: "MatriculaNota");

            migrationBuilder.AlterColumn<int>(
                name: "MatriculaId",
                table: "MatriculaNota",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MatriculaNota_Matricula_MatriculaId",
                table: "MatriculaNota",
                column: "MatriculaId",
                principalTable: "Matricula",
                principalColumn: "MatriculaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
