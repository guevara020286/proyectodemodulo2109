using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models
{
    public class AsignaturaCurso
    {
        public int AsignaturaCursoId { get; set; }

        [Display(Name = ("Asignatura"))]
        public int AsignaturaId { get; set; }

        public int CursoId { get; set; }

        public Asignatura Asignatura { get; set; }
        public Curso Curso { get; set; }
    }
}
