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
    public class MatriculasController : Controller
    {
        private readonly CBS_CCContext _context;

        public MatriculasController(CBS_CCContext context)
        {
            _context = context;
        }

        // GET: Matriculas
        public async Task<IActionResult> Index()
        {
            var cBS_CCContext = _context.Matricula.Include(m => m.Curso).Include(m => m.Estudiante).Include(m => m.Seccion);
            return View(await cBS_CCContext.ToListAsync());
        }


        public async Task<IActionResult> Save(int? id)
        {
            if (id == null)
            {
                ViewData["Operacion"] = "Create";
                ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Descripcion");
                ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "EstudianteId", "EstudianteCombo");
                ViewData["SeccionId"] = new SelectList(_context.Seccion, "SeccionId", "Descripcion");
                return View("Save");
            }
            else
            {
                var matricula = await _context.Matricula.FirstAsync(m => m.MatriculaId == id);
                ViewData["Operacion"] = "Edit";
                ViewData["CursoId"] = new SelectList(_context.Curso, "CursoId", "Descripcion", matricula.CursoId);
                ViewData["EstudianteId"] = new SelectList(_context.Estudiante, "EstudianteId", "EstudianteCombo", matricula.EstudianteId);
                ViewData["SeccionId"] = new SelectList(_context.Seccion, "SeccionId", "Descripcion", matricula.SeccionId);
                return View("Save", matricula);
            }
        }

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var matriculaToSave = (operacion == "Edit") ? await _context.Matricula.FindAsync(id) : new Matricula();

            if (ModelState.IsValid)
            {
                try
                {
                    if (await TryUpdateModelAsync<Matricula>(matriculaToSave, "", 
                            m => m.Fecha, m => m.PeriodoLectivo, m => m.EstudianteId,
                            m => m.CursoId, m => m.SeccionId))
                    {
                        if (operacion == "Create")
                        {
                            _context.Matricula.Add(matriculaToSave);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists((int)id))
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
            return PartialView("Save", matriculaToSave);
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matricula.Any(e => e.MatriculaId == id);
        }
    }
}
