using CBS_CC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBS_CC.Data
{
    public class DbInitializer
    {
        public static void Initialize(CBS_CCContext context)
        {
            context.Database.EnsureCreated();

            if (context.EstudianteEstado.Any())
            {
                return; //Ya fue inicializada la base de datos.
            }

            if (!context.EstudianteEstado.Any())
            {
                var estudiante_estado = new EstudianteEstado[]
                {
                    new EstudianteEstado { Descripcion = "Activo" },
                    new EstudianteEstado { Descripcion = "Inactivo" },
                    new EstudianteEstado { Descripcion = "Expulsado" }
                };
                foreach (EstudianteEstado ee in estudiante_estado)
                {
                    context.EstudianteEstado.Add(ee);
                }
                context.SaveChanges();
            }

            if (!context.Seccion.Any())
            {
                var seccion = new Seccion[]
                {
                    new Seccion {Descripcion = "7mo A"},
                    new Seccion {Descripcion = "7mo B"},
                    new Seccion {Descripcion = "8vo A"},
                    new Seccion {Descripcion = "9no A"}
                };
                foreach (Seccion s in seccion)
                {
                    context.Seccion.Add(s);
                }
                context.SaveChanges();
            }
        }
    }
}
