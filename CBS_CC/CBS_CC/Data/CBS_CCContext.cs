using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CBS_CC.Models
{
    public class CBS_CCContext : DbContext
    {
        public CBS_CCContext (DbContextOptions<CBS_CCContext> options)
            : base(options)
        {
        }

        public DbSet<CBS_CC.Models.Estudiante> Estudiante { get; set; }
        public DbSet<CBS_CC.Models.EstudianteEstado> EstudianteEstado { get; set; }
        public DbSet<CBS_CC.Models.Asignatura> Asignatura { get; set; }
        public DbSet<CBS_CC.Models.AsignaturaCurso> AsignaturaCurso { get; set; }
        public DbSet<CBS_CC.Models.Curso> Curso { get; set; }
        public DbSet<CBS_CC.Models.Matricula> Matricula { get; set; }
        public DbSet<CBS_CC.Models.MatriculaNota> MatriculaNota { get; set; }
        public DbSet<CBS_CC.Models.Seccion> Seccion { get; set; }

    }
}
