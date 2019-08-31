using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Models
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }

        [Display(Name = "# Carnet")]
        public string CarnetNo { get; set; }

        public string CodigoMINED { get; set; }

        [StringLength(50, ErrorMessage ="El nombre debe contenedor menos de 100 caracteres")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }

        [StringLength(10, ErrorMessage = "El sexo debe tener menos de 10 caracteres.")]
        public string Sexo { get; set; }

        [StringLength(150, ErrorMessage = "La dirección debe contener menos de 150 caracteres")]
        public string Direccion { get; set; }

        [StringLength(50, ErrorMessage = "El nombre del responsable debe contener menos de 50 caracteres")]
        public string Responsable { get; set; }

        [StringLength(13, ErrorMessage = "El telefono debe contener menos de 13 caracteres")]
        public string Telefono { get; set; }

        [Display(Name ="Estado")]
        public int EstudianteEstadoId { get; set; }

        public string EstudianteCombo { get { return $"{CarnetNo} - {NombreCompleto}"; } }

        public EstudianteEstado EstudianteEstado { get; set; }
    }
}
