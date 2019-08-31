using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models.ViewModels
{
    public class EstudianteListModel
    {
        public List<Estudiante> Estudiantes { get; set; }
        public int EstudianteEstadoId { get; set; }
        public string f_Sexo { get; set; }
        public string f_Nombre { get; set; }
    }
}
