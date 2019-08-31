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
    public class MatriculaNotasController : Controller
    {
        private readonly CBS_CCContext _context;

        public MatriculaNotasController(CBS_CCContext context)
        {
            _context = context;
        }

        // GET: MatriculaNotas
        public IActionResult Index()
        {
            ViewData["SeccionId"] = new SelectList(_context.Set<Seccion>(), "SeccionId", "Descripcion");
            ViewData["AsignaturaId"] = new SelectList(_context.Set<Asignatura>(), "AsignaturaId", "Descripcion");
            var notas = new Models.ViewModels.MatriculaNotasViewModel
            {
                f_AsignaturaId = 0,
                f_PeriodoLectivo = DateTime.Now.Year,
                f_SeccionId = 0,
                MatriculaNotas = new List<MatriculaNota>()
            };

            return View(notas);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? f_PeriodoLectivo, int? f_AsignaturaId, int? f_SeccionId)
        {
            int asignatura_id = (f_AsignaturaId == null) ? 0 : (int)f_AsignaturaId;
            int seccion_id = (f_SeccionId == null) ? 0 : (int)f_SeccionId;
            int periodo = (f_PeriodoLectivo == null) ? 0 : (int)f_PeriodoLectivo;

            var estudiantes = _context.Matricula.Where(e => e.PeriodoLectivo == f_PeriodoLectivo
                                                        && e.SeccionId == f_SeccionId);

            var notas = _context.MatriculaNota.Include(m => m.Matricula)
                            .Where(m => m.AsignaturaId == f_AsignaturaId
                                    && m.Matricula.PeriodoLectivo == f_PeriodoLectivo
                                    && m.Matricula.SeccionId == f_SeccionId);

            bool guardar = false;
            foreach (var est in estudiantes)
            {
                if (notas.FirstOrDefault(n => n.Matricula.EstudianteId == est.EstudianteId) == null)
                {
                    var nota_nueva = new MatriculaNota() { AsignaturaId = asignatura_id, ICorte = 0, IICorte = 0,
                                                IIICorte = 0, IVCorte = 0, ISemestre = 0, IISemestre = 0, NotaFinal = 0,
                                                MatriculaId = est.MatriculaId };
                    _context.MatriculaNota.Add(nota_nueva);
                    guardar = true;
                }
            }
            if (guardar)
            {
                await _context.SaveChangesAsync();
            }

            var notas_generadas = _context.MatriculaNota
                                    .Include(m => m.Asignatura)
                                    .Include(m => m.Matricula)
                                        .ThenInclude(e => e.Estudiante)
                                    .Where(m => m.AsignaturaId == f_AsignaturaId
                                        && m.Matricula.PeriodoLectivo == f_PeriodoLectivo
                                        && m.Matricula.SeccionId == f_SeccionId);

            ViewData["SeccionId"] = new SelectList(_context.Set<Seccion>(), "SeccionId", "Descripcion");
            ViewData["AsignaturaId"] = new SelectList(_context.Set<Asignatura>(), "AsignaturaId", "Descripcion");
            var notasVModel = new Models.ViewModels.MatriculaNotasViewModel
            {
                f_AsignaturaId = asignatura_id,
                f_PeriodoLectivo = periodo,
                f_SeccionId = seccion_id,
                MatriculaNotas = await notas_generadas.ToListAsync()
            };
            return View(notasVModel);
        }

        public async Task<IActionResult> Save(int? id)
        {
            if (id == null)
            {
                ViewData["SeccionId"] = new SelectList(_context.Set<Seccion>(), "SeccionId", "Descripcion");
                ViewData["AsignaturaId"] = new SelectList(_context.Set<Asignatura>(), "AsignaturaId", "Descripcion");
                ViewData["Operacion"] = "Create";
                return PartialView("_SavePartialView");
            }
            else
            {
                var notas = await _context.MatriculaNota.Include(e => e.Matricula).ThenInclude(m => m.Estudiante).FirstAsync(n => n.MatriculaNotaId == id);
                ViewData["SeccionId"] = new SelectList(_context.Set<Seccion>(), "SeccionId", "Descripcion", notas.Matricula.SeccionId);
                ViewData["AsignaturaId"] = new SelectList(_context.Set<Asignatura>(), "AsignaturaId", "Descripcion", notas.AsignaturaId);
                ViewData["Operacion"] = "Edit";
                return PartialView("_SavePartialView", notas);
            }
        }

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var notasToSave = (operacion == "Edit") ? await _context.MatriculaNota.FindAsync(id) : new MatriculaNota();

            if (ModelState.IsValid)
            {
                try
                {
                    if (await TryUpdateModelAsync<MatriculaNota>(notasToSave, "",
                            e => e.AsignaturaId, e => e.MatriculaId, e => e.ICorte, e => e.IICorte,
                            e => e.ISemestre, e => e.IIICorte, e => e.IVCorte, e => e.IISemestre, 
                            e => e.NotaFinal ))
                    {
                        if (operacion == "Create")
                        {
                            _context.MatriculaNota.Add(notasToSave);
                        }
                        notasToSave.ISemestre = Math.Round((notasToSave.ICorte + notasToSave.IICorte) / 2, 2);
                        notasToSave.IISemestre = Math.Round((notasToSave.IIICorte + notasToSave.IVCorte) / 2, 2);
                        notasToSave.NotaFinal = Math.Round((notasToSave.ISemestre + notasToSave.IISemestre) / 2, 2);

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaNotaExists((int)id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Url.Action("Index", "MatriculaNotas"));
            }
            var notas = await _context.MatriculaNota.Include(e => e.Matricula).ThenInclude(m => m.Estudiante).FirstAsync(n => n.MatriculaNotaId == notasToSave.MatriculaNotaId);
            ViewData["SeccionId"] = new SelectList(_context.Set<Seccion>(), "SeccionId", "Descripcion", notas.Matricula.SeccionId);
            ViewData["AsignaturaId"] = new SelectList(_context.Set<Asignatura>(), "AsignaturaId", "Descripcion", notas.AsignaturaId);
            return View("_SavePartialView", notasToSave);
        }

        private bool MatriculaNotaExists(int id)
        {
            return _context.MatriculaNota.Any(e => e.MatriculaNotaId == id);
        }
    }
}
