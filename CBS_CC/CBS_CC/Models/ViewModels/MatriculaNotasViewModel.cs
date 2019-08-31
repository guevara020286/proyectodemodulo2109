using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models.ViewModels
{
    public class MatriculaNotasViewModel
    {
        public List<MatriculaNota> MatriculaNotas { get; set; }
        public int f_PeriodoLectivo { get; set; }
        public int f_SeccionId { get; set; }
        public int f_AsignaturaId { get; set; }
    }
}
