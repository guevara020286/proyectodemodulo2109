using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models
{
    public class MatriculaNota
    {
        public int MatriculaNotaId { get; set; }
        public int AsignaturaId { get; set; }
        public Decimal ICorte { get; set; }
        public Decimal IICorte { get; set; }
        public Decimal ISemestre { get; set; }
        public Decimal IIICorte { get; set; }
        public Decimal IVCorte { get; set; }
        public Decimal IISemestre { get; set; }
        public Decimal NotaFinal { get; set; }

        public Matricula Matricula { get; set; }
        public Asignatura Asignatura { get; set; }
    }
}
