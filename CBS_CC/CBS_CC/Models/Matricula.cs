using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models
{
    public class Matricula
    {
        public int MatriculaId { get; set; }
        public DateTime Fecha { get; set; }
        public int PeriodoLectivo { get; set; }
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public int SeccionId { get; set; } 

        public Estudiante Estudiante { get; set; }
        public Curso Curso { get; set; }
        public Seccion Seccion { get; set; }
    }
}
