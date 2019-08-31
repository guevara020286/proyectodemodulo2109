using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBS_CC.Migrations
{
    public partial class Agregar_clase_estudiantestadoid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Estudiante");

            migrationBuilder.AddColumn<int>(
                name: "EstudianteEstadoId",
                table: "Estudiante",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Asignatura",
                columns: table => new
                {
                    AsignaturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignatura", x => x.AsignaturaId);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoId);
                });

            migrationBuilder.CreateTable(
                name: "EstudianteEstado",
                columns: table => new
                {
                    EstudianteEstadoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstudianteEstado", x => x.EstudianteEstadoId);
                });

            migrationBuilder.CreateTable(
                name: "Seccion",
                columns: table => new
                {
                    SeccionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seccion", x => x.SeccionId);
                });

            migrationBuilder.CreateTable(
                name: "AsignaturaCurso",
                columns: table => new
                {
                    AsignaturaCursoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AsignaturaId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignaturaCurso", x => x.AsignaturaCursoId);
                    table.ForeignKey(
                        name: "FK_AsignaturaCurso_Asignatura_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignatura",
                        principalColumn: "AsignaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignaturaCurso_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    MatriculaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    PeriodoLectivo = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    SeccionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.MatriculaId);
                    table.ForeignKey(
                        name: "FK_Matricula_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matricula_Estudiante_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiante",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matricula_Seccion_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Seccion",
                        principalColumn: "SeccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatriculaNota",
                columns: table => new
                {
                    MatriculaNotaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AsignaturaId = table.Column<int>(nullable: false),
                    ICorte = table.Column<decimal>(nullable: false),
                    IICorte = table.Column<decimal>(nullable: false),
                    ISemestre = table.Column<decimal>(nullable: false),
                    IIICorte = table.Column<decimal>(nullable: false),
                    IVCorte = table.Column<decimal>(nullable: false),
                    IISemestre = table.Column<decimal>(nullable: false),
                    NotaFinal = table.Column<decimal>(nullable: false),
                    MatriculaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculaNota", x => x.MatriculaNotaId);
                    table.ForeignKey(
                        name: "FK_MatriculaNota_Asignatura_AsignaturaId",
                        column: x => x.AsignaturaId,
                        principalTable: "Asignatura",
                        principalColumn: "AsignaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatriculaNota_Matricula_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matricula",
                        principalColumn: "MatriculaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_EstudianteEstadoId",
                table: "Estudiante",
                column: "EstudianteEstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignaturaCurso_AsignaturaId",
                table: "AsignaturaCurso",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignaturaCurso_CursoId",
                table: "AsignaturaCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_CursoId",
                table: "Matricula",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_EstudianteId",
                table: "Matricula",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_SeccionId",
                table: "Matricula",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaNota_AsignaturaId",
                table: "MatriculaNota",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaNota_MatriculaId",
                table: "MatriculaNota",
                column: "MatriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiante_EstudianteEstado_EstudianteEstadoId",
                table: "Estudiante",
                column: "EstudianteEstadoId",
                principalTable: "EstudianteEstado",
                principalColumn: "EstudianteEstadoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiante_EstudianteEstado_EstudianteEstadoId",
                table: "Estudiante");

            migrationBuilder.DropTable(
                name: "AsignaturaCurso");

            migrationBuilder.DropTable(
                name: "EstudianteEstado");

            migrationBuilder.DropTable(
                name: "MatriculaNota");

            migrationBuilder.DropTable(
                name: "Asignatura");

            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Seccion");

            migrationBuilder.DropIndex(
                name: "IX_Estudiante_EstudianteEstadoId",
                table: "Estudiante");

            migrationBuilder.DropColumn(
                name: "EstudianteEstadoId",
                table: "Estudiante");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Estudiante",
                maxLength: 20,
                nullable: true);
        }
    }
}
