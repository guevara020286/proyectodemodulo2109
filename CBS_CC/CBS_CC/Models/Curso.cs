using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models
{
    public class Curso
    {
        public int CursoId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "La longitud máxima es de 30 caracteres")]
        public string Descripcion { get; set; }
    }
}
