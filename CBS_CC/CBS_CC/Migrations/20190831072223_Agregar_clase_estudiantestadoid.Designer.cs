﻿// <auto-generated />
using System;
using CBS_CC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CBS_CC.Migrations
{
    [DbContext(typeof(CBS_CCContext))]
    [Migration("20190831072223_Agregar_clase_estudiantestadoid")]
    partial class Agregar_clase_estudiantestadoid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CBS_CC.Models.Asignatura", b =>
                {
                    b.Property<int>("AsignaturaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("AsignaturaId");

                    b.ToTable("Asignatura");
                });

            modelBuilder.Entity("CBS_CC.Models.AsignaturaCurso", b =>
                {
                    b.Property<int>("AsignaturaCursoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AsignaturaId");

                    b.Property<int>("CursoId");

                    b.HasKey("AsignaturaCursoId");

                    b.HasIndex("AsignaturaId");

                    b.HasIndex("CursoId");

                    b.ToTable("AsignaturaCurso");
                });

            modelBuilder.Entity("CBS_CC.Models.Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("CursoId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("CBS_CC.Models.Estudiante", b =>
                {
                    b.Property<int>("EstudianteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarnetNo");

                    b.Property<string>("CodigoMINED");

                    b.Property<string>("Direccion")
                        .HasMaxLength(150);

                    b.Property<int>("Edad");

                    b.Property<int>("EstudianteEstadoId");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("NombreCompleto")
                        .HasMaxLength(50);

                    b.Property<string>("Responsable")
                        .HasMaxLength(50);

                    b.Property<string>("Sexo")
                        .HasMaxLength(10);

                    b.Property<string>("Telefono")
                        .HasMaxLength(13);

                    b.HasKey("EstudianteId");

                    b.HasIndex("EstudianteEstadoId");

                    b.ToTable("Estudiante");
                });

            modelBuilder.Entity("CBS_CC.Models.EstudianteEstado", b =>
                {
                    b.Property<int>("EstudianteEstadoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(30);

                    b.HasKey("EstudianteEstadoId");

                    b.ToTable("EstudianteEstado");
                });

            modelBuilder.Entity("CBS_CC.Models.Matricula", b =>
                {
                    b.Property<int>("MatriculaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId");

                    b.Property<int>("EstudianteId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("PeriodoLectivo");

                    b.Property<int>("SeccionId");

                    b.HasKey("MatriculaId");

                    b.HasIndex("CursoId");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("SeccionId");

                    b.ToTable("Matricula");
                });

            modelBuilder.Entity("CBS_CC.Models.MatriculaNota", b =>
                {
                    b.Property<int>("MatriculaNotaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AsignaturaId");

                    b.Property<decimal>("ICorte");

                    b.Property<decimal>("IICorte");

                    b.Property<decimal>("IIICorte");

                    b.Property<decimal>("IISemestre");

                    b.Property<decimal>("ISemestre");

                    b.Property<decimal>("IVCorte");

                    b.Property<int?>("MatriculaId");

                    b.Property<decimal>("NotaFinal");

                    b.HasKey("MatriculaNotaId");

                    b.HasIndex("AsignaturaId");

                    b.HasIndex("MatriculaId");

                    b.ToTable("MatriculaNota");
                });

            modelBuilder.Entity("CBS_CC.Models.Seccion", b =>
                {
                    b.Property<int>("SeccionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("SeccionId");

                    b.ToTable("Seccion");
                });

            modelBuilder.Entity("CBS_CC.Models.AsignaturaCurso", b =>
                {
                    b.HasOne("CBS_CC.Models.Asignatura", "Asignatura")
                        .WithMany()
                        .HasForeignKey("AsignaturaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CBS_CC.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CBS_CC.Models.Estudiante", b =>
                {
                    b.HasOne("CBS_CC.Models.EstudianteEstado", "EstudianteEstado")
                        .WithMany()
                        .HasForeignKey("EstudianteEstadoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CBS_CC.Models.Matricula", b =>
                {
                    b.HasOne("CBS_CC.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CBS_CC.Models.Estudiante", "Estudiante")
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CBS_CC.Models.Seccion", "Seccion")
                        .WithMany()
                        .HasForeignKey("SeccionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CBS_CC.Models.MatriculaNota", b =>
                {
                    b.HasOne("CBS_CC.Models.Asignatura", "Asignatura")
                        .WithMany()
                        .HasForeignKey("AsignaturaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CBS_CC.Models.Matricula", "Matricula")
                        .WithMany()
                        .HasForeignKey("MatriculaId");
                });
#pragma warning restore 612, 618
        }
    }
}
