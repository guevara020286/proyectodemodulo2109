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
    public class CursosController : Controller
    {
        private readonly CBS_CCContext _context;

        public CursosController(CBS_CCContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Curso.ToListAsync());
        }

        public async Task<IActionResult> Save(int? id)
        {
            if (id == null)
            {
                ViewData["Operacion"] = "Create";
                return PartialView("_SavePartialView");
            }
            else
            {
                var cursos = await _context.Curso.FirstAsync(a => a.CursoId == id);
                ViewData["Operacion"] = "Edit";
                return PartialView("_SavePartialView", cursos);
            }
        }

        [HttpPost, ActionName("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SavePost(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var cursoToSave = (operacion == "Edit") ? await _context.Curso.FindAsync(id) : new Curso();

            if (ModelState.IsValid)
            {
                try
                {
                    if (await TryUpdateModelAsync<Curso>(cursoToSave, "", a => a.Descripcion))
                    {
                        if (operacion == "Create")
                        {
                            _context.Curso.Add(cursoToSave);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists((int)id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Url.Action("Index", "Cursos"));
            }
            return PartialView("_SavePartialView", cursoToSave);
        }

        public async Task<IActionResult> SetAsignaturas(int? id)
        {
            ViewData["DescripcionCurso"] = _context.Curso.Find(id).Descripcion;
            ViewData["_CursoId"] = id;
            var cursosA = await _context.AsignaturaCurso.Include(a => a.Curso).Include(a => a.Asignatura)
                                    .Where(a => a.CursoId == id).ToListAsync();
            return View("SetAsignaturas", cursosA);
        }

        public async Task<IActionResult> SaveAsignatura(int? id, int cursoId)
        {
            ViewData["DescripcionCurso"] = _context.Curso.Find(cursoId).Descripcion;
            ViewData["_CursoId"] = cursoId;
            if (id == null)
            {
                ViewData["Operacion"] = "Create";
                ViewData["AsignaturaId"] = new SelectList(_context.Set<Asignatura>(), "AsignaturaId", "Descripcion");
                return PartialView("_SaveAsignaturaPartialView");
            }
            else
            {
                var acurso = await _context.AsignaturaCurso.FirstAsync(a => a.AsignaturaCursoId == id);
                ViewData["Operacion"] = "Edit";
                ViewData["AsignaturaId"] = new SelectList(_context.Set<Asignatura>(), "AsignaturaId", "Descripcion", acurso.AsignaturaCursoId);
                return PartialView("_SaveAsignaturaPartialView", acurso);
            }
        }

        [HttpPost, ActionName("SaveAsignatura")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAsignatura(int? id)
        {
            string operacion = (id != null) ? "Edit" : "Create";
            var acursoToSave = (operacion == "Edit") ? await _context.AsignaturaCurso.FindAsync(id) : new AsignaturaCurso();

            if (ModelState.IsValid)
            {
                try
                {
                    if (await TryUpdateModelAsync<AsignaturaCurso>(acursoToSave, "", a => a.CursoId, a => a.AsignaturaId))
                    {
                        if (operacion == "Create")
                        {
                            _context.AsignaturaCurso.Add(acursoToSave);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists((int)id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Url.Action("SetAsignaturas", "Cursos", new { id = acursoToSave.CursoId }));
            }
            return PartialView("_SaveAsignaturaPartialView", acursoToSave);
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.CursoId == id);
        }
    }
}
