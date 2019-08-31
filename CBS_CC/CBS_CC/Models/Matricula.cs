using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models
{
    public class Matricula
    {
        public int MatriculaId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Display(Name ="Año lectivo")]
        public int PeriodoLectivo { get; set; }

        [Display(Name = "Estudiante")]
        public int EstudianteId { get; set; }

        [Display(Name = "Año academico")]
        public int CursoId { get; set; }

        [Display(Name = "Seccion")]
        public int SeccionId { get; set; } 

        public Estudiante Estudiante { get; set; }
        public Curso Curso { get; set; }
        public Seccion Seccion { get; set; }
    }
}
