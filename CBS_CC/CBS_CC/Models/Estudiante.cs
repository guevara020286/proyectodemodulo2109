using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        public string CarnetNo { get; set; }
        public string CodigoMINED { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public string Responsable { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }
    }
}
