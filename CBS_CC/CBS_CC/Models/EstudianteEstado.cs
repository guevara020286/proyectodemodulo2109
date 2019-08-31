using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models
{
    public class EstudianteEstado
    {
        public int EstudianteEstadoId { get; set; }

        [StringLength(30)]
        public string Descripcion { get; set; }
    }
}
