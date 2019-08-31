using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CBS_CC.Models;

namespace CBS_CC.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly CBS_CCContext _context;

        public EstudiantesController(CBS_CCContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var estudiantes = _context.Estudiante.Include(e => e.EstudianteEstado);
            ViewData["EstudianteEstadoId"] = new SelectList(_context.Set<EstudianteEstado>(), "EstudianteEstadoId", "Descripcion");

            var estudianteViewModel = new Models.ViewModels.EstudianteListModel
            {
                f_Nombre = "",
                f_Sexo = "",
                EstudianteEstadoId = -1,
                Estudiantes = await estudiantes.ToListAsync()
            };

            return View(estudianteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string f_Nombre, string f_Sexo, int? EstudianteEstadoId)
        {
            string filtrar_nombre = string.IsNullOrEmpty(f_Nombre) ? "" : f_Nombre;
            string filtrar_sexo = string.IsNullOrEmpty(f_Sexo) ? "" : f_Sexo;
            int estudiante_estadoid = (EstudianteEstadoId == null) ? -1 : (int)EstudianteEstadoId;

            var estudiantes = _context.Estudiante.Where(e => (e.NombreCompleto.ToUpper().Contains(filtrar_nombre.ToUpper()))
                                                        && (e.Sexo.ToUpper().Contains(filtrar_sexo.ToUpper()))
                                                        && (e.EstudianteEstadoId == estudiante_estadoid || estudiante_estadoid == -1));
            ViewData["EstudianteEstadoId"] = new SelectList(_context.Set<EstudianteEstado>(), "EstudianteEstadoId", "Descripcion");

            var estudianteViewModel = new Models.ViewModels.EstudianteListModel
            {
                f_Nombre = filtrar_nombre,
                f_Sexo = filtrar_sexo,
                EstudianteEstadoId = estudiante_estadoid,
                Estudiantes = await estudiantes.ToListAsync()
            };

            return View(estudianteViewModel);
        }

        public async Task<IActionResult> Save(int? id)
        {
            if (id == null)
            {
                ViewData["EstudianteEstadoId"] = new SelectList(_context.Set<EstudianteEstado>(), "EstudianteEstadoId", "Descripcion");
                ViewData["Operacion"] = "Create";
                return View("Save");
            }
            else
            {
                var estudiantes = await _context.Estudiante.Include(e => e.EstudianteEstado).FirstAsync(e => e.EstudianteEstadoId == id);
                ViewData["EstudianteEstadoId"] = new SelectList(_context.Set<EstudianteEstado>(), "EstudianteEstadoId", "Descripcion", estudiantes.EstudianteEstadoId);
                ViewData["Operacion"] = "Edit";
                return View("Save", estudiantes);
            }
        }

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var estudianteToSave = (operacion == "Edit") ? await _context.Estudiante.FindAsync(id) : new Estudiante();

            if (ModelState.IsValid)
            {
                try
                {
                    if (await TryUpdateModelAsync<Estudiante>(estudianteToSave, "",
                            e => e.CarnetNo, e => e.CodigoMINED, e => e.NombreCompleto, e => e.FechaNacimiento,
                            e => e.Edad, e => e.Sexo, e => e.Direccion, e => e.Responsable, e => e.Telefono,
                            e => e.EstudianteEstadoId))
                    {
                        if (operacion == "Create")
                        {
                            _context.Estudiante.Add(estudianteToSave);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists((int)id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteEstadoId"] = new SelectList(_context.Set<EstudianteEstado>(), "EstudianteEstadoId", "Descripcion", estudianteToSave.EstudianteEstadoId);
            return View("Save", estudianteToSave);
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.EstudianteId == id);
        }
    }
}
